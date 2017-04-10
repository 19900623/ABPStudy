using System;

namespace Abp.Web.Models
{
    /// <summary>
    /// This class is used to create standard responses for AJAX/remote requests.
    /// ��������ΪAJAX/remote���󴴽���׼����Ӧ
    /// </summary>
    [Serializable]
    public class AjaxResponse : AjaxResponse<object>
    {
        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object.<see cref="AjaxResponseBase.Success"/> is set as true.
        /// ����һ��<see cref="AjaxResponse"/>����<see cref="AjaxResponseBase.Success"/>����Ϊtrue
        /// </summary>
        public AjaxResponse()
        {

        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponseBase.Success"/> specified.
        /// ʹ��ָ����<see cref="AjaxResponseBase.Success"/>����һ��<see cref="AjaxResponse"/>����
        /// </summary>
        /// <param name="success">Indicates success status of the result / ָʾ����ĳɹ�״̬</param>
        public AjaxResponse(bool success)
            : base(success)
        {

        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponse{TResult}.Result"/> specified.<see cref="AjaxResponseBase.Success"/> is set as true.
        /// ʹ��ָ����<see cref="AjaxResponse{TResult}.Result"/>����һ��<see cref="AjaxResponse"/>����<see cref="AjaxResponseBase.Success"/>����Ϊtrue
        /// </summary>
        /// <param name="result">The actual result object / ʵ�ʽ������</param>
        public AjaxResponse(object result)
            : base(result)
        {

        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponseBase.Error"/> specified.<see cref="AjaxResponseBase.Success"/> is set as false.
        /// ʹ��ָ����<see cref="AjaxResponseBase.Error"/>����һ��<see cref="AjaxResponse"/>����<see cref="AjaxResponseBase.Success"/>����Ϊfalse
        /// </summary>
        /// <param name="error">Error details / ��������</param>
        /// <param name="unAuthorizedRequest">Used to indicate that the current user has no privilege to perform this request / ����ָʾ��ǰ�û�û��Ȩ��ִ�д�����</param>
        public AjaxResponse(ErrorInfo error, bool unAuthorizedRequest = false)
            : base(error, unAuthorizedRequest)
        {

        }
    }
}