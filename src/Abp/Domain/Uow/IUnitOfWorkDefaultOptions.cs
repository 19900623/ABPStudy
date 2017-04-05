using System;
using System.Collections.Generic;
using System.Transactions;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Used to get/set default options for a unit of work.
    /// ���ڻ�ȡ/���ù�����ԪĬ��ѡ��
    /// </summary>
    public interface IUnitOfWorkDefaultOptions
    {
        /// <summary>
        /// Scope option.
        /// ���ﷶΧ
        /// </summary>
        TransactionScopeOption Scope { get; set; }

        /// <summary>
        /// Should unit of works be transactional.Default: true.
        /// ������Ԫ�ܷ�֧��������Default: true.
        /// </summary>
        bool IsTransactional { get; set; }

        /// <summary>
        /// Gets/sets a timeout value for unit of works.
        /// ��ȡ/���ù�����Ԫ�ĳ�ʱʱ��
        /// </summary>
        TimeSpan? Timeout { get; set; }

        /// <summary>
        /// Gets/sets isolation level of transaction.This is used if <see cref="IsTransactional"/> is true.
        /// ��ȡ/��������ĸ��뼶��,��� <see cref="IsTransactional"/> Ϊture���������õ�
        /// </summary>
        IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// Gets list of all data filter configurations.
        /// ��ȡ���еĹ����������б�
        /// </summary>
        IReadOnlyList<DataFilterConfiguration> Filters { get; }

        /// <summary>
        /// Registers a data filter to unit of work system.
        /// ע��һ�����ݹ��Ƕ��󵽹�����Ԫ
        /// </summary>
        /// <param name="filterName">Name of the filter. / ����������</param>
        /// <param name="isEnabledByDefault">Is filter enabled by default. / �������Ƿ�Ĭ������</param>
        void RegisterFilter(string filterName, bool isEnabledByDefault);

        /// <summary>
        /// Overrides a data filter definition.
        /// ��д����������
        /// </summary>
        /// <param name="filterName">Name of the filter. / ����������</param>
        /// <param name="isEnabledByDefault">Is filter enabled by default. / �������Ƿ�Ĭ������</param>
        void OverrideFilter(string filterName, bool isEnabledByDefault);
    }
}