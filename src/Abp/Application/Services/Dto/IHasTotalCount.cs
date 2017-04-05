namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to set "Total Count of Items" to a DTO.
    /// �˽ӿ�ΪDTO���塰��������
    /// </summary>
    public interface IHasTotalCount
    {
        /// <summary>
        /// Total count of Items.
        /// ������
        /// </summary>
        int TotalCount { get; set; }
    }
}