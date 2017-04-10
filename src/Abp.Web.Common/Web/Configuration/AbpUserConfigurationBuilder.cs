using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Abp.Application.Features;
using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.Timing.Timezone;
using Abp.Web.Models.AbpUserConfiguration;
using Abp.Web.Security.AntiForgery;
using System.Linq;
using Abp.Dependency;
using Abp.Extensions;

namespace Abp.Web.Configuration
{
    /// <summary>
    /// ABP�û����ù�����
    /// </summary>
    public class AbpUserConfigurationBuilder : ITransientDependency
    {
        /// <summary>
        /// ���⻧����
        /// </summary>
        private readonly IMultiTenancyConfig _multiTenancyConfig;

        /// <summary>
        /// ���Թ�����
        /// </summary>
        private readonly ILanguageManager _languageManager;

        /// <summary>
        /// ���ػ�������
        /// </summary>
        private readonly ILocalizationManager _localizationManager;

        /// <summary>
        /// ���ܹ�����
        /// </summary>
        private readonly IFeatureManager _featureManager;

        /// <summary>
        /// ���ܼ����
        /// </summary>
        private readonly IFeatureChecker _featureChecker;

        /// <summary>
        /// Ȩ�޹�����
        /// </summary>
        private readonly IPermissionManager _permissionManager;

        /// <summary>
        /// �û�����������
        /// </summary>
        private readonly IUserNavigationManager _userNavigationManager;

        /// <summary>
        /// ���ö��������
        /// </summary>
        private readonly ISettingDefinitionManager _settingDefinitionManager;

        /// <summary>
        /// ���ù�����
        /// </summary>
        private readonly ISettingManager _settingManager;

        /// <summary>
        /// ABP��α����
        /// </summary>
        private readonly IAbpAntiForgeryConfiguration _abpAntiForgeryConfiguration;

        /// <summary>
        /// ABP Session
        /// </summary>
        private readonly IAbpSession _abpSession;

        /// <summary>
        /// Ȩ�޼����
        /// </summary>
        private readonly IPermissionChecker _permissionChecker;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="multiTenancyConfig">���⻧����</param>
        /// <param name="languageManager">���Թ�����</param>
        /// <param name="localizationManager">���ػ�������</param>
        /// <param name="featureManager">���ܹ�����</param>
        /// <param name="featureChecker">���ܼ����</param>
        /// <param name="permissionManager">Ȩ�޹�����</param>
        /// <param name="userNavigationManager">�û�����������</param>
        /// <param name="settingDefinitionManager">���ö��������</param>
        /// <param name="settingManager">���ù�����</param>
        /// <param name="abpAntiForgeryConfiguration">ABP��α����</param>
        /// <param name="abpSession">ABP Session</param>
        /// <param name="permissionChecker">Ȩ�޼����</param>
        public AbpUserConfigurationBuilder(
            IMultiTenancyConfig multiTenancyConfig,
            ILanguageManager languageManager,
            ILocalizationManager localizationManager,
            IFeatureManager featureManager,
            IFeatureChecker featureChecker,
            IPermissionManager permissionManager,
            IUserNavigationManager userNavigationManager,
            ISettingDefinitionManager settingDefinitionManager,
            ISettingManager settingManager,
            IAbpAntiForgeryConfiguration abpAntiForgeryConfiguration,
            IAbpSession abpSession,
            IPermissionChecker permissionChecker)
        {
            _multiTenancyConfig = multiTenancyConfig;
            _languageManager = languageManager;
            _localizationManager = localizationManager;
            _featureManager = featureManager;
            _featureChecker = featureChecker;
            _permissionManager = permissionManager;
            _userNavigationManager = userNavigationManager;
            _settingDefinitionManager = settingDefinitionManager;
            _settingManager = settingManager;
            _abpAntiForgeryConfiguration = abpAntiForgeryConfiguration;
            _abpSession = abpSession;
            _permissionChecker = permissionChecker;
        }
        
