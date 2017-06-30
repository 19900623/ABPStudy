using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Abp.Dependency;

namespace Abp.WebApi.Controllers
{
    /// <summary>
    /// This class is used to use IOC system to create api controllers.It's used by ASP.NET system.
    /// ��������ʹ��IOCϵͳ����API����������ͨ��ASP.NETϵͳʹ��
    /// </summary>
    public class AbpApiControllerActivator : IHttpControllerActivator
    {
        /// <summary>
        /// IOC������
        /// </summary>
        private readonly IIocResolver _iocResolver;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="iocResolver"></param>
        public AbpApiControllerActivator(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        /// <summary>
        /// ����Http������
        /// </summary>
        /// <param name="request">Http������Ϣ</param>
        /// <param name="controllerDescriptor">Http������������</param>
        /// <param name="controllerType">����������</param>
        /// <returns></returns>
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controllerWrapper = _iocResolver.ResolveAsDisposable<IHttpController>(controllerType);
            request.RegisterForDispose(controllerWrapper);
            return controllerWrapper.Object;
        }
    }
}