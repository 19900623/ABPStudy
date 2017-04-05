using System;

namespace Abp.EntityFramework
{
    /// <summary>
    /// ���ݿ�����������ƥ�����ӿ�
    /// </summary>
    public interface IDbContextTypeMatcher
    {
        /// <summary>
        /// ������ݿ�����������
        /// </summary>
        /// <param name="dbContextTypes">���ݿ�����������</param>
        void Populate(Type[] dbContextTypes);

        /// <summary>
        /// ��ȡ���������
        /// </summary>
        /// <param name="sourceDbContextType">Դ���ݿ�����������</param>
        /// <returns>��������</returns>
        Type GetConcreteType(Type sourceDbContextType);
    }
}