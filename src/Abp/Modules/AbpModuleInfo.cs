using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;

namespace Abp.Modules
{
    /// <summary>
    /// Used to store all needed information for a module.
    /// �����洢һ��ģ��������Ϣ
    /// </summary>
    public class AbpModuleInfo
    {
        /// <summary>
        /// The assembly which contains the module definition.
        /// ����ģ�鶨��ĳ���
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// Type of the module.
        /// ģ�������
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Instance of the module.
        /// ģ��ʵ��
        /// </summary>
        public AbpModule Instance { get; }

        /// <summary>
        /// All dependent modules of this module.
        /// ��ģ�����������ģ��
        /// </summary>
        public List<AbpModuleInfo> Dependencies { get; }

        /// <summary>
        /// Creates a new AbpModuleInfo object.
        /// ����һ���µ�AbpModuleInfo����.
        /// </summary>
        public AbpModuleInfo([NotNull] Type type, [NotNull] AbpModule instance)
        {
            Check.NotNull(type, nameof(type));
            Check.NotNull(instance, nameof(instance));

            Type = type;
            Instance = instance;
            Assembly = Type.Assembly;

            Dependencies = new List<AbpModuleInfo>();
        }

        /// <summary>
        /// ��дToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Type.AssemblyQualifiedName ??
                   Type.FullName;
        }
    }
}