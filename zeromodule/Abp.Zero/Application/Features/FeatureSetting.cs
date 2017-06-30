using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.MultiTenancy;

namespace Abp.Application.Features
{
    /// <summary>
    /// ���õĻ���
    /// </summary>
    [Table("AbpFeatures")]
    [MultiTenancySide(MultiTenancySides.Host)]
    public abstract class FeatureSetting : CreationAuditedEntity<long>
    {
        /// <summary>
        /// <see cref="Name"/>�ֶε���󳤶�
        /// </summary>
        public const int MaxNameLength = 128;

        /// <summary>
        /// <see cref="Value"/>���Ե���󳤶�
        /// </summary>
        public const int MaxValueLength = 2000;

        /// <summary>
        /// ��������
        /// </summary>
        [Required]
        [MaxLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// ����ֵ
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [MaxLength(MaxValueLength)]
        public virtual string Value { get; set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        protected FeatureSetting()
        {
            
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="value">����ֵ</param>
        protected FeatureSetting(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}