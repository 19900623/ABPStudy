namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to request a sorted result.
    /// ���ӿڶ�������һ������Ľ����
    /// </summary>
    public interface ISortedResultRequest
    {
        /// <summary>
        /// Sorting information.Should include sorting field and optionally a direction (ASC or DESC)Can contain more than one field separated by comma (,).
        /// ������Ϣ.Ӧ��ָʾ������ֶκͷ���ASC ���� DESC���ܰ�����������ֶΣ�ʹ�ö��ţ�,)�ָ�
        /// </summary>
        /// <example>
        /// ����:
        /// "Name"
        /// "Name DESC"
        /// "Name ASC, Age DESC"
        /// </example>
        string Sorting { get; set; }
    }
}