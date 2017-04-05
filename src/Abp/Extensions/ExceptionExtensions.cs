using System;
using System.Runtime.ExceptionServices;

namespace Abp.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Exception"/> class.
    /// <see cref="Exception"/> ����չ����.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Uses <see cref="ExceptionDispatchInfo.Capture"/> method to re-throws exception while preserving stack trace.
        /// ʹ�÷��� <see cref="ExceptionDispatchInfo.Capture"/> �����׳��쳣,�����ջ���٣�Ҳ����ԭ����exception�Ķ�ջ��Ϣ�������������׳��쳣ʱ�Ķ�ջ��Ϣ
        /// </summary>
        /// <param name="exception">Exception to be re-thrown / Ҫ�׳����쳣</param>
        public static void ReThrow(this Exception exception)
        {
            ExceptionDispatchInfo.Capture(exception).Throw();
        }
    }
}