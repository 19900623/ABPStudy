using System.Threading.Tasks;
using Abp.Threading;
using Castle.DynamicProxy;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// This interceptor is used to manage database connection and transactions.
    /// ���������ù������ݿ����Ӻ�����
    /// </summary>
    internal class UnitOfWorkInterceptor : IInterceptor
    {
        /// <summary>
        /// ������Ԫ������
        /// </summary>
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="unitOfWorkManager">������Ԫ������</param>
        public UnitOfWorkInterceptor(IUnitOfWorkManager unitOfWorkManager)
        {
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// Intercepts a method.
        /// ����һ������
        /// </summary>
        /// <param name="invocation">Method invocation arguments / �������ò���</param>
        public void Intercept(IInvocation invocation)
        {
            var unitOfWorkAttr = UnitOfWorkAttribute.GetUnitOfWorkAttributeOrNull(invocation.MethodInvocationTarget);
            if (unitOfWorkAttr == null || unitOfWorkAttr.IsDisabled)
            {
                //No need to a uow / ����Ҫ������Ԫ
                invocation.Proceed();
                return;
            }

            //No current uow, run a new one / 
            PerformUow(invocation, unitOfWorkAttr.CreateOptions());
        }

        /// <summary>
        /// ׼��������Ԫ
        /// </summary>
        /// <param name="invocation"></param>
        /// <param name="options">������Ԫѡ��</param>
        private void PerformUow(IInvocation invocation, UnitOfWorkOptions options)
        {
            if (AsyncHelper.IsAsyncMethod(invocation.Method))
            {
                PerformAsyncUow(invocation, options);
            }
            else
            {
                PerformSyncUow(invocation, options);
            }
        }

        /// <summary>
        /// ׼��ͬ��������Ԫ
        /// </summary>
        /// <param name="invocation"></param>
        /// <param name="options">������Ԫѡ��</param>
        private void PerformSyncUow(IInvocation invocation, UnitOfWorkOptions options)
        {
            using (var uow = _unitOfWorkManager.Begin(options))
            {
                invocation.Proceed();
                uow.Complete();
            }
        }

        /// <summary>
        /// ׼���첽������Ԫ
        /// </summary>
        /// <param name="invocation"></param>
        /// <param name="options">������Ԫѡ��</param>
        private void PerformAsyncUow(IInvocation invocation, UnitOfWorkOptions options)
        {
            var uow = _unitOfWorkManager.Begin(options);

            invocation.Proceed();

            if (invocation.Method.ReturnType == typeof(Task))
            {
                invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithPostActionAndFinally(
                    (Task)invocation.ReturnValue,
                    async () => await uow.CompleteAsync(),
                    exception => uow.Dispose()
                    );
            }
            else //Task<TResult>
            {
                invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithPostActionAndFinallyAndGetResult(
                    invocation.Method.ReturnType.GenericTypeArguments[0],
                    invocation.ReturnValue,
                    async () => await uow.CompleteAsync(),
                    (exception) => uow.Dispose()
                    );
            }
        }
    }
}