        /// <summary>
        /// ��ȡ�û���������
        /// </summary>
        /// <returns></returns>
        public async Task<AbpUserConfigurationDto> GetAll()
        {
            return new AbpUserConfigurationDto
            {
                MultiTenancy = GetUserMultiTenancyConfig(),
                Session = GetUserSessionConfig(),
                Localization = GetUserLocalizationConfig(),
                Features = await GetUserFeaturesConfig(),
                Auth = await GetUserAuthConfig(),
                Nav = await GetUserNavConfig(),
                Setting = await GetUserSettingConfig(),
                Clock = GetUserClockConfig(),
                Timing = await GetUserTimingConfig(),
                Security = GetUserSecurityConfig()
            };
        }

        /// <summary>
        /// ��ȡ�û����⻧����
        /// </summary>
        /// <returns></returns>
        private AbpMultiTenancyConfigDto GetUserMultiTenancyConfig()
        {
            return new AbpMultiTenancyConfigDto
            {
                IsEnabled = _multiTenancyConfig.IsEnabled
            };
        }

        /// <summary>
        /// ��ȡ�û�Session����
        /// </summary>
        /// <returns></returns>
        private AbpUserSessionConfigDto GetUserSessionConfig()
        {
            return new AbpUserSessionConfigDto
            {
                UserId = _abpSession.UserId,
                TenantId = _abpSession.TenantId,
                ImpersonatorUserId = _abpSession.ImpersonatorUserId,
                ImpersonatorTenantId = _abpSession.ImpersonatorTenantId,
                MultiTenancySide = _abpSession.MultiTenancySide
            };
        }

        /// <summary>
        /// ��ȡ�û����ػ�����
        /// </summary>
        /// <returns></returns>
        private AbpUserLocalizationConfigDto GetUserLocalizationConfig()
        {
            var currentCulture = Thread.CurrentThread.CurrentUICulture;
            var languages = _languageManager.GetLanguages();

            var config = new AbpUserLocalizationConfigDto
            {
                CurrentCulture = new AbpUserCurrentCultureConfigDto
                {
                    Name = currentCulture.Name,
                    DisplayName = currentCulture.DisplayName
                },
                Languages = languages.ToList()
            };

            if (languages.Count > 0)
            {
                config.CurrentLanguage = _languageManager.CurrentLanguage;
            }

            var sources = _localizationManager.GetAllSources().OrderBy(s => s.Name).ToArray();
            config.Sources = sources.Select(s => new AbpLocalizationSourceDto
            {
                Name = s.Name,
                Type = s.GetType().Name
            }).ToList();

            config.Values = new Dictionary<string, Dictionary<string, string>>();
            foreach (var source in sources)
            {
                var stringValues = source.GetAllStrings(currentCulture).OrderBy(s => s.Name).ToList();
                var stringDictionary = stringValues
                    .ToDictionary(_ => _.Name, _ => _.Value);
                config.Values.Add(source.Name, stringDictionary);
            }

            return config;
        }

        /// <summary>
        /// ��ȡ�û���������
        /// </summary>
        /// <returns></returns>
        private async Task<AbpUserFeatureConfigDto> GetUserFeaturesConfig()
        {
            var config = new AbpUserFeatureConfigDto()
            {
                AllFeatures = new Dictionary<string, AbpStringValueDto>()
            };

            var allFeatures = _featureManager.GetAll().ToList();

            if (_abpSession.TenantId.HasValue)
            {
                var currentTenantId = _abpSession.GetTenantId();
                foreach (var feature in allFeatures)
                {
                    var value = await _featureChecker.GetValueAsync(currentTenantId, feature.Name);
                    config.AllFeatures.Add(feature.Name, new AbpStringValueDto
                    {
                        Value = value
                    });
                }
            }
            else
            {
                foreach (var feature in allFeatures)
                {
                    config.AllFeatures.Add(feature.Name, new AbpStringValueDto
                    {
                        Value = feature.DefaultValue
                    });
                }
            }

            return config;
        }

