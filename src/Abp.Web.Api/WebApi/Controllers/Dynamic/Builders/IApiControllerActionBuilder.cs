using System.Reflection;
using Abp.Web;
using System.Web.Http.Filters;

namespace Abp.WebApi.Controllers.Dynamic.Builders
{
    /// <summary>
    /// �˽ӿ����ڶ��嶯̬API ������action
    /// </summary>
    public interface IApiControllerActionBuilder
    {
        /// <summary>
        /// The controller builder related to this action.
        /// �뵱ǰAction��صĿ�����������
        /// </summary>
        IApiControllerBuilder Controller { get; }

        /// <summary>
        /// Gets name of the action.
        /// ��ȡAction������
        /// </summary>
        string ActionName { get; }

        /// <summary>
        /// Gets the action method.
        /// ��ȡAction������Ϣ
        /// </summary>
        MethodInfo Method { get; }

        /// <summary>
        /// Gets current HttpVerb setting.
        /// ��ȡ��ǰHttp��������
        /// </summary>
        HttpVerb? Verb { get; set; }

        /// <summary>
        /// Is API Explorer enabled.
        /// �Ƿ���API��� 
        /// </summary>
        bool? IsApiExplorerEnabled { get; set; }

        /// <summary>
        /// Gets current filters.
        /// ��ȡ��ǰ������
        /// </summary>
        IFilter[] Filters { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to create action for this method.
        /// ��ȡ������һ��ֵ����ֵָʾ�Ƿ�Ϊ�÷�������Action��
        /// </summary>
        bool DontCreate { get; set; }
    }

    /// <summary>
    /// This interface is used to define a dynamic api controller action.
    /// �˽ӿ����ڶ��嶯̬API ������action
    /// </summary>
    /// <typeparam name="T">Type of the proxied object</typeparam>
    public interface IApiControllerActionBuilder<T>: IApiControllerActionBuilder
    {
        /// <summary>
        /// Used to specify Http verb of the action.
        /// </summary>
        /// <param name="verb">Http very</param>
        /// <returns>Action builder</returns>
        IApiControllerActionBuilder<T> WithVerb(HttpVerb verb);

        /// <summary>
        /// Enables/Disables API Explorer for the action.
        /// </summary>
        IApiControllerActionBuilder<T> WithApiExplorer(bool isEnabled);

        /// <summary>
        /// Used to specify another method definition.
        /// </summary>
        /// <param name="methodName">Name of the method in proxied type</param>
        /// <returns>Action builder</returns>
        IApiControllerActionBuilder<T> ForMethod(string methodName);

        /// <summary>
        /// Tells builder to not create action for this method.
        /// </summary>
        /// <returns>Controller builder</returns>
        IApiControllerBuilder<T> DontCreateAction();

        /// <summary>
        /// Builds the controller.
        /// This method must be called at last of the build operation.
        /// </summary>
        void Build();

        /// <summary>
        /// Used to add action filters to apply to this action.
        /// </summary>
        /// <param name="filters"> Action Filters to apply.</param>
        IApiControllerActionBuilder<T> WithFilters(params IFilter[] filters);
    }
}