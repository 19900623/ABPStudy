using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Collections.Extensions;

namespace Abp.Modules
{
    /// <summary>
    /// Used to store AbpModuleInfo objects as a dictionary.
    /// ��һ���ֵ�洢AbpModuleInfo����
    /// </summary>
    internal class AbpModuleCollection : List<AbpModuleInfo>
    {
        /// <summary>
        /// ����ģ������
        /// </summary>
        public Type StartupModuleType { get; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="startupModuleType">����ģ������</param>
        public AbpModuleCollection(Type startupModuleType)
        {
            StartupModuleType = startupModuleType;
        }

        /// <summary>
        /// Gets a reference to a module instance.
        /// ��ȡһ��ģ��ʵ��������
        /// </summary>
        /// <typeparam name="TModule">Module type / ģ������</typeparam>
        /// <returns>Reference to the module instance / ģ��ʵ��������</returns>
        public TModule GetModule<TModule>() where TModule : AbpModule
        {
            var module = this.FirstOrDefault(m => m.Type == typeof(TModule));
            if (module == null)
            {
                throw new AbpException("Can not find module for " + typeof(TModule).FullName);
            }

            return (TModule)module.Instance;
        }

        /// <summary>
        /// Sorts modules according to dependencies.If module A depends on module B, A comes after B in the returned List.
        /// ��ģ��������Զ�ģ������.���A������B����ôA������B֮��
        /// </summary>
        /// <returns>Sorted list</returns>
        public List<AbpModuleInfo> GetSortedModuleListByDependency()
        {
            var sortedModules = this.SortByDependencies(x => x.Dependencies);
            EnsureKernelModuleToBeFirst(sortedModules);
            EnsureStartupModuleToBeLast(sortedModules, StartupModuleType);
            return sortedModules;
        }

        /// <summary>
        /// ȷ���ں�ģ������
        /// </summary>
        /// <param name="modules">ģ���б�</param>
        public static void EnsureKernelModuleToBeFirst(List<AbpModuleInfo> modules)
        {
            var kernelModuleIndex = modules.FindIndex(m => m.Type == typeof(AbpKernelModule));
            if (kernelModuleIndex <= 0)
            {
                //It's already the first!
                return;
            }

            var kernelModule = modules[kernelModuleIndex];
            modules.RemoveAt(kernelModuleIndex);
            modules.Insert(0, kernelModule);
        }

        /// <summary>
        /// ȷ������ģ�����
        /// </summary>
        /// <param name="modules">ģ���б�</param>
        /// <param name="startupModuleType">����ģ������</param>
        public static void EnsureStartupModuleToBeLast(List<AbpModuleInfo> modules, Type startupModuleType)
        {
            var startupModuleIndex = modules.FindIndex(m => m.Type == startupModuleType);
            if (startupModuleIndex >= modules.Count - 1)
            {
                //It's already the last!
                return;
            }

            var startupModule = modules[startupModuleIndex];
            modules.RemoveAt(startupModuleIndex);
            modules.Add(startupModule);
        }

        /// <summary>
        /// ȷ���ں�ģ������
        /// </summary>
        public void EnsureKernelModuleToBeFirst()
        {
            EnsureKernelModuleToBeFirst(this);
        }

        /// <summary>
        /// ȷ������ģ�����
        /// </summary>
        public void EnsureStartupModuleToBeLast()
        {
            EnsureStartupModuleToBeLast(this, StartupModuleType);
        }
    }
}