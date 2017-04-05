using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Abp.Threading
{
    /// <summary>
    /// �ڲ��첽������
    /// </summary>
    internal static class InternalAsyncHelper
    {
        /// <summary>
        /// �ȴ�������ɣ�����Finally����ִ��action
        /// </summary>
        /// <param name="actualReturnValue">ʵ�ʷ���ֵ</param>
        /// <param name="finalAction">���ִ�еķ���</param>
        /// <returns></returns>
        public static async Task AwaitTaskWithFinally(Task actualReturnValue, Action<Exception> finalAction)
        {
            Exception exception = null;

            try
            {
                await actualReturnValue;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                finalAction(exception);
            }
        }

        /// <summary>
        /// �ȴ�Post����������ɣ�����Finally����ִ��action
        /// </summary>
        /// <param name="actualReturnValue">ʵ�ʷ���ֵ</param>
        /// <param name="postAction">POST ����</param>
        /// <param name="finalAction">���ִ�еķ���</param>
        /// <returns></returns>
        public static async Task AwaitTaskWithPostActionAndFinally(Task actualReturnValue, Func<Task> postAction, Action<Exception> finalAction)
        {
            Exception exception = null;

            try
            {
                await actualReturnValue;
                await postAction();
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                finalAction(exception);
            }
        }

        /// <summary>
        /// �ȴ�����(ǰһ��Action,POST Action)��ɣ�����Finally����ִ��action
        /// </summary>
        /// <param name="actualReturnValue">ʵ�ʷ���ֵ</param>
        /// <param name="preAction">ǰһ������</param>
        /// <param name="postAction">POST���ͷ���</param>
        /// <param name="finalAction">���ִ�еķ���</param>
        /// <returns></returns>
        public static async Task AwaitTaskWithPreActionAndPostActionAndFinally(Func<Task> actualReturnValue, Func<Task> preAction = null, Func<Task> postAction = null, Action<Exception> finalAction = null)
        {
            Exception exception = null;

            try
            {
                if (preAction != null)
                {
                    await preAction();
                }

                await actualReturnValue();

                if (postAction != null)
                {
                    await postAction();
                }
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                if (finalAction != null)
                {
                    finalAction(exception);                    
                }
            }
        }

        /// <summary>
        /// �ȴ��������,���õ�����ֵ,����Finally����ִ��action
        /// </summary>
        /// <typeparam name="T">�������</typeparam>
        /// <param name="actualReturnValue">ʵ�ʷ���ֵ</param>
        /// <param name="finalAction">���ִ�еķ���</param>
        /// <returns></returns>
        public static async Task<T> AwaitTaskWithFinallyAndGetResult<T>(Task<T> actualReturnValue, Action<Exception> finalAction)
        {
            Exception exception = null;

            try
            {
                return await actualReturnValue;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                finalAction(exception);
            }
        }

        /// <summary>
        /// ���õȴ���ɵ�����,���õ�����ֵ,����Finally����ִ��action
        /// </summary>
        /// <param name="taskReturnType">���񷵻�����</param>
        /// <param name="actualReturnValue">ʵ�ʷ���ֵ</param>
        /// <param name="finalAction">���ִ�еķ���</param>
        /// <returns></returns>
        public static object CallAwaitTaskWithFinallyAndGetResult(Type taskReturnType, object actualReturnValue, Action<Exception> finalAction)
        {
            return typeof(InternalAsyncHelper)
                .GetMethod("AwaitTaskWithFinallyAndGetResult", BindingFlags.Public | BindingFlags.Static)
                .MakeGenericMethod(taskReturnType)
                .Invoke(null, new object[] { actualReturnValue, finalAction });
        }

        /// <summary>
        /// �ȴ�����(POST Action)���,����Finally����ִ��action,���ҵȴ����ؽ��
        /// </summary>
        /// <typeparam name="T">�������</typeparam>
        /// <param name="actualReturnValue">ʵ�ʷ���ֵ</param>
        /// <param name="postAction">POST���͵�Action</param>
        /// <param name="finalAction">���ִ�еķ���</param>
        /// <returns></returns>
        public static async Task<T> AwaitTaskWithPostActionAndFinallyAndGetResult<T>(Task<T> actualReturnValue, Func<Task> postAction, Action<Exception> finalAction)
        {
            Exception exception = null;

            try
            {
                var result = await actualReturnValue;
                await postAction();
                return result;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                finalAction(exception);
            }
        }

        /// <summary>
        /// ���õȴ�����(POST Action)���,����Finally����ִ��action,���ҵȴ����ؽ��
        /// </summary>
        /// <param name="taskReturnType">���񷵻�����</param>
        /// <param name="actualReturnValue">ʵ�ʷ���ֵ</param>
        /// <param name="action">�ȴ�ִ�е�action</param>
        /// <param name="finalAction">���ִ�еķ���</param>
        /// <returns></returns>
        public static object CallAwaitTaskWithPostActionAndFinallyAndGetResult(Type taskReturnType, object actualReturnValue, Func<Task> action, Action<Exception> finalAction)
        {
            return typeof (InternalAsyncHelper)
                .GetMethod("AwaitTaskWithPostActionAndFinallyAndGetResult", BindingFlags.Public | BindingFlags.Static)
                .MakeGenericMethod(taskReturnType)
                .Invoke(null, new object[] { actualReturnValue, action, finalAction });
        }

        /// <summary>
        /// �ȴ�����(Pre Action �Լ� Post Action)���,����Finally����ִ��action,���ҵȴ����ؽ��
        /// </summary>
        /// <typeparam name="T">�������</typeparam>
        /// <param name="actualReturnValue">ʵ�ʷ���ֵ</param>
        /// <param name="preAction">Pre Action</param>
        /// <param name="postAction">Post Action</param>
        /// <param name="finalAction">���ִ�еķ���</param>
        /// <returns></returns>
        public static async Task<T> AwaitTaskWithPreActionAndPostActionAndFinallyAndGetResult<T>(Func<Task<T>> actualReturnValue, Func<Task> preAction = null, Func<Task> postAction = null, Action<Exception> finalAction = null)
        {
            Exception exception = null;

            try
            {
                if (preAction != null)
                {
                    await preAction();
                }

                var result = await actualReturnValue();

                if (postAction != null)
                {
                    await postAction();                    
                }

                return result;
            }
            catch (Exception ex)
            {
                exception = ex;
                throw;
            }
            finally
            {
                if (finalAction != null)
                {
                    finalAction(exception);
                }
            }
        }

        /// <summary>
        /// ���õȴ�����(Pre Action �Լ� Post Action)���,����Finally����ִ��action,���ҵȴ����ؽ��
        /// </summary>
        /// <param name="taskReturnType">���񷵻�����</param>
        /// <param name="actualReturnValue">ʵ�ʷ���ֵ</param>
        /// <param name="preAction">Pre Action</param>
        /// <param name="postAction">Post Action</param>
        /// <param name="finalAction">���ִ�еķ���</param>
        /// <returns></returns>
        public static object CallAwaitTaskWithPreActionAndPostActionAndFinallyAndGetResult(Type taskReturnType, Func<object> actualReturnValue, Func<Task> preAction = null, Func<Task> postAction = null, Action<Exception> finalAction = null)
        {
            return typeof(InternalAsyncHelper)
                .GetMethod("AwaitTaskWithPreActionAndPostActionAndFinallyAndGetResult", BindingFlags.Public | BindingFlags.Static)
                .MakeGenericMethod(taskReturnType)
                .Invoke(null, new object[] { actualReturnValue, preAction, postAction, finalAction });
        }
    }
}