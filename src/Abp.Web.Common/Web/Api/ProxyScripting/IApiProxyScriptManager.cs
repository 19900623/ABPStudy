namespace Abp.Web.Api.ProxyScripting
{
    /// <summary>
    /// API����ű�������
    /// </summary>
    public interface IApiProxyScriptManager
    {
        /// <summary>
        /// ��ȡAPI����ű�
        /// </summary>
        /// <param name="options">API��������ѡ��</param>
        /// <returns></returns>
        string GetScript(ApiProxyGenerationOptions options);
    }
}