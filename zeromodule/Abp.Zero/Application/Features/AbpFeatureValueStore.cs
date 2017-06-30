using System.Threading.Tasks;
using Abp.Application.Editions;
using Abp.Authorization.Users;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.MultiTenancy;
using Abp.Runtime.Caching;

namespace Abp.Application.Features
{
    /// <summary>
    /// <see cref="IFeatureValueStore"/>��ʵ��
    /// </summary>
    public abstract class AbpFeatureValueStore<TTenant, TUser> : 
        IAbpZeroFeatureValueStore, 
        ITransientDependency,
        IEventHandler<EntityChangedEventData<Edition>>,
        IEventHandler<EntityChangedEventData<EditionFeatureSetting>>

        where TTenant : AbpTenant<TUser>
        where TUser : AbpUser<TUser>
    {
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<TenantFeatureSetting, long> _tenantFeatureRepository;
        private readonly IRepository<TTenant> _tenantRepository;
        private readonly IRepository<EditionFeatureSetting, long> _editionFeatureRepository;
        private readonly IFeatureManager _featureManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// ���캯��
        /// </summary>
        protected AbpFeatureValueStore(
            ICacheManager cacheManager,
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
            IRepository<TTenant> tenantRepository,
            IRepository<EditionFeatureSetting, long> editionFeatureRepository,
            IFeatureManager featureManager,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _cacheManager = cacheManager;
            _tenantFeatureRepository = tenantFeatureRepository;
            _tenantRepository = tenantRepository;
            _editionFeatureRepository = editionFeatureRepository;
            _featureManager = featureManager;
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// ��ȡֵ(û���򷵻�Null)
        /// </summary>
        /// <param name="tenantId">�⻧ID</param>
        /// <param name="featureName">��������</param>
        /// <returns></returns>
        public virtual Task<string> GetValueOrNullAsync(int tenantId, Feature feature)
        {
            return GetValueOrNullAsync(tenantId, feature.Name);
        }

        /// <summary>
        /// ��ȡ�汾ֵ(û���򷵻�Null)
        /// </summary>
        /// <param name="editionId">�⻧ID</param>
        /// <param name="featureName">��������</param>
        /// <returns></returns>
        public virtual async Task<string> GetEditionValueOrNullAsync(int editionId, string featureName)
        {
            var cacheItem = await GetEditionFeatureCacheItemAsync(editionId);
            return cacheItem.FeatureValues.GetOrDefault(featureName);
        }

        /// <summary>
        /// ��ȡֵ(û���򷵻�Null)
        /// </summary>
        /// <param name="tenantId">�⻧ID</param>
        /// <param name="featureName">��������</param>
        /// <returns></returns>
        public async Task<string> GetValueOrNullAsync(int tenantId, string featureName)
        {
            var cacheItem = await GetTenantFeatureCacheItemAsync(tenantId);
            var value = cacheItem.FeatureValues.GetOrDefault(featureName);
            if (value != null)
            {
                return value;
            }

            if (cacheItem.EditionId.HasValue)
            {
                value = await GetEditionValueOrNullAsync(cacheItem.EditionId.Value, featureName);
                if (value != null)
                {
                    return value;
                }
            }

            return null;
        }

        /// <summary>
        /// ���ð汾����ֵ - �첽
        /// </summary>
        /// <param name="editionId">�汾ID</param>
        /// <param name="featureName">��������</param>
        /// <param name="value">ֵ</param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task SetEditionFeatureValueAsync(int editionId, string featureName, string value)
        {
            if (await GetEditionValueOrNullAsync(editionId, featureName) == value)
            {
                return;
            }

            var currentFeature = await _editionFeatureRepository.FirstOrDefaultAsync(f => f.EditionId == editionId && f.Name == featureName);

            var feature = _featureManager.GetOrNull(featureName);
            if (feature == null || feature.DefaultValue == value)
            {
                if (currentFeature != null)
                {
                    await _editionFeatureRepository.DeleteAsync(currentFeature);
                }

                return;
            }

            if (currentFeature == null)
            {
                await _editionFeatureRepository.InsertAsync(new EditionFeatureSetting(editionId, featureName, value));
            }
            else
            {
                currentFeature.Value = value;
            }
        }

        /// <summary>
        /// ��ȡ�⻧���ܻ�����
        /// </summary>
        /// <param name="tenantId">�⻧ID</param>
        /// <returns></returns>
        protected async Task<TenantFeatureCacheItem> GetTenantFeatureCacheItemAsync(int tenantId)
        {
            return await _cacheManager.GetTenantFeatureCache().GetAsync(tenantId, async () =>
            {
                TTenant tenant;
                using (var uow = _unitOfWorkManager.Begin())
                {
                    using (_unitOfWorkManager.Current.SetTenantId(null))
                    {
                        tenant = await _tenantRepository.GetAsync(tenantId);

                        await uow.CompleteAsync();
                    }
                }

                var newCacheItem = new TenantFeatureCacheItem { EditionId = tenant.EditionId };

                using (var uow = _unitOfWorkManager.Begin())
                {
                    using (_unitOfWorkManager.Current.SetTenantId(tenantId))
                    {
                        var featureSettings = await _tenantFeatureRepository.GetAllListAsync();
                        foreach (var featureSetting in featureSettings)
                        {
                            newCacheItem.FeatureValues[featureSetting.Name] = featureSetting.Value;
                        }

                        await uow.CompleteAsync();
                    }
                }

                return newCacheItem;
            });
        }

        /// <summary>
        /// ��ȡ�汾���ܻ�����
        /// </summary>
        /// <param name="editionId">�汾ID</param>
        /// <returns></returns>
        protected virtual async Task<EditionfeatureCacheItem> GetEditionFeatureCacheItemAsync(int editionId)
        {
            return await _cacheManager
                .GetEditionFeatureCache()
                .GetAsync(
                    editionId,
                    async () => await CreateEditionFeatureCacheItem(editionId)
                );
        }

        /// <summary>
        /// �����汾���ܻ�����
        /// </summary>
        /// <param name="editionId">�汾ID</param>
        /// <returns></returns>
        protected virtual async Task<EditionfeatureCacheItem> CreateEditionFeatureCacheItem(int editionId)
        {
            var newCacheItem = new EditionfeatureCacheItem();

            var featureSettings = await _editionFeatureRepository.GetAllListAsync(f => f.EditionId == editionId);
            foreach (var featureSetting in featureSettings)
            {
                newCacheItem.FeatureValues[featureSetting.Name] = featureSetting.Value;
            }

            return newCacheItem;
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void HandleEvent(EntityChangedEventData<EditionFeatureSetting> eventData)
        {
            _cacheManager.GetEditionFeatureCache().Remove(eventData.Entity.EditionId);
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void HandleEvent(EntityChangedEventData<Edition> eventData)
        {
            if (eventData.Entity.IsTransient())
            {
                return;
            }

            _cacheManager.GetEditionFeatureCache().Remove(eventData.Entity.Id);
        }
    }
}