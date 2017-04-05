using System;
using System.Reflection;
using Abp.Reflection.Extensions;

namespace Abp.Application.Services
{
    /// <summary>
    /// Զ�̷�������
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method)]
    public class RemoteServiceAttribute : Attribute
    {
        /// <summary>
        /// �Ƿ����á� Default: true.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// �Ƿ�����Ԫ���ݡ�Default: true.
        /// </summary>
        public bool IsMetadataEnabled { get; set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="isEnabled"></param>
        public RemoteServiceAttribute(bool isEnabled = true)
        {
            IsEnabled = isEnabled;
            IsMetadataEnabled = true;
        }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        /// <param name="type">����</param>
        /// <returns></returns>
        public virtual bool IsEnabledFor(Type type)
        {
            return IsEnabled;
        }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        /// <param name="method">����</param>
        /// <returns></returns>
        public virtual bool IsEnabledFor(MethodInfo method)
        {
            return IsEnabled;
        }

        /// <summary>
        /// �Ƿ�����Ԫ����
        /// </summary>
        /// <param name="type">����</param>
        /// <returns></returns>
        public virtual bool IsMetadataEnabledFor(Type type)
        {
            return IsMetadataEnabled;
        }

        /// <summary>
        /// �Ƿ�����Ԫ����
        /// </summary>
        /// <param name="method">����</param>
        /// <returns></returns>
        public virtual bool IsMetadataEnabledFor(MethodInfo method)
        {
            return IsMetadataEnabled;
        }

        /// <summary>
        /// �Ƿ���ʾ����
        /// </summary>
        /// <param name="type">����</param>
        /// <returns></returns>
        public static bool IsExplicitlyEnabledFor(Type type)
        {
            var remoteServiceAttr = type.GetSingleAttributeOrNull<RemoteServiceAttribute>();
            return remoteServiceAttr != null && remoteServiceAttr.IsEnabledFor(type);
        }

        /// <summary>
        /// �Ƿ���ʾ����
        /// </summary>
        /// <param name="type">����</param>
        /// <returns></returns>
        public static bool IsExplicitlyDisabledFor(Type type)
        {
            var remoteServiceAttr = type.GetSingleAttributeOrNull<RemoteServiceAttribute>();
            return remoteServiceAttr != null && !remoteServiceAttr.IsEnabledFor(type);
        }

        /// <summary>
        /// Ԫ�����Ƿ���ʾ����
        /// </summary>
        /// <param name="type">����</param>
        /// <returns></returns>
        public static bool IsMetadataExplicitlyEnabledFor(Type type)
        {
            var remoteServiceAttr = type.GetSingleAttributeOrNull<RemoteServiceAttribute>();
            return remoteServiceAttr != null && remoteServiceAttr.IsMetadataEnabledFor(type);
        }

        /// <summary>
        /// Ԫ�����Ƿ���ʾ����
        /// </summary>
        /// <param name="type">����</param>
        /// <returns></returns>
        public static bool IsMetadataExplicitlyDisabledFor(Type type)
        {
            var remoteServiceAttr = type.GetSingleAttributeOrNull<RemoteServiceAttribute>();
            return remoteServiceAttr != null && !remoteServiceAttr.IsMetadataEnabledFor(type);
        }

        /// <summary>
        /// Ԫ�����Ƿ���ʾ����
        /// </summary>
        /// <param name="method">����</param>
        /// <returns></returns>
        public static bool IsMetadataExplicitlyDisabledFor(MethodInfo method)
        {
            var remoteServiceAttr = method.GetSingleAttributeOrNull<RemoteServiceAttribute>();
            return remoteServiceAttr != null && !remoteServiceAttr.IsMetadataEnabledFor(method);
        }

        /// <summary>
        /// Ԫ�����Ƿ���ʾ����
        /// </summary>
        /// <param name="method">����</param>
        /// <returns></returns>
        public static bool IsMetadataExplicitlyEnabledFor(MethodInfo method)
        {
            var remoteServiceAttr = method.GetSingleAttributeOrNull<RemoteServiceAttribute>();
            return remoteServiceAttr != null && remoteServiceAttr.IsMetadataEnabledFor(method);
        }
    }
}