using System.Threading.Tasks;

namespace Abp.Authorization
{
    /// <summary>
    /// Most simple implementation of <see cref="IPermissionDependency"/>.It checks one or more permissions if they are granted.
    /// <see cref="IPermissionDependency"/>.��򵥵�ʵ�֣������һ������Ȩ�ޣ�������Ǳ�����
    /// </summary>
    public class SimplePermissionDependency : IPermissionDependency
    {
        /// <summary>
        /// A list of permissions to be checked if they are granted.
        /// Ҫ����Ȩ���б�������Ǳ�����
        /// </summary>
        public string[] Permissions { get; set; }

        /// <summary>
        /// If this property is set to true, all of the <see cref="Permissions"/> must be granted.
        /// �������Ϊtrue�����е�<see cref="Permissions"/>���뱻����
        /// If it's false, at least one of the <see cref="Permissions"/> must be granted.
        /// �������Ϊfalse������һ��<see cref="Permissions"/>���뱻����
        /// Default: false.
        /// </summary>
        public bool RequiresAll { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimplePermissionDependency"/> class.
        /// ��ʼ��<see cref="SimplePermissionDependency"/>����ʵ��
        /// </summary>
        /// <param name="permissions">The features. / ����</param>
        public SimplePermissionDependency(params string[] permissions)
        {
            Permissions = permissions;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimplePermissionDependency"/> class.
        /// ��ʼ��<see cref="SimplePermissionDependency"/>����ʵ��
        /// </summary>
        /// <param name="requiresAll">
        /// If this is set to true, all of the <see cref="Permissions"/> must be granted.
        /// �������Ϊtrue�����е�<see cref="Permissions"/>���뱻����
        /// If it's false, at least one of the <see cref="Permissions"/> must be granted.
        /// �������Ϊfalse������һ��<see cref="Permissions"/>���뱻����
        /// </param>
        /// <param name="features">The features.</param>
        public SimplePermissionDependency(bool requiresAll, params string[] features)
            : this(features)
        {
            RequiresAll = requiresAll;
        }

        /// <summary>
        /// ����Ƿ�����Ȩ������
        /// </summary>
        /// <param name="context">Ȩ������������</param>
        /// <returns></returns>
        public Task<bool> IsSatisfiedAsync(IPermissionDependencyContext context)
        {
            return context.User != null
                ? context.PermissionChecker.IsGrantedAsync(context.User, RequiresAll, Permissions)
                : context.PermissionChecker.IsGrantedAsync(RequiresAll, Permissions);
        }
    }
}