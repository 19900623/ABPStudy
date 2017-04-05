using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// ������ԪĬ��ѡ��ʵ�� -- �����η�internal ��Ϊ public(Derrick 2017/04/02)
    /// </summary>
    public class UnitOfWorkDefaultOptions : IUnitOfWorkDefaultOptions
    {
        /// <summary>
        /// ���ﷶΧ
        /// </summary>
        public TransactionScopeOption Scope { get; set; }

        /// <summary>
        /// ������Ԫ�ܷ�֧��������.Default: true.
        /// </summary>
        public bool IsTransactional { get; set; }

        /// <summary>
        /// ��ȡ/���ù�����Ԫ�ĳ�ʱʱ��
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// ��ȡ/��������ĸ��뼶��.��� <see cref="IsTransactional"/> Ϊture���������õ�
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// ��ȡ���еĹ����������б�
        /// </summary>
        public IReadOnlyList<DataFilterConfiguration> Filters
        {
            get { return _filters; }
        }
        private readonly List<DataFilterConfiguration> _filters;

        /// <summary>
        /// ע��һ�����ݹ��Ƕ��󵽹�����Ԫ
        /// </summary>
        /// <param name="filterName">����������</param>
        /// <param name="isEnabledByDefault">�������Ƿ�Ĭ������</param>
        public void RegisterFilter(string filterName, bool isEnabledByDefault)
        {
            if (_filters.Any(f => f.FilterName == filterName))
            {
                throw new AbpException("There is already a filter with name: " + filterName);
            }

            _filters.Add(new DataFilterConfiguration(filterName, isEnabledByDefault));
        }

        /// <summary>
        /// ��д����������
        /// </summary>
        /// <param name="filterName">����������</param>
        /// <param name="isEnabledByDefault">�������Ƿ�Ĭ������</param>
        public void OverrideFilter(string filterName, bool isEnabledByDefault)
        {
            _filters.RemoveAll(f => f.FilterName == filterName);
            _filters.Add(new DataFilterConfiguration(filterName, isEnabledByDefault));
        }

        /// <summary>
        /// ����һ��<see cref="UnitOfWorkDefaultOptions"/>����
        /// </summary>
        public UnitOfWorkDefaultOptions()
        {
            _filters = new List<DataFilterConfiguration>();
            IsTransactional = true;
            Scope = TransactionScopeOption.Required;
        }
    }
}