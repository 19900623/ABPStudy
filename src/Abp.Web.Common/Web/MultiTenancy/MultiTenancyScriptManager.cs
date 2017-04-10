using System;
using System.Globalization;
using System.Text;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Extensions;
using Abp.MultiTenancy;

namespace Abp.Web.MultiTenancy
{
    /// <summary>
    /// ���⻧�ű�������Ĭ��ʵ��
    /// </summary>
    public class MultiTenancyScriptManager : IMultiTenancyScriptManager, ITransientDependency
    {
        /// <summary>
        /// ���⻧����
        /// </summary>
        private readonly IMultiTenancyConfig _multiTenancyConfig;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="multiTenancyConfig">���⻧����</param>
        public MultiTenancyScriptManager(IMultiTenancyConfig multiTenancyConfig)
        {
            _multiTenancyConfig = multiTenancyConfig;
        }

        /// <summary>
        /// ��ȡ���⻧�ͻ��˽ű�
        /// </summary>
        /// <returns></returns>
        public string GetScript()
        {
            var script = new StringBuilder();

            script.AppendLine("(function(abp){");
            script.AppendLine();

            script.AppendLine("    abp.multiTenancy = abp.multiTenancy || {};");
            script.AppendLine("    abp.multiTenancy.isEnabled = " + _multiTenancyConfig.IsEnabled.ToString().ToLower(CultureInfo.InvariantCulture) + ";");

            script.AppendLine();
            script.Append("})(abp);");

            return script.ToString();
        }
    }
}