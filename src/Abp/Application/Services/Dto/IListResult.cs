using System.Collections.Generic;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to return a list of items to clients.
    /// �˽ӿڶ��巵�ؿͻ��˵ı�����
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="Items"/> list / <see cref="Items"/>�б����������</typeparam>
    public interface IListResult<T>
    {
        /// <summary>
        /// List of items.
        /// �б�
        /// </summary>
        IReadOnlyList<T> Items { get; set; }
    }
}