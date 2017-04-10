namespace Abp.Web.Api.Modeling
{
    /// <summary>
    /// API����ģ���ṩ��
    /// </summary>
    public interface IApiDescriptionModelProvider
    {
        /// <summary>
        /// Ӧ�ó���API����ģ��
        /// </summary>
        /// <returns></returns>
        ApplicationApiDescriptionModel CreateModel();
    }
}