using System;
using System.Collections.Generic;

namespace Abp.Web.Api.Modeling
{
    /// <summary>
    /// API��������ģ��
    /// </summary>
    [Serializable]
    public class ActionApiDescriptionModel
    {
        /// <summary>
        /// ��������
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Http����ʽ
        /// </summary>
        public string HttpMethod { get; }

        /// <summary>
        /// ����Url
        /// </summary>
        public string Url { get; }

        public IList<ParameterApiDescriptionModel> Parameters { get; }

        public ReturnValueApiDescriptionModel ReturnValue { get; }

        private ActionApiDescriptionModel()
        {

        }

        public ActionApiDescriptionModel(string name, ReturnValueApiDescriptionModel returnValue, string url, string httpMethod = null)
        {
            Name = name;
            ReturnValue = returnValue;
            Url = url;
            HttpMethod = httpMethod;

            Parameters = new List<ParameterApiDescriptionModel>();
        }

        public ParameterApiDescriptionModel AddParameter(ParameterApiDescriptionModel parameter)
        {
            Parameters.Add(parameter);
            return parameter;
        }
    }
}