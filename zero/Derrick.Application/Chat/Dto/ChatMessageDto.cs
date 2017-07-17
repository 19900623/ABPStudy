using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Derrick.Chat.Dto
{
    /// <summary>
    /// ������ϢDto
    /// </summary>
    [AutoMapFrom(typeof(ChatMessage))]
    public class ChatMessageDto : EntityDto
    {
        /// <summary>
        /// �û�ID
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// �̻�ID
        /// </summary>
        public int? TenantId { get; set; }
        /// <summary>
        /// Ŀ���û�ID
        /// </summary>
        public long TargetUserId { get; set; }
        /// <summary>
        /// Ŀ���̻�ID
        /// </summary>
        public int? TargetTenantId { get; set; }
        /// <summary>
        /// ����һ��
        /// </summary>
        public ChatSide Side { get; set; }
        /// <summary>
        /// ������Ϣ��ȡ״̬
        /// </summary>
        public ChatMessageReadState ReadState { get; set; }
        /// <summary>
        /// ��Ϣ
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreationTime { get; set; }

    }
}