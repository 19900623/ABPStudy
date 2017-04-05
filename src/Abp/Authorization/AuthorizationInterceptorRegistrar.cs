using System;
using System.Linq;
using System.Reflection;
using Abp.Application.Features;
using Abp.Dependency;
using Castle.Core;
using Castle.MicroKernel;

namespace Abp.Authorization
{
    /// <summary>
    /// This class is used to register interceptors on the Application Layer.
    /// ����������Ӧ�ò�ע��������
    /// </summary>
    internal static class AuthorizationInterceptorRegistrar
    {
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="iocManager">IOC������</param>
        public static void Initialize(IIocManager iocManager)
        {
            //���ע���¼�
            iocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;            
        }

        /// <summary>
        /// ���ע���¼�������
        /// </summary>
        /// <param name="key"></param>
        /// <param name="handler"></param>
        private static void Kernel_ComponentRegistered(string key, IHandler handler)
        {
            if (ShouldIntercept(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(AuthorizationInterceptor))); 
            }
        }

        /// <summary>
        /// Ӧ������
        /// </summary>
        /// <param name="type">����</param>
        /// <returns></returns>
        private static bool ShouldIntercept(Type type)
        {
            if (SelfOrMethodsDefinesAttribute<AbpAuthorizeAttribute>(type))
            {
                return true;
            }

            if (SelfOrMethodsDefinesAttribute<RequiresFeatureAttribute>(type))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// �Լ��򷽷���������
        /// </summary>
        /// <typeparam name="TAttr">���Զ���</typeparam>
        /// <param name="type">����</param>
        /// <returns></returns>
        private static bool SelfOrMethodsDefinesAttribute<TAttr>(Type type)
        {
            if (type.IsDefined(typeof(TAttr), true))
            {
                return true;
            }

            return type
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Any(m => m.IsDefined(typeof(TAttr), true));
        }
    }
}