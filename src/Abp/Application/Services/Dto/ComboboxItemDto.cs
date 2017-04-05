using System;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This DTO can be used as a simple item for a combobox/list.
    /// ��DTO������combobox/list��һ����
    /// </summary>
    [Serializable]
    public class ComboboxItemDto
    {
        /// <summary>
        /// Value of the item.
        /// ���Ӧ��ֵ
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Display text of the item.
        /// ���Ӧ����ʾ�ı�
        /// </summary>
        public string DisplayText { get; set; }

        /// <summary>
        /// Is selected?
        /// �Ƿ�ѡ��
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Creates a new <see cref="ComboboxItemDto"/>.
        /// ���캯��
        /// </summary>
        public ComboboxItemDto()
        {

        }

        /// <summary>
        /// Creates a new <see cref="ComboboxItemDto"/>.
        /// ���캯��
        /// </summary>
        /// <param name="value">Value of the item / ���Ӧ��ֵ</param>
        /// <param name="displayText">Display text of the item / ���Ӧ����ʾ�ı�</param>
        public ComboboxItemDto(string value, string displayText)
        {
            Value = value;
            DisplayText = displayText;
        }
    }
}