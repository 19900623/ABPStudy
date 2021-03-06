﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Abp.Authorization.Users
{
    /// <summary>
    /// Used to store a User Login for external Login services.
    /// 用于存储外部登录服务的用户登录
    /// </summary>
    [Table("AbpUserLogins")]
    public class UserLogin : Entity<long>, IMayHaveTenant
    {
        /// <summary>
        /// <see cref="LoginProvider"/>属性的最大长度
        /// </summary>
        public const int MaxLoginProviderLength = 128;

        /// <summary>
        /// <see cref="ProviderKey"/>属性的最大长度
        /// </summary>
        public const int MaxProviderKeyLength = 256;
        /// <summary>
        /// 商户ID
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// 登录提供者
        /// </summary>
        [Required]
        [MaxLength(MaxLoginProviderLength)]
        public virtual string LoginProvider { get; set; }

        /// <summary>
        /// <see cref="LoginProvider"/>中的key
        /// </summary>
        [Required]
        [MaxLength(MaxProviderKeyLength)]
        public virtual string ProviderKey { get; set; }
    }
}
