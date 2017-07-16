using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using Derrick.Authorization.Users.Dto;
using Derrick.MultiTenancy;

namespace Derrick.Authorization.Users
{
    /// <summary>
    /// <see cref="IUserLinkAppService"/>ʵ�֣��û�����APP����
    /// </summary>
    [AbpAuthorize]
    public class UserLinkAppService : AbpZeroTemplateAppServiceBase, IUserLinkAppService
    {
        /// <summary>
        /// ABP��¼��Ϣ������Ͱ���
        /// </summary>
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        /// <summary>
        /// �û����ӹ���
        /// </summary>
        private readonly IUserLinkManager _userLinkManager;
        /// <summary>
        /// �̻��ִ�
        /// </summary>
        private readonly IRepository<Tenant> _tenantRepository;
        /// <summary>
        /// �û��ʺŲִ�
        /// </summary>
        private readonly IRepository<UserAccount, long> _userAccountRepository;
        /// <summary>
        /// ��¼����
        /// </summary>
        private readonly LogInManager _logInManager;
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="abpLoginResultTypeHelper">ABP��¼��Ϣ������Ͱ���</param>
        /// <param name="userLinkManager">�û����ӹ���</param>
        /// <param name="tenantRepository">�̻��ִ�</param>
        /// <param name="userAccountRepository">�û��ʺŲִ�</param>
        /// <param name="logInManager">��¼����</param>
        public UserLinkAppService(
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            IUserLinkManager userLinkManager,
            IRepository<Tenant> tenantRepository,
            IRepository<UserAccount, long> userAccountRepository, 
            LogInManager logInManager)
        {
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _userLinkManager = userLinkManager;
            _tenantRepository = tenantRepository;
            _userAccountRepository = userAccountRepository;
            _logInManager = logInManager;
        }
        /// <summary>
        /// ���ӵ��û�
        /// </summary>
        /// <param name="linkToUserInput">�û�����Input��Ϣ</param>
        /// <returns></returns>
        public async Task LinkToUser(LinkToUserInput input)
        {
            var loginResult = await _logInManager.LoginAsync(input.UsernameOrEmailAddress, input.Password, input.TenancyName);

            if (loginResult.Result != AbpLoginResultType.Success)
            {
                throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, input.UsernameOrEmailAddress, input.TenancyName);
            }

            if (AbpSession.IsUser(loginResult.User))
            {
                throw new UserFriendlyException(L("YouCannotLinkToSameAccount"));
            }

            if (loginResult.User.ShouldChangePasswordOnNextLogin)
            {
                throw new UserFriendlyException(L("ChangePasswordBeforeLinkToAnAccount"));
            }

            await _userLinkManager.Link(GetCurrentUser(), loginResult.User);
        }
        /// <summary>
        /// ��ȡ�����û�Dto�б�(����ҳ)
        /// </summary>
        /// <param name="input">�û�����Input</param>
        /// <returns></returns>
        public async Task<PagedResultDto<LinkedUserDto>> GetLinkedUsers(GetLinkedUsersInput input)
        {
            var currentUserAccount = await _userLinkManager.GetUserAccountAsync(AbpSession.ToUserIdentifier());
            if (currentUserAccount == null)
            {
                return new PagedResultDto<LinkedUserDto>(0, new List<LinkedUserDto>());
            }

            var query = CreateLinkedUsersQuery(currentUserAccount, input.Sorting);
            query = query.Skip(input.SkipCount)
                        .Take(input.MaxResultCount);

            var totalCount = await query.CountAsync();
            var linkedUsers = await query.ToListAsync();

            return new PagedResultDto<LinkedUserDto>(
                totalCount,
                linkedUsers
            );
        }
        /// <summary>
        /// ��ȡ���ʹ�õ������û�
        /// </summary>
        /// <returns></returns>
        [DisableAuditing]
        public async Task<ListResultDto<LinkedUserDto>> GetRecentlyUsedLinkedUsers()
        {
            var currentUserAccount = await _userLinkManager.GetUserAccountAsync(AbpSession.ToUserIdentifier());
            if (currentUserAccount == null)
            {
                return new ListResultDto<LinkedUserDto>();
            }

            var query = CreateLinkedUsersQuery(currentUserAccount, "LastLoginTime DESC");
            var recentlyUsedlinkedUsers = await query.Skip(0).Take(3).ToListAsync();

            return new ListResultDto<LinkedUserDto>(recentlyUsedlinkedUsers);
        }
        /// <summary>
        /// �û��Ͽ�����
        /// </summary>
        /// <param name="input">�Ͽ������û�Input</param>
        /// <returns></returns>
        public async Task UnlinkUser(UnlinkUserInput input)
        {
            var currentUserAccount = await _userLinkManager.GetUserAccountAsync(AbpSession.ToUserIdentifier());

            if (!currentUserAccount.UserLinkId.HasValue)
            {
                throw new ApplicationException(L("You are not linked to any account"));
            }

            if (!await _userLinkManager.AreUsersLinked(AbpSession.ToUserIdentifier(), input.ToUserIdentifier()))
            {
                return;
            }

            await _userLinkManager.Unlink(input.ToUserIdentifier());
        }
        /// <summary>
        /// ���������û���ѯ
        /// </summary>
        /// <param name="currentUserAccount">��ǰ�û��ʺ�</param>
        /// <param name="sorting">����</param>
        /// <returns></returns>
        private IQueryable<LinkedUserDto> CreateLinkedUsersQuery(UserAccount currentUserAccount, string sorting)
        {
            var currentUserIdentifier = AbpSession.ToUserIdentifier();

            return (from userAccount in _userAccountRepository.GetAll()
                    join tenant in _tenantRepository.GetAll() on userAccount.TenantId equals tenant.Id into tenantJoined
                    from tenant in tenantJoined.DefaultIfEmpty()
                    where
                        (userAccount.TenantId != currentUserIdentifier.TenantId ||
                        userAccount.UserId != currentUserIdentifier.UserId) &&
                        userAccount.UserLinkId.HasValue &&
                        userAccount.UserLinkId == currentUserAccount.UserLinkId
                    select new LinkedUserDto
                    {
                        Id = userAccount.UserId,
                        TenantId = userAccount.TenantId,
                        TenancyName = tenant == null ? "." : tenant.TenancyName,
                        Username = userAccount.UserName,
                        LastLoginTime = userAccount.LastLoginTime
                    }).OrderBy(sorting);
        }
    }
}