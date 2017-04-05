using System;
using System.Data.Entity;
using Abp.MultiTenancy;

namespace Abp.EntityFramework
{
    /// <summary>
    /// ���ݿ��������ṩ�߽ӿ�
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IDbContextProvider<out TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// ��ȡ���ݿ�������
        /// </summary>
        /// <returns>���ݿ������Ķ���</returns>
        TDbContext GetDbContext();

        /// <summary>
        /// ��ȡ���ݿ�������
        /// </summary>
        /// <param name="multiTenancySide">��ʾ���⻧˫���е�һ��</param>
        /// <returns>���ݿ�������</returns>
        TDbContext GetDbContext(MultiTenancySides? multiTenancySide );
    }
}