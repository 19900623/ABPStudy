using System.ComponentModel.DataAnnotations;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// Simply implements <see cref="ILimitedResultRequest"/>.
    /// <see cref="ILimitedResultRequest"/>��ʵ��
    /// </summary>
    public class LimitedResultRequestDto : ILimitedResultRequest
    {
        /// <summary>
        /// ��������������
        /// </summary>
        [Range(1, int.MaxValue)]
        public virtual int MaxResultCount { get; set; } = 10;
    }
}