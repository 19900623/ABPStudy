using System;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// Simply implements <see cref="IPagedAndSortedResultRequest"/>.
    /// <see cref="IPagedAndSortedResultRequest"/>��ʵ��
    /// </summary>
    [Serializable]
    public class PagedAndSortedResultRequestDto : PagedResultRequestDto, IPagedAndSortedResultRequest
    {
        /// <summary>
        /// �����ַ���
        /// </summary>
        public virtual string Sorting { get; set; }
    }
}