using System.Reflection;
using Abp.Web;
using System.Web.Http.Filters;
using System.Linq;
using Abp.Reflection;
using System.Web.Http;

namespace Abp.WebApi.Controllers.Dynamic.Builders
{
    /// <summary>
    /// Used to build <see cref="DynamicApiActionInfo"/> object.
    /// ���ڹ���<see cref="DynamicApiActionInfo"/>����
    /// </summary>
    /// <typeparam name="T">Type of the proxied object / ��������������</typeparam>
    internal class ApiControllerActionBuilder<T> : IApiControllerActionBuilder<T>
    {
        /// <summary>
        /// Selected action name.
        /// ѡ�е�action����
        /// </summary>
        public string ActionName { get; }

        /// <summary>
        /// Underlying proxying method.
        /// Ǳ�ڵĴ�����
        /// </summary>
        public MethodInfo Method { get; }

        /// <summary>
        /// Selected Http verb.
        /// ѡ�е�Http������
        /// </summary>
        public HttpVerb? Verb { get; set; }

        /// <summary>
        /// Is API Explorer enabled.
        /// �Ƿ���API���
        /// </summary>
        public bool? IsApiExplorerEnabled { get; set; }

        /// <summary>
        /// Action Filters for dynamic controller method.
        /// ��̬������������action����������
        /// </summary>
        public IFilter[] Filters { get; set; }

        /// <summary>
        /// A flag to set if no action will be created for this method.
        /// ����˷���������action��������һ�����
        /// </summary>
        public bool DontCreate { get; set; }

        /// <summary>
        /// Reference to the <see cref="ApiControllerBuilder{T}"/> which created this object.
        /// �����˶����<see cref="ApiControllerBuilder{T}"/>������
        /// </summary>
        public IApiControllerBuilder Controller
        {
            get { return _controller; }
        }
        private readonly ApiControllerBuilder<T> _controller;

        /// <summary>
        /// Creates a new <see cref="ApiControllerActionBuilder{T}"/> object.
        /// ���캯��
        /// </summary>
        /// <param name="apiControllerBuilder">Reference to the <see cref="ApiControllerBuilder{T}"/> which created this object / �����˶����<see cref="ApiControllerBuilder{T}"/>������</param>
        /// <param name="methodInfo">Method / ����</param>
        public ApiControllerActionBuilder(ApiControllerBuilder<T> apiControllerBuilder, MethodInfo methodInfo)
        {
            _controller = apiControllerBuilder;
            Method = methodInfo;
            ActionName = Method.Name;
        }

        /// <summary>
        /// Used to specify Http verb of the action.
        /// ���ڵ�ǰActionָ����http������
        /// </summary>
        /// <param name="verb">Http very / http������</param>
        /// <returns>Action builder / action������</returns>
        public IApiControllerActionBuilder<T> WithVerb(HttpVerb verb)
        {
            Verb = verb;
            return this;
        }

        /// <summary>
        /// Enables/Disables API Explorer for the action.
        /// Ϊaction ����/���� API���
        /// </summary>
        public IApiControllerActionBuilder<T> WithApiExplorer(bool isEnabled)
        {
            IsApiExplorerEnabled = isEnabled;
            return this;
        }

        /// <summary>
        /// Used to specify another method definition.
        /// ����ָ����һ��������
        /// </summary>
        /// <param name="methodName">Name of the method in proxied type / �������ͷ�����</param>
        /// <returns>Action builder / action������</returns>
        public IApiControllerActionBuilder<T> ForMethod(string methodName)
        {
            return _controller.ForMethod(methodName);
        }

        /// <summary>
        /// Used to add action filters to apply to this method.
        /// �������Ӧ���ڴ˷�����action������
        /// </summary>
        /// <param name="filters"> Action Filters to apply. / Ӧ�ù�����</param>
        public IApiControllerActionBuilder<T> WithFilters(params IFilter[] filters)
        {
            Filters = filters;
            return this;
        }

        /// <summary>
        /// Tells builder to not create action for this method.
        /// ������������Ϊ�˷�������action
        /// </summary>
        /// <returns>Controller builder / ������������</returns>
        public IApiControllerBuilder<T> DontCreateAction()
        {
            DontCreate = true;
            return _controller;
        }

        /// <summary>
        /// Builds the controller.This method must be called at last of the build operation.
        /// �������������˷������������ɲ���������
        /// </summary>
        public void Build()
        {
            _controller.Build();
        }

        /// <summary>
        /// Builds <see cref="DynamicApiActionInfo"/> object for this configuration.
        /// Ϊ�����ù���<see cref="DynamicApiActionInfo"/>����
        /// </summary>
        /// <param name="conventionalVerbs"></param>
        /// <returns></returns>
        internal DynamicApiActionInfo BuildActionInfo(bool conventionalVerbs)
        {
            return new DynamicApiActionInfo(
                ActionName,
                GetNormalizedVerb(conventionalVerbs),
                Method,
                Filters,
                IsApiExplorerEnabled
            );
        }

        /// <summary>
        /// ��ȡͳһ�Ĳ���
        /// </summary>
        /// <param name="conventionalVerbs">�Ƿ���ͨ�õ�������</param>
        /// <returns></returns>
        private HttpVerb GetNormalizedVerb(bool conventionalVerbs)
        {
            if (Verb != null)
            {
                return Verb.Value;
            }

            if (Method.IsDefined(typeof(HttpGetAttribute)))
            {
                return HttpVerb.Get;
            }

            if (Method.IsDefined(typeof(HttpPostAttribute)))
            {
                return HttpVerb.Post;
            }

            if (Method.IsDefined(typeof(HttpPutAttribute)))
            {
                return HttpVerb.Put;
            }

            if (Method.IsDefined(typeof(HttpDeleteAttribute)))
            {
                return HttpVerb.Delete;
            }

            if (Method.IsDefined(typeof(HttpPatchAttribute)))
            {
                return HttpVerb.Patch;
            }

            if (Method.IsDefined(typeof(HttpOptionsAttribute)))
            {
                return HttpVerb.Options;
            }

            if (Method.IsDefined(typeof(HttpHeadAttribute)))
            {
                return HttpVerb.Head;
            }

            if (conventionalVerbs)
            {
                var conventionalVerb = DynamicApiVerbHelper.GetConventionalVerbForMethodName(ActionName);
                if (conventionalVerb == HttpVerb.Get && !HasOnlyPrimitiveIncludingNullableTypeParameters(Method))
                {
                    conventionalVerb = DynamicApiVerbHelper.GetDefaultHttpVerb();
                }

                return conventionalVerb;
            }

            return DynamicApiVerbHelper.GetDefaultHttpVerb();
        }

        /// <summary>
        /// ����ԭʼ�İ����ɿ����Ͳ���
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        private static bool HasOnlyPrimitiveIncludingNullableTypeParameters(MethodInfo methodInfo)
        {
            return methodInfo.GetParameters().All(p => TypeHelper.IsPrimitiveExtendedIncludingNullable(p.ParameterType) || p.IsDefined(typeof(FromUriAttribute)));
        }
    }
}