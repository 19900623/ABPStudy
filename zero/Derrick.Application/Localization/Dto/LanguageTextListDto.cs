using Abp.Application.Services.Dto;

namespace Derrick.Localization.Dto
{
    /// <summary>
    /// �����ı��б�Dto
    /// </summary>
    public class LanguageTextListDto
    {
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// ����ֵ
        /// </summary>
        public string BaseValue { get; set; }
        /// <summary>
        /// Ŀ��ֵ
        /// </summary>
        public string TargetValue { get; set; }
    }
}