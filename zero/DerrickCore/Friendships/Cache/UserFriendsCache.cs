using Abp;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using Derrick.Chat;
using System.Linq;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Microsoft.AspNet.Identity;
using Derrick.Authorization.Users;

namespace Derrick.Friendships.Cache
{
    /// <summary>
    /// �û����ѻ���ʵ��
    /// </summary>
    public class UserFriendsCache : IUserFriendsCache, ISingletonDependency
    {
        /// <summary>
        /// �����������
        /// </summary>
        private readonly ICacheManager _cacheManager;
        /// <summary>
        /// ���Ѳִ�
        /// </summary>
        private readonly IRepository<Friendship, long> _friendshipRepository;
        /// <summary>
        /// ������Ϣ�ִ�
        /// </summary>
        private readonly IRepository<ChatMessage, long> _chatMessageRepository;
        /// <summary>
        /// �̻���������
        /// </summary>
        private readonly ITenantCache _tenantCache;
        /// <summary>
        /// �û���������
        /// </summary>
        private readonly UserManager _userManager;
        /// <summary>
        /// ������Ԫ����
        /// </summary>
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        private readonly object _syncObj = new object();
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="cacheManager">�����������</param>
        /// <param name="friendshipRepository">���Ѳִ�</param>
        /// <param name="chatMessageRepository">������Ϣ�ִ�</param>
        /// <param name="tenantCache">�̻���������</param>
        /// <param name="userManager">�û���������</param>
        /// <param name="unitOfWorkManager">������Ԫ����</param>
        public UserFriendsCache(
            ICacheManager cacheManager,
            IRepository<Friendship, long> friendshipRepository,
            IRepository<ChatMessage, long> chatMessageRepository,
            ITenantCache tenantCache,
            UserManager userManager,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _cacheManager = cacheManager;
            _friendshipRepository = friendshipRepository;
            _chatMessageRepository = chatMessageRepository;
            _tenantCache = tenantCache;
            _userManager = userManager;
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="userIdentifier">�û���ʶ</param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual UserWithFriendsCacheItem GetCacheItem(UserIdentifier userIdentifier)
        {
            return _cacheManager
                .GetCache(FriendCacheItem.CacheName)
                .Get<string, UserWithFriendsCacheItem>(userIdentifier.ToUserIdentifierString(), f => GetUserFriendsCacheItemInternal(userIdentifier));
        }
        /// <summary>
        /// ��ȡ�������Null
        /// </summary>
        /// <param name="userIdentifier">�û���ʶ</param>
        /// <returns></returns>
        public virtual UserWithFriendsCacheItem GetCacheItemOrNull(UserIdentifier userIdentifier)
        {
            return _cacheManager
                .GetCache(FriendCacheItem.CacheName)
                .GetOrDefault<string, UserWithFriendsCacheItem>(userIdentifier.ToUserIdentifierString());
        }
        /// <summary>
        /// ����δ����Ϣ����
        /// </summary>
        /// <param name="userIdentifier">�û���ʶ</param>
        /// <param name="friendIdentifier">�û����ѱ�ʶ</param>
        [UnitOfWork]
        public virtual void ResetUnreadMessageCount(UserIdentifier userIdentifier, UserIdentifier friendIdentifier)
        {
            var user = GetCacheItemOrNull(userIdentifier);
            if (user == null)
            {
                return;
            }

            lock (_syncObj)
            {
                var friend = user.Friends.FirstOrDefault(
                    f => f.FriendUserId == friendIdentifier.UserId &&
                         f.FriendTenantId == friendIdentifier.TenantId
                );

                if (friend == null)
                {
                    return;
                }

                friend.UnreadMessageCount = 0;
            }
        }

        /// <summary>
        /// ����δ����Ϣ����
        /// </summary>
        /// <param name="userIdentifier">�û���ʶ</param>
        /// <param name="friendIdentifier">�û����ѱ�ʶ</param>
        /// <param name="change">�޸ĵ�����</param>
        [UnitOfWork]
        public virtual void IncreaseUnreadMessageCount(UserIdentifier userIdentifier, UserIdentifier friendIdentifier, int change)
        {
            var user = GetCacheItemOrNull(userIdentifier);
            if (user == null)
            {
                return;
            }

            lock (_syncObj)
            {
                var friend = user.Friends.FirstOrDefault(
                    f => f.FriendUserId == friendIdentifier.UserId &&
                         f.FriendTenantId == friendIdentifier.TenantId
                );

                if (friend == null)
                {
                    return;
                }

                friend.UnreadMessageCount += change;
            }
        }
        /// <summary>
        /// ��Ӻ���
        /// </summary>
        /// <param name="userIdentifier">�û���ʶ</param>
        /// <param name="friend">���ѻ�����</param>
        public void AddFriend(UserIdentifier userIdentifier, FriendCacheItem friend)
        {
            var user = GetCacheItemOrNull(userIdentifier);
            if (user == null)
            {
                return;
            }

            lock (_syncObj)
            {
                if (!user.Friends.ContainsFriend(friend))
                {
                    user.Friends.Add(friend);
                }
            }
        }
        /// <summary>
        /// �Ƴ�����
        /// </summary>
        /// <param name="userIdentifier">�û���ʶ</param>
        /// <param name="friend">���ѻ�����</param>
        public void RemoveFriend(UserIdentifier userIdentifier, FriendCacheItem friend)
        {
            var user = GetCacheItemOrNull(userIdentifier);
            if (user == null)
            {
                return;
            }

            lock (_syncObj)
            {
                if (user.Friends.ContainsFriend(friend))
                {
                    user.Friends.Remove(friend);
                }
            }
        }
        /// <summary>
        /// ���º���
        /// </summary>
        /// <param name="userIdentifier">�û���ʶ</param>
        /// <param name="friend">���ѻ�����</param>
        public void UpdateFriend(UserIdentifier userIdentifier, FriendCacheItem friend)
        {
            var user = GetCacheItemOrNull(userIdentifier);
            if (user == null)
            {
                return;
            }

            lock (_syncObj)
            {
                var existingFriendIndex = user.Friends.FindIndex(
                    f => f.FriendUserId == friend.FriendUserId &&
                         f.FriendTenantId == friend.FriendTenantId
                );

                if (existingFriendIndex >= 0)
                {
                    user.Friends[existingFriendIndex] = friend;
                }
            }
        }
        /// <summary>
        /// �ڲ���ȡ�û����ѻ�����
        /// </summary>
        /// <param name="userIdentifier">�û���ʶ</param>
        /// <returns></returns>
        [UnitOfWork]
        protected virtual UserWithFriendsCacheItem GetUserFriendsCacheItemInternal(UserIdentifier userIdentifier)
        {
            var tenancyName = userIdentifier.TenantId.HasValue
                ? _tenantCache.GetOrNull(userIdentifier.TenantId.Value)?.TenancyName
                : null;

            using (_unitOfWorkManager.Current.SetTenantId(userIdentifier.TenantId))
            {
                var friendCacheItems =
                    (from friendship in _friendshipRepository.GetAll()
                     join chatMessage in _chatMessageRepository.GetAll() on
                     new { UserId = userIdentifier.UserId, TenantId = userIdentifier.TenantId, TargetUserId = friendship.FriendUserId, TargetTenantId = friendship.FriendTenantId, ChatSide = ChatSide.Receiver } equals
                     new { UserId = chatMessage.UserId, TenantId = chatMessage.TenantId, TargetUserId = chatMessage.TargetUserId, TargetTenantId = chatMessage.TargetTenantId, ChatSide = chatMessage.Side } into chatMessageJoined
                     where friendship.UserId == userIdentifier.UserId
                     select new FriendCacheItem
                     {
                         FriendUserId = friendship.FriendUserId,
                         FriendTenantId = friendship.FriendTenantId,
                         State = friendship.State,
                         FriendUserName = friendship.FriendUserName,
                         FriendTenancyName = friendship.FriendTenancyName,
                         FriendProfilePictureId = friendship.FriendProfilePictureId,
                         UnreadMessageCount = chatMessageJoined.Count(cm => cm.ReadState == ChatMessageReadState.Unread)
                     }).ToList();

                var user = _userManager.FindById(userIdentifier.UserId);

                return new UserWithFriendsCacheItem
                {
                    TenantId = userIdentifier.TenantId,
                    UserId = userIdentifier.UserId,
                    TenancyName = tenancyName,
                    UserName = user.UserName,
                    ProfilePictureId = user.ProfilePictureId,
                    Friends = friendCacheItems
                };
            }
        }
    }
}