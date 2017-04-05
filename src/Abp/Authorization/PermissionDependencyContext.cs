using Abp.Dependency;

namespace Abp.Authorization
{
    /// <summary>
    /// Ȩ������������
    /// </summary>
    internal class PermissionDependencyContext : IPermissionDependencyContext, ITransientDependency
    {
        /// <summary>
        /// ��ҪȨ�޵��û������û���û�����Ϊ��
        /// </summary>
        public UserIdentifier User { get; set; }

        /// <summary>
        /// IOC������
        /// </summary>
        public IIocResolver IocResolver { get; }
        
        /// <summary>
        /// Ȩ�޼����
        /// </summary>
        public IPermissionChecker PermissionChecker { get; set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="iocResolver">IOC������</param>
        public PermissionDependencyContext(IIocResolver iocResolver)
        {
            IocResolver = iocResolver;
            PermissionChecker = NullPermissionChecker.Instance;
        }
    }
}