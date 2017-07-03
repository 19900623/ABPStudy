using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Authorization.Users;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Abp.IdentityFramework;
using Abp.Localization;
using Abp.Runtime.Caching;
using Abp.Zero;
using Microsoft.AspNet.Identity;

namespace Abp.MultiTenancy
{
    /// <summary>
    /// Tenant manager.Implements domain logic for <see cref="AbpTenant{TUser}"/>.
    /// �̻�����ʵ��<see cref="AbpTenant{TUser}"/>�����߼�
    /// </summary>
    /// <typeparam name="TTenant">Type of the application Tenant / Ӧ�ó����̻�������</typeparam>
    /// <typeparam name="TUser">Type of the application User / Ӧ�ó����û�������</typeparam>
    public abstract class AbpTenantManager<TTenant, TUser> : IDomainService,
        IEventHandler<EntityChangedEventData<TTenant>>,
        IEventHandler<EntityDeletedEventData<Edition>>
        where TTenant : AbpTenant<TUser>
        where TUser : AbpUser<TUser>
    {
        /// <summary>
        /// ABP�汾��������
        /// </summary>
        public AbpEditionManager EditionManager { get; set; }
        /// <summary>
        /// ���ػ���������
        /// </summary>
        public ILocalizationManager LocalizationManager { get; set; }
        /// <summary>
        /// �����������
        /// </summary>
        public ICacheManager CacheManager { get; set; }
        /// <summary>
        /// ���ܹ�������
        /// </summary>
        public IFeatureManager FeatureManager { get; set; }
        /// <summary>
        /// �̻��ִ�
        /// </summary>
        protected IRepository<TTenant> TenantRepository { get; set; }
        /// <summary>
        /// �̻����ִܲ�
        /// </summary>
        protected IRepository<TenantFeatureSetting, long> TenantFeatureRepository { get; set; }
        /// <summary>
        /// ABP Zero����ֵ�洢
        /// </summary>
        private readonly IAbpZeroFeatureValueStore _featureValueStore;
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="tenantRepository">�̻��ִ�</param>
        /// <param name="tenantFeatureRepository">�̻����ִܲ�</param>
        /// <param name="editionManager">ABP�汾��������</param>
        /// <param name="featureValueStore">ABP Zero����ֵ�洢</param>
        protected AbpTenantManager(
            IRepository<TTenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
            AbpEditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore)
        {
            _featureValueStore = featureValueStore;
            TenantRepository = tenantRepository;
            TenantFeatureRepository = tenantFeatureRepository;
            EditionManager = editionManager;
            LocalizationManager = NullLocalizationManager.Instance;
        }
        /// <summary>
        /// �̻��б�
        /// </summary>
        public virtual IQueryable<TTenant> Tenants { get { return TenantRepository.GetAll(); } }
        /// <summary>
        /// �����̻�
        /// </summary>
        /// <param name="tenant">�̻�����</param>
        /// <returns></returns>
        public virtual async Task<IdentityResult> CreateAsync(TTenant tenant)
        {
            if (await TenantRepository.FirstOrDefaultAsync(t => t.TenancyName == tenant.TenancyName) != null)
            {
                return AbpIdentityResult.Failed(string.Format(L("TenancyNameIsAlreadyTaken"), tenant.TenancyName));
            }

            var validationResult = await ValidateTenantAsync(tenant);
            if (!validationResult.Succeeded)
            {
                return validationResult;
            }

            await TenantRepository.InsertAsync(tenant);
            return IdentityResult.Success;
        }
        /// <summary>
        /// �����̻�
        /// </summary>
        /// <param name="tenant">�̻�����</param>
        /// <returns></returns>
        public async Task<IdentityResult> UpdateAsync(TTenant tenant)
        {
            if (await TenantRepository.FirstOrDefaultAsync(t => t.TenancyName == tenant.TenancyName && t.Id != tenant.Id) != null)
            {
                return AbpIdentityResult.Failed(string.Format(L("TenancyNameIsAlreadyTaken"), tenant.TenancyName));
            }

            await TenantRepository.UpdateAsync(tenant);
            return IdentityResult.Success;
        }
        /// <summary>
        /// ����ID�����̻�
        /// </summary>
        /// <param name="id">�̻�ID</param>
        /// <returns></returns>
        public virtual async Task<TTenant> FindByIdAsync(int id)
        {
            return await TenantRepository.FirstOrDefaultAsync(id);
        }
        /// <summary>
        /// ͨ���̻���ȡID�����û�ҵ����׳��쳣
        /// </summary>
        /// <param name="id">�̻�ID</param>
        /// <returns></returns>
        public virtual async Task<TTenant> GetByIdAsync(int id)
        {
            var tenant = await FindByIdAsync(id);
            if (tenant == null)
            {
                throw new AbpException("There is no tenant with id: " + id);
            }

            return tenant;
        }
        /// <summary>
        /// ͨ���̻����Ʋ����̻�
        /// </summary>
        /// <param name="tenancyName">�̻�����</param>
        /// <returns></returns>
        public virtual Task<TTenant> FindByTenancyNameAsync(string tenancyName)
        {
            return TenantRepository.FirstOrDefaultAsync(t => t.TenancyName == tenancyName);
        }
        /// <summary>
        /// ɾ���̻�
        /// </summary>
        /// <param name="tenant">�̻�����</param>
        /// <returns></returns>
        public virtual async Task<IdentityResult> DeleteAsync(TTenant tenant)
        {
            await TenantRepository.DeleteAsync(tenant);
            return IdentityResult.Success;
        }
        /// <summary>
        /// ��ȡ����ֵ��Null
        /// </summary>
        /// <param name="tenantId">�̻�ID</param>
        /// <param name="featureName">��������</param>
        /// <returns></returns>
        public Task<string> GetFeatureValueOrNullAsync(int tenantId, string featureName)
        {
            return _featureValueStore.GetValueOrNullAsync(tenantId, featureName);
        }
        /// <summary>
        /// ��ȡ����ֵ�б�
        /// </summary>
        /// <param name="tenantId">�̻�ID</param>
        /// <returns></returns>
        public virtual async Task<IReadOnlyList<NameValue>> GetFeatureValuesAsync(int tenantId)
        {
            var values = new List<NameValue>();

            foreach (var feature in FeatureManager.GetAll())
            {
                values.Add(new NameValue(feature.Name, await GetFeatureValueOrNullAsync(tenantId, feature.Name) ?? feature.DefaultValue));
            }

            return values;
        }
        /// <summary>
        /// ���ù���ֵ
        /// </summary>
        /// <param name="tenantId">�̻�ID</param>
        /// <param name="values">����ֵ�б�</param>
        /// <returns></returns>
        public virtual async Task SetFeatureValuesAsync(int tenantId, params NameValue[] values)
        {
            if (values.IsNullOrEmpty())
            {
                return;
            }

            foreach (var value in values)
            {
                await SetFeatureValueAsync(tenantId, value.Name, value.Value);
            }
        }
        /// <summary>
        /// ���ù���ֵ
        /// </summary>
        /// <param name="tenantId">�̻�ID</param>
        /// <param name="featureName">��������</param>
        /// <param name="value">ֵ</param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task SetFeatureValueAsync(int tenantId, string featureName, string value)
        {
            await SetFeatureValueAsync(await GetByIdAsync(tenantId), featureName, value);
        }
        /// <summary>
        /// ���ù���ֵ
        /// </summary>
        /// <param name="tenant">�̻�����</param>
        /// <param name="featureName">��������</param>
        /// <param name="value">ֵ</param>
        /// <returns></returns>
        [UnitOfWork]
        public virtual async Task SetFeatureValueAsync(TTenant tenant, string featureName, string value)
        {
            //No need to change if it's already equals to the current value
            if (await GetFeatureValueOrNullAsync(tenant.Id, featureName) == value)
            {
                return;
            }

            //Get the current feature setting
            var currentSetting = await TenantFeatureRepository.FirstOrDefaultAsync(f => f.TenantId == tenant.Id && f.Name == featureName);

            //Get the feature
            var feature = FeatureManager.GetOrNull(featureName);
            if (feature == null)
            {
                if (currentSetting != null)
                {
                    await TenantFeatureRepository.DeleteAsync(currentSetting);
                }

                return;
            }

            //Determine default value
            var defaultValue = tenant.EditionId.HasValue
                ? (await EditionManager.GetFeatureValueOrNullAsync(tenant.EditionId.Value, featureName) ?? feature.DefaultValue)
                : feature.DefaultValue;

            //No need to store value if it's default
            if (value == defaultValue)
            {
                if (currentSetting != null)
                {
                    await TenantFeatureRepository.DeleteAsync(currentSetting);
                }

                return;
            }

            //Insert/update the feature value
            if (currentSetting == null)
            {
                await TenantFeatureRepository.InsertAsync(new TenantFeatureSetting(tenant.Id, featureName, value));
            }
            else
            {
                currentSetting.Value = value;
            }
        }

        /// <summary>
        /// Resets all custom feature settings for a tenant.Tenant will have features according to it's edition.
        /// Ϊ�̻����������Զ��幦�ܡ��̻���ӵ����汾���ص�
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        public async Task ResetAllFeaturesAsync(int tenantId)
        {
            await TenantFeatureRepository.DeleteAsync(f => f.TenantId == tenantId);
        }
        /// <summary>
        /// ��֤�̻�
        /// </summary>
        /// <param name="tenant">�̻�����</param>
        /// <returns></returns>
        protected virtual async Task<IdentityResult> ValidateTenantAsync(TTenant tenant)
        {
            var nameValidationResult = await ValidateTenancyNameAsync(tenant.TenancyName);
            if (!nameValidationResult.Succeeded)
            {
                return nameValidationResult;
            }

            return IdentityResult.Success;
        }
        /// <summary>
        /// �����̻�������֤�̻�
        /// </summary>
        /// <param name="tenancyName">�̻���</param>
        /// <returns></returns>
        protected virtual async Task<IdentityResult> ValidateTenancyNameAsync(string tenancyName)
        {
            if (!Regex.IsMatch(tenancyName, AbpTenant<TUser>.TenancyNameRegex))
            {
                return AbpIdentityResult.Failed(L("InvalidTenancyName"));
            }

            return IdentityResult.Success;
        }
        /// <summary>
        /// ��ȡ���ػ��ַ���
        /// </summary>
        /// <param name="name">����</param>
        /// <returns></returns>
        private string L(string name)
        {
            return LocalizationManager.GetString(AbpZeroConsts.LocalizationSourceName, name);
        }
        /// <summary>
        /// �����̻��޸��¼�
        /// </summary>
        /// <param name="eventData">�¼�����</param>
        public void HandleEvent(EntityChangedEventData<TTenant> eventData)
        {
            if (eventData.Entity.IsTransient())
            {
                return;
            }

            CacheManager.GetTenantFeatureCache().Remove(eventData.Entity.Id);
        }
        /// <summary>
        /// �����̻�ɾ���¼�
        /// </summary>
        /// <param name="eventData">�¼�����</param>
        [UnitOfWork]
        public virtual void HandleEvent(EntityDeletedEventData<Edition> eventData)
        {
            var relatedTenants = TenantRepository.GetAllList(t => t.EditionId == eventData.Entity.Id);
            foreach (var relatedTenant in relatedTenants)
            {
                relatedTenant.EditionId = null;
            }
        }
    }
}