        /// <summary>
        /// ��ȡ�û���֤����
        /// </summary>
        /// <returns></returns>
        private async Task<AbpUserAuthConfigDto> GetUserAuthConfig()
        {
            var config = new AbpUserAuthConfigDto();

            var allPermissionNames = _permissionManager.GetAllPermissions(false).Select(p => p.Name).ToList();
            var grantedPermissionNames = new List<string>();

            if (_abpSession.UserId.HasValue)
            {
                foreach (var permissionName in allPermissionNames)
                {
                    if (await _permissionChecker.IsGrantedAsync(permissionName))
                    {
                        grantedPermissionNames.Add(permissionName);
                    }
                }
            }

            config.AllPermissions = allPermissionNames.ToDictionary(permissionName => permissionName, permissionName => "true");
            config.GrantedPermissions = grantedPermissionNames.ToDictionary(permissionName => permissionName, permissionName => "true");

            return config;
        }

        /// <summary>
        /// ��ȡ�û���������
        /// </summary>
        /// <returns></returns>
        private async Task<AbpUserNavConfigDto> GetUserNavConfig()
        {
            var userMenus = await _userNavigationManager.GetMenusAsync(_abpSession.ToUserIdentifier());
            return new AbpUserNavConfigDto
            {
                Menus = userMenus.ToDictionary(userMenu => userMenu.Name, userMenu => userMenu)
            };
        }

        /// <summary>
        /// ��ȡ�û���������
        /// </summary>
        /// <returns></returns>
        private async Task<AbpUserSettingConfigDto> GetUserSettingConfig()
        {
            var config = new AbpUserSettingConfigDto
            {
                Values = new Dictionary<string, string>()
            };

            var settingDefinitions = _settingDefinitionManager
                .GetAllSettingDefinitions()
                .Where(sd => sd.IsVisibleToClients);

            foreach (var settingDefinition in settingDefinitions)
            {
                var settingValue = await _settingManager.GetSettingValueAsync(settingDefinition.Name);
                config.Values.Add(settingDefinition.Name, settingValue);
            }

            return config;
        }

        /// <summary>
        /// ��ȡ�û�ʱ������
        /// </summary>
        /// <returns></returns>
        private AbpUserClockConfigDto GetUserClockConfig()
        {
            return new AbpUserClockConfigDto
            {
                Provider = Clock.Provider.GetType().Name.ToCamelCase()
            };
        }

        /// <summary>
        /// ��ȡ�û���ʱ����
        /// </summary>
        /// <returns></returns>
        private async Task<AbpUserTimingConfigDto> GetUserTimingConfig()
        {
            var timezoneId = await _settingManager.GetSettingValueAsync(TimingSettingNames.TimeZone);
            var timezone = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);

            return new AbpUserTimingConfigDto
            {
                TimeZoneInfo = new AbpUserTimeZoneConfigDto
                {
                    Windows = new AbpUserWindowsTimeZoneConfigDto
                    {
                        TimeZoneId = timezoneId,
                        BaseUtcOffsetInMilliseconds = timezone.BaseUtcOffset.TotalMilliseconds,
                        CurrentUtcOffsetInMilliseconds = timezone.GetUtcOffset(Clock.Now).TotalMilliseconds,
                        IsDaylightSavingTimeNow = timezone.IsDaylightSavingTime(Clock.Now)
                    },
                    Iana = new AbpUserIanaTimeZoneConfigDto
                    {
                        TimeZoneId = TimezoneHelper.WindowsToIana(timezoneId)
                    }
                }
            };
        }

        /// <summary>
        /// ��ȡ�û���ȫ����
        /// </summary>
        /// <returns></returns>
        private AbpUserSecurityConfigDto GetUserSecurityConfig()
        {
            return new AbpUserSecurityConfigDto()
            {
                AntiForgery = new AbpUserAntiForgeryConfigDto
                {
                    TokenCookieName = _abpAntiForgeryConfiguration.TokenCookieName,
                    TokenHeaderName = _abpAntiForgeryConfiguration.TokenHeaderName
                }
            };
        }
    }
}