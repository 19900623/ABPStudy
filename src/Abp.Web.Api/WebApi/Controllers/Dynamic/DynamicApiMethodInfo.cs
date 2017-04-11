using System.Reflection;
using System.Web.Http.Filters;
using Abp.Web;

namespace Abp.WebApi.Controllers.Dynamic
{
    /// <summary>
    /// Used to store an action information of a dynamic ApiController.
    /// ���ڴ洢��̬API�������е�Action��Ϣ
    /// </summary>
    public class DynamicApiActionInfo
    {
        /// <summary>
        /// Name of the action in the controller.
        /// ��������Action������
        /// </summary>
        public string ActionName { get; private set; }

        /// <summary>
        /// The method which will be invoked when this action is called.
        /// ���ô˲���ʱ�����õķ���
        /// </summary>
        public MethodInfo Method { get; private set; }

        /// <summary>
        /// The HTTP verb that is used to call this action.
        /// ���ô˷�����Http����
        /// </summary>
        public HttpVerb Verb { get; private set; }

        /// <summary>
        /// Dynamic Action Filters for this Controller Action.
        /// �˿�����Action�Ķ�̬Action������
        /// </summary>
        public IFilter[] Filters { get; set; }

        /// <summary>
        /// Is API Explorer enabled.
        /// �Ƿ�����APIԤ��
        /// </summary>
        public bool? IsApiExplorerEnabled { get; set; }

        /// <summary>
        /// Createa a new <see cref="DynamicApiActionInfo"/> object.
        /// ���캯��
        /// </summary>
        /// <param name="actionName">Name of the action in the controller / ��������Action������</param>
        /// <param name="verb">The HTTP verb that is used to call this action / ���ô˷�����Http����</param>
        /// <param name="method">The method which will be invoked when this action is called / ���ô˲���ʱ�����õķ���</param>
        /// <param name="filters">Filters / �˿�����Action�Ķ�̬Action������</param>
        /// <param name="isApiExplorerEnabled">Is API explorer enabled / �Ƿ�����APIԤ��</param>
        public DynamicApiActionInfo(
            string actionName, 
            HttpVerb verb, 
            MethodInfo method, 
            IFilter[] filters = null,
            bool? isApiExplorerEnabled = null)
        {
            ActionName = actionName;
            Verb = verb;
            Method = method;
            IsApiExplorerEnabled = isApiExplorerEnabled;
            Filters = filters ?? new IFilter[] { }; //Assigning or initialzing the action filters.
        }
    }
}