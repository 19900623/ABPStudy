using System.Collections.Generic;

namespace Abp.Authorization.Roles
{
    /// <summary>
    /// Equality comparer for <see cref="Permission"/> objects.
    /// <see cref="Permission"/>������ȱȽ���
    /// </summary>
    internal class PermissionEqualityComparer : IEqualityComparer<Permission>
    {
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        public static PermissionEqualityComparer Instance { get { return _instance; } }
        private static PermissionEqualityComparer _instance = new PermissionEqualityComparer();

        /// <summary>
        /// ��ȱȽϲ���
        /// </summary>
        /// <param name="x">Ȩ��X����</param>
        /// <param name="y">Ȩ��Y����</param>
        /// <returns></returns>
        public bool Equals(Permission x, Permission y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }

            return Equals(x.Name, y.Name);
        }
        /// <summary>
        /// ��ȡȨ�����Ƶ�HashCode
        /// </summary>
        /// <param name="permission">Ȩ�޶���</param>
        /// <returns></returns>
        public int GetHashCode(Permission permission)
        {
            return permission.Name.GetHashCode();
        }
    }
}