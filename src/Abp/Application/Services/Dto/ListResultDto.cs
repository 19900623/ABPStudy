using System;
using System.Collections.Generic;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// Implements <see cref="IListResult{T}"/>.
    /// 实现接口 <see cref="IListResult{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="Items"/> list / <see cref="Items"/>中项的类型</typeparam>
    [Serializable]
    public class ListResultDto<T> : IListResult<T>
    {
        /// <summary>
        /// List of items.
        /// 列表项
        /// </summary>
        public IReadOnlyList<T> Items
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }
        private IReadOnlyList<T> _items;

        /// <summary>
        /// Creates a new <see cref="ListResultDto{T}"/> object.
        /// 构造函数
        /// </summary>
        public ListResultDto()
        {
            
        }

        /// <summary>
        /// Creates a new <see cref="ListResultDto{T}"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="items">List of items / 列表项</param>
        public ListResultDto(IReadOnlyList<T> items)
        {
            Items = items;
        }
    }
}