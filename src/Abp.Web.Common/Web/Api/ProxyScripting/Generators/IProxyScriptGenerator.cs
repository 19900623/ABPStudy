using Abp.Web.Api.Modeling;

namespace Abp.Web.Api.ProxyScripting.Generators
{
    /// <summary>
    /// ����ű�������
    /// </summary>
    public interface IProxyScriptGenerator
    {
        /// <summary>
        /// �����ű�
        /// </summary>
        /// <param name="model">APIӦ�ó�������ģ��</param>
        /// <returns></returns>
        string CreateScript(ApplicationApiDescriptionModel model);
    }
}