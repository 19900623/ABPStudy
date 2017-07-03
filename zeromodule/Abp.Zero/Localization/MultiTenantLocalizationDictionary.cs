using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Localization.Dictionaries;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;

namespace Abp.Localization
{
    /// <summary>
    /// <see cref="IMultiTenantLocalizationDictionary"/>��ʵ��
    /// </summary>
    public class MultiTenantLocalizationDictionary :
        IMultiTenantLocalizationDictionary
    {
        /// <summary>
        /// Դ����
        /// </summary>
        private readonly string _sourceName;
        /// <summary>
        /// ���ػ��ַ������ֵ�
        /// </summary>
        private readonly ILocalizationDictionary _internalDictionary;
        /// <summary>
        /// �Զ��屾�ػ��ִ�
        /// </summary>
        private readonly IRepository<ApplicationLanguageText, long> _customLocalizationRepository;
        /// <summary>
        /// �����������
        /// </summary>
        private readonly ICacheManager _cacheManager;
        /// <summary>
        /// Abp Session����
        /// </summary>
        private readonly IAbpSession _session;
        /// <summary>
        /// ������Ԫ����
        /// </summary>
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// ���캯��
        /// </summary>
        public MultiTenantLocalizationDictionary(
            string sourceName,
            ILocalizationDictionary internalDictionary,
            IRepository<ApplicationLanguageText, long> customLocalizationRepository,
            ICacheManager cacheManager,
            IAbpSession session,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _sourceName = sourceName;
            _internalDictionary = internalDictionary;
            _customLocalizationRepository = customLocalizationRepository;
            _cacheManager = cacheManager;
            _session = session;
            _unitOfWorkManager = unitOfWorkManager;
        }
        /// <summary>
        /// �����Ļ���Ϣ
        /// </summary>
        public CultureInfo CultureInfo { get { return _internalDictionary.CultureInfo; } }
        /// <summary>
        /// Name����
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string name]
        {
            get { return _internalDictionary[name]; }
            set { _internalDictionary[name] = value; }
        }
        /// <summary>
        /// ��ȡ�ַ�����Null
        /// </summary>
        /// <param name="name">����</param>
        /// <returns></returns>
        public LocalizedString GetOrNull(string name)
        {
            return GetOrNull(_session.TenantId, name);
        }
        /// <summary>
        /// ��ȡ�ַ�����Null
        /// </summary>
        /// <param name="tenantId">�̻�ID</param>
        /// <param name="name">����</param>
        /// <returns></returns>
        public LocalizedString GetOrNull(int? tenantId, string name)
        {
            //Get cache
            var cache = _cacheManager.GetMultiTenantLocalizationDictionaryCache();

            //Get for current tenant
            var dictionary = cache.Get(CalculateCacheKey(tenantId), () => GetAllValuesFromDatabase(tenantId));
            var value = dictionary.GetOrDefault(name);
            if (value != null)
            {
                return new LocalizedString(name, value, CultureInfo);
            }

            //Fall back to host
            if (tenantId != null)
            {
                dictionary = cache.Get(CalculateCacheKey(null), () => GetAllValuesFromDatabase(null));
                value = dictionary.GetOrDefault(name);
                if (value != null)
                {
                    return new LocalizedString(name, value, CultureInfo);
                }
            }

            //Not found in database, fall back to internal dictionary
            var internalLocalizedString = _internalDictionary.GetOrNull(name);
            if (internalLocalizedString != null)
            {
                return internalLocalizedString;
            }

            //Not found at all
            return null;
        }
        /// <summary>
        /// ��ȡ�����ַ���
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<LocalizedString> GetAllStrings()
        {
            return GetAllStrings(_session.TenantId);
        }
        /// <summary>
        /// ��ȡָ���̻��������ַ���
        /// </summary>
        /// <param name="tenantId">�̻�ID</param>
        /// <returns></returns>
        public IReadOnlyList<LocalizedString> GetAllStrings(int? tenantId)
        {
            //Get cache
            var cache = _cacheManager.GetMultiTenantLocalizationDictionaryCache();

            //Create a temp dictionary to build (by underlying dictionary)
            var dictionary = new Dictionary<string, LocalizedString>();

            foreach (var localizedString in _internalDictionary.GetAllStrings())
            {
                dictionary[localizedString.Name] = localizedString;
            }

            //Override by host
            if (tenantId != null)
            {
                var defaultDictionary = cache.Get(CalculateCacheKey(null), () => GetAllValuesFromDatabase(null));
                foreach (var keyValue in defaultDictionary)
                {
                    dictionary[keyValue.Key] = new LocalizedString(keyValue.Key, keyValue.Value, CultureInfo);
                }
            }

            //Override by tenant
            var tenantDictionary = cache.Get(CalculateCacheKey(tenantId), () => GetAllValuesFromDatabase(tenantId));
            foreach (var keyValue in tenantDictionary)
            {
                dictionary[keyValue.Key] = new LocalizedString(keyValue.Key, keyValue.Value, CultureInfo);
            }

            return dictionary.Values.ToImmutableList();
        }
        /// <summary>
        /// ���򻺴�Key
        /// </summary>
        /// <param name="tenantId">�̻�ID</param>
        /// <returns></returns>
        private string CalculateCacheKey(int? tenantId)
        {
            return MultiTenantLocalizationDictionaryCacheHelper.CalculateCacheKey(tenantId, _sourceName, CultureInfo.Name);
        }

        /// <summary>
        /// �����ݿ��ȡ����ֵ
        /// </summary>
        /// <param name="tenantId">�̻�ID</param>
        /// <returns></returns>
        [UnitOfWork]
        protected virtual Dictionary<string, string> GetAllValuesFromDatabase(int? tenantId)
        {
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                return _customLocalizationRepository
                    .GetAllList(l => l.Source == _sourceName && l.LanguageName == CultureInfo.Name)
                    .ToDictionary(l => l.Key, l => l.Value);
            }
        }
    }
}