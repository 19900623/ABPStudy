using System;
using System.Collections.Generic;

namespace Abp.Modules
{
    /// <summary>
    /// ABPģ��������ӿ�
    /// </summary>
    public interface IAbpModuleManager
    {
        /// <summary>
        /// ����ģ���ģ����Ϣ
        /// </summary>
        AbpModuleInfo StartupModule { get; }

        /// <summary>
        /// ģ����Ϣֻ���б�
        /// </summary>
        IReadOnlyList<AbpModuleInfo> Modules { get; }

        /// <summary>
        /// ��ʼ��ģ��
        /// </summary>
        /// <param name="startupModule"></param>
        void Initialize(Type startupModule);

        /// <summary>
        /// ����ģ��
        /// </summary>
        void StartModules();

        /// <summary>
        /// �ر�ģ��
        /// </summary>
        void ShutdownModules();
    }
}