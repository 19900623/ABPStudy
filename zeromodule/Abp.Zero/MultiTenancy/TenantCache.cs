using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.Runtime.Caching;
using Abp.Runtime.Security;

namespace Abp.MultiTenancy
{
    /// <summary>
    /// �̻�����ʵ��
    /// </summary>
    /// <typeparam name="TTenant"></typeparam>
    /// <typeparam name="TUser"></typeparam>
    public class TenantCache<TTenant, TUser> : ITenantCache, IEventHandler<EntityChangedEventData<TTenant>>
        where TTenant : AbpTenant<TUser>
        where TUser : AbpUser<TUser>
    {
        /// <summary>
        /// �����������
        /// </summary>
        private readonly ICacheManager _cacheManager;
        /// <summary>
        /// �̻��ִ�����
        /// </summary>
        private readonly IRepository<TTenant> _tenantRepository;
        /// <summary>
        /// ������Ԫ����
        /// </summary>
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="cacheManager">�����������</param>
        /// <param name="tenantRepository">�̻��ִ�����</param>
        /// <param name="unitOfWorkManager">������Ԫ����</param>
        public TenantCache(
            ICacheManager cacheManager,
            IRepository<TTenant> tenantRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _cacheManager = cacheManager;
            _tenantRepository = tenantRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        /// <summary>
        /// ��ȡ�̻�������
        /// </summary>
        /// <param name="tenantId">�̻�ID</param>
        /// <returns></returns>
        public virtual TenantCacheItem Get(int tenantId)
        {
            var cacheItem = GetOrNull(tenantId);

            if (cacheItem == null)
            {
                throw new AbpException("There is no tenant with given id: " + tenantId);
            }

            return cacheItem;
        }
        /// <summary>
        /// ��ȡ�̻�������
        /// </summary>
        /// <param name="tenancyName">�̻�����</param>
        /// <returns></returns>
        public virtual TenantCacheItem Get(string tenancyName)
        {
            var cacheItem = GetOrNull(tenancyName);

            if (cacheItem == null)
            {
                throw new AbpException("There is no tenant with given tenancy name: " + tenancyName);
            }

            return cacheItem;
        }
        /// <summary>
        /// ��ȡ�̻��������Null
        /// </summary>
        /// <param name="tenancyName">�̻�����</param>
        /// <returns></returns>
        public virtual TenantCacheItem GetOrNull(string tenancyName)
        {
            var tenantId = _cacheManager.GetTenantByNameCache()
                .Get(tenancyName, () => GetTenantOrNull(tenancyName)?.Id);

            if (tenantId == null)
            {
                return null;
            }

            return Get(tenantId.Value);
        }
        /// <summary>
        /// ��ȡ�̻��������Null
        /// </summary>
        /// <param name="tenantId">�̻�ID</param>
        /// <returns></returns>
        public TenantCacheItem GetOrNull(int tenantId)
        {
            return _cacheManager
                .GetTenantCache()
                .Get(
                    tenantId,
                    () =>
                    {
                        var tenant = GetTenantOrNull(tenantId);
                        if (tenant == null)
                        {
                            return null;
                        }

                        return CreateTenantCacheItem(tenant);
                    }
                );
        }
        /// <summary>
        /// �����̻�������
        /// </summary>
        /// <param name="tenant">�̻�����</param>
        /// <returns></returns>
        protected virtual TenantCacheItem CreateTenantCacheItem(TTenant tenant)
        {
            return new TenantCacheItem
            {
                Id = tenant.Id,
                Name = tenant.Name,
                TenancyName = tenant.TenancyName,
                EditionId = tenant.EditionId,
                ConnectionString = SimpleStringCipher.Instance.Decrypt(tenant.ConnectionString),
                IsActive = tenant.IsActive
            };
        }
        /// <summary>
        /// ��ȡ�̻���Null
        /// </summary>
        /// <param name="tenantId">�̻�ID</param>
        /// <returns></returns>
        [UnitOfWork]
        protected virtual TTenant GetTenantOrNull(int tenantId)
        {
            using (_unitOfWorkManager.Current.SetTenantId(null))
            {
                return _tenantRepository.FirstOrDefault(tenantId);
            }
        }
        /// <summary>
        /// ��ȡ�̻���Null
        /// </summary>
        /// <param name="tenancyName">�̻�����</param>
        /// <returns></returns>
        [UnitOfWork]
        protected virtual TTenant GetTenantOrNull(string tenancyName)
        {
            using (_unitOfWorkManager.Current.SetTenantId(null))
            {
                return _tenantRepository.FirstOrDefault(t => t.TenancyName == tenancyName);
            }
        }
        /// <summary>
        /// �����̻��޸��¼�
        /// </summary>
        /// <param name="eventData">�¼�����</param>
        public void HandleEvent(EntityChangedEventData<TTenant> eventData)
        {
            var existingCacheItem = _cacheManager.GetTenantCache().GetOrDefault(eventData.Entity.Id);

            _cacheManager
                .GetTenantByNameCache()
                .Remove(
                    existingCacheItem != null
                        ? existingCacheItem.TenancyName
                        : eventData.Entity.TenancyName
                );

            _cacheManager
                .GetTenantCache()
                .Remove(eventData.Entity.Id);
        }
    }
}