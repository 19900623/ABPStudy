using System;
using System.Linq;
using System.Reflection;
using System.Transactions;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// This attribute is used to indicate that declaring method is atomic and should be considered as a unit of work.
    /// ������������������������ķ�����ԭ��,Ӧ����Ϊһ��������Ԫ
    /// A method that has this attribute is intercepted, a database connection is opened and a transaction is started before call the method.
    /// ӵ�д����Եķ����������أ��ڵ��÷���֮ǰһ�����ݿ����Ӻ�һ�����񽫱�����
    /// At the end of method call, transaction is committed and all changes applied to the database if there is no exception,otherwise it's rolled back. 
    /// ���û���쳣���ڷ������ý���ʱ�������ύ�����еĸ����ύ�����ݿ��У����򣬻ع�
    /// </summary>
    /// <remarks>
    /// This attribute has no effect if there is already a unit of work before calling this method, if so, it uses the same transaction.
    /// ����ڵ��÷���֮ǰ���Ѿ����ڹ�����Ԫ��������Բ�������κ�Ӱ�졣����ʹ����ͬ������
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method)]
    public class UnitOfWorkAttribute : Attribute
    {
        /// <summary>
        /// Scope option.
        /// ���ﷶΧ
        /// </summary>
        public TransactionScopeOption? Scope { get; set; }

        /// <summary>
        /// Is this UOW transactional?Uses default value if not supplied.
        /// �˹�����Ԫ�Ƿ�֧���¼���Ĭ�ϲ�֧��
        /// </summary>
        public bool? IsTransactional { get; set; }

        /// <summary>
        /// Timeout of UOW As milliseconds.Uses default value if not supplied.
        /// ������Ԫ�ĳ�ʱʱ�䣨���룩Ĭ�ϲ�֧��
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// If this UOW is transactional, this option indicated the isolation level of the transaction.Uses default value if not supplied.
        /// ���������Ԫ֧�����񣬴�����ָʾ����ĸ��뼶��.Ĭ�ϲ�֧��
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// Used to prevent starting a unit of work for the method.
        /// ������ֹΪһ����������������Ԫ������Ѿ�����һ��������Ԫ��
        /// If there is already a started unit of work, this property is ignored.Default: false.
        /// �����Խ������ԣ�Ĭ��ֵΪ:false
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// Creates a new UnitOfWorkAttribute object.
        /// ����һ��һ��������Ԫ����
        /// </summary>
        public UnitOfWorkAttribute()
        {

        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// ����һ���µ�<see cref="UnitOfWorkAttribute"/> ����.
        /// </summary>
        /// <param name="isTransactional">
        /// Is this unit of work will be transactional? / ָʾ������Ԫ�Ƿ�֧������
        /// </param>
        public UnitOfWorkAttribute(bool isTransactional)
        {
            IsTransactional = isTransactional;
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// ����һ���µ�<see cref="UnitOfWorkAttribute"/> ����.
        /// </summary>
        /// <param name="timeout">As milliseconds / ��ʱʱ�䣨���룩</param>
        public UnitOfWorkAttribute(int timeout)
        {
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.
        /// ����һ���µ�<see cref="UnitOfWorkAttribute"/> ����.
        /// </summary>
        /// <param name="isTransactional">Is this unit of work will be transactional? / ָʾ������Ԫ�Ƿ�֧���¸�</param>
        /// <param name="timeout">As milliseconds / ��ʱʱ�䣨���룩</param>
        public UnitOfWorkAttribute(bool isTransactional, int timeout)
        {
            IsTransactional = isTransactional;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.<see cref="IsTransactional"/> is automatically set to true.
        /// ����һ���µ�<see cref="UnitOfWorkAttribute"/> ����.<see cref="IsTransactional"/> ����Ϊtrue.
        /// </summary>
        /// <param name="isolationLevel">Transaction isolation level / ������뼶��</param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel)
        {
            IsTransactional = true;
            IsolationLevel = isolationLevel;
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.<see cref="IsTransactional"/> is automatically set to true.
        /// ����һ���µ� <see cref="UnitOfWorkAttribute"/> ����.<see cref="IsTransactional"/> ����Ϊtrue.
        /// </summary>
        /// <param name="isolationLevel">Transaction isolation level / ������뼶��</param>
        /// <param name="timeout">Transaction  timeout as milliseconds / ����ʱʱ�䣨���룩</param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel, int timeout)
        {
            IsTransactional = true;
            IsolationLevel = isolationLevel;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object.<see cref="IsTransactional"/> is automatically set to true.
        /// ����һ���µ� <see cref="UnitOfWorkAttribute"/> ����.<see cref="IsTransactional"/> ����Ϊtrue.
        /// </summary>
        /// <param name="scope">Transaction scope / ���ﷶΧ</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope)
        {
            IsTransactional = true;
            Scope = scope;
        }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkAttribute"/> object. <see cref="IsTransactional"/> is automatically set to true.
        /// ����һ���µ� <see cref="UnitOfWorkAttribute"/> ����.<see cref="IsTransactional"/> ����Ϊtrue.
        /// </summary>
        /// <param name="scope">Transaction scope / ���ﷶΧ</param>
        /// <param name="timeout">Transaction timeout as milliseconds / ����ʱʱ�䣨���룩</param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, int timeout)
        {
            IsTransactional = true;
            Scope = scope;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// Gets UnitOfWorkAttribute for given method or null if no attribute defined.
        /// �Ӹ����ķ����л�ȡUnitOfWorkAttribute���ԣ����û�з���Null
        /// </summary>
        /// <param name="methodInfo">Method to get attribute / ��ȡ���Եķ���</param>
        /// <returns>The UnitOfWorkAttribute object / <see cref="UnitOfWorkAttribute"/>����</returns>
        public static UnitOfWorkAttribute GetUnitOfWorkAttributeOrNull(MemberInfo methodInfo)
        {
            var attrs = methodInfo.GetCustomAttributes(true).OfType<UnitOfWorkAttribute>().ToArray();
            if (attrs.Length > 0)
            {
                return attrs[0];
            }

            if (UnitOfWorkHelper.IsConventionalUowClass(methodInfo.DeclaringType))
            {
                return new UnitOfWorkAttribute(); //Default
            }

            return null;
        }

        /// <summary>
        /// ����������Ԫѡ��
        /// </summary>
        /// <returns></returns>
        public UnitOfWorkOptions CreateOptions()
        {
            return new UnitOfWorkOptions
            {
                IsTransactional = IsTransactional,
                IsolationLevel = IsolationLevel,
                Timeout = Timeout,
                Scope = Scope
            };
        }
    }
}