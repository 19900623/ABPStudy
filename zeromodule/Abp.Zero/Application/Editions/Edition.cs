using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.MultiTenancy;

namespace Abp.Application.Editions
{
    /// <summary>
    /// Represents an edition of the application.
    /// ��ʾӦ�ó���İ汾
    /// </summary>
    [Table("AbpEditions")]
    [MultiTenancySide(MultiTenancySides.Host)]
    public class Edition : FullAuditedEntity
    {
        /// <summary>
        /// <see cref="Name"/>���Ե���󳤶�
        /// </summary>
        public const int MaxNameLength = 32;

        /// <summary>
        /// <see cref="DisplayName"/>���Ե���󳤶� 
        /// </summary>
        public const int MaxDisplayNameLength = 64;

        /// <summary>
        /// Unique name of this edition.
        /// �˰汾��Ψһ����
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Display name of this edition.
        /// �˰汾����ʾ����
        /// </summary>
        [Required]
        [StringLength(MaxDisplayNameLength)]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        public Edition()
        {
            Name = Guid.NewGuid().ToString("N");
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="displayName"></param>
        public Edition(string displayName)
            : this()
        {
            DisplayName = displayName;
        }
    }
}