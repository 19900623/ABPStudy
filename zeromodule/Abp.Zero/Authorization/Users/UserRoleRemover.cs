using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;

namespace Abp.Authorization.Users
{
    /// <summary>
    /// Removes the user from all organization units when a user is deleted.
    /// ��һ���û�ɾ����ʱ��ɾ���û����еĽ�ɫ��Ϣ
    /// </summary>
    public class UserRoleRemover :
        IEventHandler<EntityDeletedEventData<AbpUserBase>>,
        ITransientDependency
    {
        /// <summary>
        /// �û���ɫ�ִ�
        /// </summary>
        private readonly IRepository<UserRole, long> _userRoleRepository;
        /// <summary>
        /// ������Ԫ����
        /// </summary>
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="unitOfWorkManager">������Ԫ����</param>
        /// <param name="userRoleRepository">�û���ɫ�ִ�</param>
        public UserRoleRemover(
            IUnitOfWorkManager unitOfWorkManager, 
            IRepository<UserRole, long> userRoleRepository)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// �����û�ɾ���¼�
        /// </summary>
        /// <param name="eventData">�¼�����</param>
        [UnitOfWork]
        public virtual void HandleEvent(EntityDeletedEventData<AbpUserBase> eventData)
        {
            using (_unitOfWorkManager.Current.SetTenantId(eventData.Entity.TenantId))
            {
                _userRoleRepository.Delete(
                    ur => ur.UserId == eventData.Entity.Id
                );
            }
        }
    }
}