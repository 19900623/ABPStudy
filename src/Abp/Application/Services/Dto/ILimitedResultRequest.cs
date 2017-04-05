namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to request a limited result.
    /// �˽ӿڶ�������һ�����޽����
    /// </summary>
    public interface ILimitedResultRequest
    {
        /// <summary>
        /// Max expected result count.
        /// ��������������
        /// </summary>
        int MaxResultCount { get; set; }
    }
}