using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// This interface is used to work with active unit of work.This interface can not be injected.Use <see cref="IUnitOfWorkManager"/> instead.
    /// �˽ӿ������������Ԫ���˽ӿڲ��ܱ�ע�롣ʹ�� <see cref="IUnitOfWorkManager"/> ����.
    /// </summary>
    public interface IActiveUnitOfWork
    {
        /// <summary>
        /// This event is raised when this UOW is successfully completed.
        /// ���¼��ڹ�����Ԫ�ɹ����ʱ������
        /// </summary>
        event EventHandler Completed;

        /// <summary>
        /// This event is raised when this UOW is failed.
        /// ���¼��ڹ�����Ԫʧ��ʱ������
        /// </summary>
        event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// This event is raised when this UOW is disposed.
        /// ���¼��ڹ�����Ԫ����ʱ������
        /// </summary>
        event EventHandler Disposed;

        /// <summary>
        /// Gets if this unit of work is transactional.
        /// ��ȡ������Ԫ�Ƿ��������Ե�
        /// </summary>
        UnitOfWorkOptions Options { get; }

        /// <summary>
        /// Gets data filter configurations for this unit of work.
        /// �ӹ�����Ԫ��ȡ���ݹ���������
        /// </summary>
        IReadOnlyList<DataFilterConfiguration> Filters { get; }

        /// <summary>
        /// Is this UOW disposed?
        /// �˹�����Ԫ�Ƿ�����
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// Saves all changes until now in this unit of work.
        /// ���湤����Ԫ�����е��޸�
        /// This method may be called to apply changes whenever needed.
        /// �����������ҪӦ���޸�ʱ����
        /// Note that if this unit of work is transactional, saved changes are also rolled back if transaction is rolled back.
        /// No explicit call is needed to SaveChanges generally, 
        /// ע�⣬���������Ԫ�������Եģ�����������������������ʧ��ʱ���ع�����
        /// since all changes saved at end of a unit of work automatically.
        /// һ�㲻����ʽ����SaveChages����Ϊ������Ԫ���Զ��������б��
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Saves all changes until now in this unit of work.
        /// ���湤����Ԫ�����е��޸�
        /// This method may be called to apply changes whenever needed.
        /// �����������ҪӦ���޸�ʱ����
        /// Note that if this unit of work is transactional, saved changes are also rolled back if transaction is rolled back.
        /// No explicit call is needed to SaveChanges generally, 
        /// ע�⣬���������Ԫ�������Եģ�����������������������ʧ��ʱ���ع�����
        /// since all changes saved at end of a unit of work automatically.
        /// һ�㲻����ʽ����SaveChages����Ϊ������Ԫ���Զ��������б��
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// Disables one or more data filters.
        /// ����һ���������ݹ�����
        /// Does nothing for a filter if it's already disabled. 
        /// ���һ�������������ã��������������κβ���
        /// Use this method in a using statement to re-enable filters if needed.
        /// ʹ��using�����ʹ�ø÷�������������Ҫʱ���ù�����
        /// </summary>
        /// <param name="filterNames">One or more filter names. <see cref="AbpDataFilters"/> for standard filters. / һ��������׼������������</param>
        /// <returns>A <see cref="IDisposable"/> handle to take back the disable effect. / ���ջؽ���Ч��</returns>
        IDisposable DisableFilter(params string[] filterNames);

        /// <summary>
        /// Enables one or more data filters.
        /// ����һ���������ݹ�����
        /// Does nothing for a filter if it's already enabled.
        /// ���һ�������������ã��������������κβ���
        /// Use this method in a using statement to re-disable filters if needed.
        /// ʹ��using�����ʹ�ø÷�������������Ҫʱ���ù�����
        /// </summary>
        /// <param name="filterNames">One or more filter names. <see cref="AbpDataFilters"/> for standard filters. / һ��������׼������������</param>
        /// <returns>A <see cref="IDisposable"/> handle to take back the enable effect. / ���ջ�����Ч��</returns>
        IDisposable EnableFilter(params string[] filterNames);

        /// <summary>
        /// Checks if a filter is enabled or not.
        /// ���һ���������Ƿ񱻽���
        /// </summary>
        /// <param name="filterName">Name of the filter. <see cref="AbpDataFilters"/> for standard filters. / <see cref="AbpDataFilters"/>������������</param>
        bool IsFilterEnabled(string filterName);

        /// <summary>
        /// Sets (overrides) value of a filter parameter.
        /// ���ã����أ�����������������
        /// </summary>
        /// <param name="filterName">Name of the filter / ����������</param>
        /// <param name="parameterName">Parameter's name / ��������</param>
        /// <param name="value">Value of the parameter to be set / Ҫ���������õ�ֵ</param>
        IDisposable SetFilterParameter(string filterName, string parameterName, object value);

        /// <summary>
        /// Sets/Changes Tenant's Id for this UOW.
        /// ����/�޸ĵ�ǰ������Ԫ�⻧��ID
        /// </summary>
        /// <param name="tenantId">The tenant id. / �⻧ID</param>
        IDisposable SetTenantId(int? tenantId);

        /// <summary>
        /// Gets Tenant Id for this UOW.
        /// ��ȡ��ǰ������Ԫ�⻧��ID
        /// </summary>
        /// <returns></returns>
        int? GetTenantId();
    }
}