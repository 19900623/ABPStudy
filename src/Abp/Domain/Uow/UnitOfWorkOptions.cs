using System;
using System.Collections.Generic;
using System.Transactions;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Unit of work options.
    /// ������Ԫѡ��
    /// </summary>
    public class UnitOfWorkOptions
    {
        /// <summary>
        /// Scope option.
        /// ���ﷶΧѡ��
        /// </summary>
        public TransactionScopeOption? Scope { get; set; }

        /// <summary>
        /// Is this UOW transactional?Uses default value if not supplied.
        /// �˹�����Ԫ�Ƿ�֧�����Ĭ�ϲ�֧�֡�
        /// </summary>
        public bool? IsTransactional { get; set; }

        /// <summary>
        /// Timeout of UOW As milliseconds.Uses default value if not supplied.
        /// �˹�����Ԫ�ĳ�ʱʱ��Ϊ���롣�����֧�֣���ʹ��Ĭ��ֵ
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// If this UOW is transactional, this option indicated the isolation level of the transaction.
        /// ���������Ԫ֧�����񣬴�ѡ��ָ���¼��ĸ��뼶��
        /// Uses default value if not supplied.
        /// �����֧�֣���ʹ��Ĭ��ֵ
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// This option should be set to <see cref="TransactionScopeAsyncFlowOption.Enabled"/> if unit of work is used in an async scope.
        /// ���������ʹ���첽����ѡ���Ω����Ϊ<see cref="TransactionScopeAsyncFlowOption.Enabled"/>
        /// </summary>
        public TransactionScopeAsyncFlowOption? AsyncFlowOption { get; set; }

        /// <summary>
        /// Can be used to enable/disable some filters. 
        /// ����������/����ĳЩ������
        /// </summary>
        public List<DataFilterConfiguration> FilterOverrides { get; private set; }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkOptions"/> object.
        /// ����һ���µ� <see cref="UnitOfWorkOptions"/> ����
        /// </summary>
        public UnitOfWorkOptions()
        {
            FilterOverrides = new List<DataFilterConfiguration>();
        }

        /// <summary>
        /// Ϊû���ṩ�ߵ�ѡ�����Ĭ��ֵ
        /// </summary>
        /// <param name="defaultOptions"></param>
        internal void FillDefaultsForNonProvidedOptions(IUnitOfWorkDefaultOptions defaultOptions)
        {
            //TODO: Do not change options object..?

            if (!IsTransactional.HasValue)
            {
                IsTransactional = defaultOptions.IsTransactional;
            }

            if (!Scope.HasValue)
            {
                Scope = defaultOptions.Scope;
            }

            if (!Timeout.HasValue && defaultOptions.Timeout.HasValue)
            {
                Timeout = defaultOptions.Timeout.Value;
            }

            if (!IsolationLevel.HasValue && defaultOptions.IsolationLevel.HasValue)
            {
                IsolationLevel = defaultOptions.IsolationLevel.Value;
            }
        }
    }
}