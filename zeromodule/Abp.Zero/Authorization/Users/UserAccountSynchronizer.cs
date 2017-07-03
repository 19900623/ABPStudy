using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;

namespace Abp.Authorization.Users
{
    /// <summary>
    /// Synchronizes a user's information to user account.
    /// ͬ��һ���û���Ϣ���û��ʺ�
    /// </summary>
    public class UserAccountSynchronizer :
        IEventHandler<EntityCreatedEventData<AbpUserBase>>,
        IEventHandler<EntityDeletedEventData<AbpUserBase>>,
        IEventHandler<EntityUpdatedEventData<AbpUserBase>>,
        ITransientDependency
    {
        /// <summary>
        /// �û��ʺŲִ�
        /// </summary>
        private readonly IRepository<UserAccount, long> _userAccountRepository;
        /// <summary>
        /// ������Ԫ��������
        /// </summary>
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// ���캯��
        /// </summary>
        public UserAccountSynchronizer(
            IRepository<UserAccount, long> userAccountRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _userAccountRepository = userAccountRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// �����û��Ĵ����¼�
        /// </summary>
        [UnitOfWork]
        public virtual void HandleEvent(EntityCreatedEventData<AbpUserBase> eventData)
        {
            using (_unitOfWorkManager.Current.SetTenantId(null))
            {
                _userAccountRepository.Insert(new UserAccount
                {
                    TenantId = eventData.Entity.TenantId,
                    UserName = eventData.Entity.UserName,
                    UserId = eventData.Entity.Id,
                    EmailAddress = eventData.Entity.EmailAddress,
                    LastLoginTime = eventData.Entity.LastLoginTime
                });
            }
        }

        /// <summary>
        /// Handles deletion event of user
        /// �����û���ɾ���¼�
        /// </summary>
        /// <param name="eventData"></param>
        [UnitOfWork]
        public virtual void HandleEvent(EntityDeletedEventData<AbpUserBase> eventData)
        {
            using (_unitOfWorkManager.Current.SetTenantId(null))
            {
                var userAccount =
                    _userAccountRepository.FirstOrDefault(
                        ua => ua.TenantId == eventData.Entity.TenantId && ua.UserId == eventData.Entity.Id);
                if (userAccount != null)
                {
                    _userAccountRepository.Delete(userAccount);
                }
            }
        }

        /// <summary>
        /// Handles update event of user
        /// �����û����޸��¼�
        /// </summary>
        /// <param name="eventData"></param>
        [UnitOfWork]
        public virtual void HandleEvent(EntityUpdatedEventData<AbpUserBase> eventData)
        {
            using (_unitOfWorkManager.Current.SetTenantId(null))
            {
                var userAccount = _userAccountRepository.FirstOrDefault(ua => ua.TenantId == eventData.Entity.TenantId && ua.UserId == eventData.Entity.Id);
                if (userAccount != null)
                {
                    userAccount.UserName = eventData.Entity.UserName;
                    userAccount.EmailAddress = eventData.Entity.EmailAddress;
                    userAccount.LastLoginTime = eventData.Entity.LastLoginTime;
                    _userAccountRepository.Update(userAccount);
                }
            }
        }
    }
}