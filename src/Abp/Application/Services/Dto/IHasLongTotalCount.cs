namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to set "Total Count of Items" to a DTO for long type.
    /// �˽ӿ�ΪDTO��׼������long���͵ġ���������
    /// </summary>
    public interface IHasLongTotalCount
    {
        /// <summary>
        /// Total count of Items.
        /// ������
        /// </summary>
        long TotalCount { get; set; }
    }
}