using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace Derrick.Authorization.Users.Profile.Dto
{
    /// <summary>
    /// �����û�������ƬInput
    /// </summary>
    public class UpdateProfilePictureInput
    {
        /// <summary>
        /// �ļ�����
        /// </summary>
        [Required]
        [MaxLength(400)]
        public string FileName { get; set; }
        /// <summary>
        /// X����
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y����
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// �߶�
        /// </summary>
        public int Height { get; set; }
    }
}