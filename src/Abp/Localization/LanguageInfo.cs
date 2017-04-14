namespace Abp.Localization
{
    /// <summary>
    /// Represents an available language for the application.
    /// ��ʾӦ�ó���Ŀ�������
    /// </summary>
    public class LanguageInfo
    {
        /// <summary>
        /// Code name of the language.It should be valid culture code.
        /// ����Code,Ӧ������Ч���������
        /// Ex: "en-US" for American English, "tr-TR" for Turkey Turkish.
        /// ����:"en-US"��������Ӣ�"tr-TR"������������
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display name of the language in it's original language.
        /// ������ԭʼ��������ʾ���Ե�����
        /// Ex: "English" for English, "T�rk�e" for Turkish.
        /// ����:"English"����Ӣ��
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// An icon can be set to display on the UI.
        /// ��UI����ʾ��ͼ��
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Is this the default language?
        /// �Ƿ���Ĭ������
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Creates a new <see cref="LanguageInfo"/> object.
        /// ���캯��
        /// </summary>
        /// <param name="name">
        /// Code name of the language.It should be valid culture code.
        /// ����Code,Ӧ������Ч���������
        /// Ex: "en-US" for American English, "tr-TR" for Turkey Turkish.
        /// ����:"en-US"��������Ӣ�"tr-TR"������������
        /// </param>
        /// <param name="displayName">
        /// Display name of the language in it's original language.
        /// ������ԭʼ��������ʾ���Ե�����
        /// Ex: "English" for English, "T�rk�e" for Turkish.
        /// ����:"English"����Ӣ��
        /// </param>
        /// <param name="icon">An icon can be set to display on the UI / ��UI����ʾ��ͼ��</param>
        /// <param name="isDefault">Is this the default language? / �Ƿ���Ĭ������</param>
        public LanguageInfo(string name, string displayName, string icon = null, bool isDefault = false)
        {
            Name = name;
            DisplayName = displayName;
            Icon = icon;
            IsDefault = isDefault;
        }
    }
}