using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace Derrick.Localization.Dto
{
    /// <summary>
    /// ��ȡ����Output
    /// </summary>
    public class GetLanguagesOutput : ListResultDto<ApplicationLanguageListDto>
    {
        /// <summary>
        /// Ĭ����������
        /// </summary>
        public string DefaultLanguageName { get; set; }
        /// <summary>
        /// ���캯��
        /// </summary>
        public GetLanguagesOutput()
        {
            
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="items">Ӧ�ó��������б�Dto</param>
        /// <param name="defaultLanguageName">Ĭ����������</param>
        public GetLanguagesOutput(IReadOnlyList<ApplicationLanguageListDto> items, string defaultLanguageName)
            : base(items)
        {
            DefaultLanguageName = defaultLanguageName;
        }
    }
}