using System;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Defines a unit of work.This interface is internally used by ABP.
    /// ����һ��������Ԫ,�˽ӿ���ABP�ڲ�ʹ��
    /// Use <see cref="IUnitOfWorkManager.Begin()"/> to start a new unit of work.
    /// ʹ�� <see cref="IUnitOfWorkManager"/> ����һ���µĹ�����Ԫ
    /// </summary>
    public interface IUnitOfWork : IActiveUnitOfWork, IUnitOfWorkCompleteHandle
    {
        /// <summary>
        /// Unique id of this UOW.
        /// UOWΨһ��ID
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Reference to the outer UOW if exists.
        /// �ⲿUOW�����ã��������
        /// </summary>
        IUnitOfWork Outer { get; set; }
        
        /// <summary>
        /// Begins the unit of work with given options.
        /// ʹ�ø�����ѡ���������Ԫ
        /// </summary>
        /// <param name="options">Unit of work options</param>
        void Begin(UnitOfWorkOptions options);
    }
}