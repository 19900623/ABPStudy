using Abp.Configuration;
using Abp.Dependency;
using Abp.Extensions;
using Derrick.Configuration;

namespace Derrick.Web
{
    /// <summary>
    /// <see cref="IWebUrlService"/>ʵ�֣�վ��Url����
    /// </summary>
    public class WebUrlService : IWebUrlService, ITransientDependency
    {
        /// <summary>
        /// �̻�����ռλ��
        /// </summary>
        public const string TenancyNamePlaceHolder = "{TENANCY_NAME}";

        /// <summary>
        /// ���ù�����
        /// </summary>
        private readonly ISettingManager _settingManager;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="settingManager">���ù�����</param>
        public WebUrlService(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        /// <summary>
        /// ��ȡվ�����ַ
        /// </summary>
        /// <param name="tenancyName">�̻�����</param>
        /// <returns></returns>
        public string GetSiteRootAddress(string tenancyName = null)
        {
            var siteRootFormat = _settingManager.GetSettingValue(AppSettings.General.WebSiteRootAddress).EnsureEndsWith('/');

            if (!siteRootFormat.Contains(TenancyNamePlaceHolder))
            {
                return siteRootFormat;
            }

            if (siteRootFormat.Contains(TenancyNamePlaceHolder + "."))
            {
                siteRootFormat = siteRootFormat.Replace(TenancyNamePlaceHolder + ".", TenancyNamePlaceHolder);
            }

            if (tenancyName.IsNullOrEmpty())
            {
                return siteRootFormat.Replace(TenancyNamePlaceHolder, "");
            }

            return siteRootFormat.Replace(TenancyNamePlaceHolder, tenancyName + ".");
        }
    }
}