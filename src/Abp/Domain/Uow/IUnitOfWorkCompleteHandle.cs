using System;
using System.Threading.Tasks;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Used to complete a unit of work.This interface can not be injected or directly used.Use <see cref="IUnitOfWorkManager"/> instead.
    /// �������һ��������Ԫ,�˽ӿڲ��ܱ�ע���ֱ��ʹ�á�ʹ�� <see cref="IUnitOfWorkManager"/> ����.
    /// </summary>
    public interface IUnitOfWorkCompleteHandle : IDisposable
    {
        /// <summary>
        /// Completes this unit of work.It saves all changes and commit transaction if exists.
        /// ��ɴ˹�����Ԫ,�������еĸ��䣬��������񣬲��ύ����
        /// </summary>
        void Complete();

        /// <summary>
        /// Completes this unit of work.It saves all changes and commit transaction if exists.
        /// ��ɴ˹�����Ԫ-�첽���������еĸ��䣬��������񣬲��ύ����
        /// </summary>
        Task CompleteAsync();
    }
}