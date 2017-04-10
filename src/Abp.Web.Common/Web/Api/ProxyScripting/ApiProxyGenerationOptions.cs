using System.Collections.Generic;
using Abp.Collections.Extensions;

namespace Abp.Web.Api.ProxyScripting
{
    /// <summary>
    /// API��������ѡ��
    /// </summary>
    public class ApiProxyGenerationOptions
    {
        /// <summary>
        /// ��������
        /// </summary>
        public string GeneratorType { get; set; }

        /// <summary>
        /// �û�����
        /// </summary>
        public bool UseCache { get; set; }

        /// <summary>
        /// ģ���б�
        /// </summary>
        public string[] Modules { get; set; }

        /// <summary>
        /// ����������
        /// </summary>
        public string[] Controllers { get; set; }

        /// <summary>
        /// Action����
        /// </summary>
        public string[] Actions { get; set; }

        /// <summary>
        /// ���Լ���
        /// </summary>
        public IDictionary<string, string> Properties { get; set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="generatorType">��������</param>
        /// <param name="useCache"></param>
        public ApiProxyGenerationOptions(string generatorType, bool useCache = true)
        {
            GeneratorType = generatorType;
            UseCache = useCache;

            Properties = new Dictionary<string, string>();
        }

        /// <summary>
        /// �Ƿ��ǲ�������
        /// </summary>
        /// <returns></returns>
        public bool IsPartialRequest()
        {
            return !(Modules.IsNullOrEmpty() && Controllers.IsNullOrEmpty() && Actions.IsNullOrEmpty());
        }
    }
}