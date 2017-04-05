using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Abp.Extensions;
using Abp.Runtime.Session;
using Castle.Core;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Base for all Unit Of Work classes.
    /// ������Ԫ����
    /// </summary>
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// �ⲿUOW�����ã��������
        /// </summary>
        [DoNotWire]
        public IUnitOfWork Outer { get; set; }

        /// <summary>
        /// ���¼��ڹ�����Ԫ�ɹ����ʱ������
        /// </summary>
        public event EventHandler Completed;

        /// <summary>
        /// ���¼��ڹ�����Ԫʧ��ʱ������
        /// </summary>
        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// ���¼��ڹ�����Ԫ����ʱ������
        /// </summary>
        public event EventHandler Disposed;

        /// <summary>
        /// ������Ԫѡ��
        /// </summary>
        public UnitOfWorkOptions Options { get; private set; }

        /// <summary>
        /// �ӹ�����Ԫ��ȡ���ݹ���������
        /// </summary>
        public IReadOnlyList<DataFilterConfiguration> Filters
        {
            get { return _filters.ToImmutableList(); }
        }
        private readonly List<DataFilterConfiguration> _filters;

        /// <summary>
        /// Gets default UOW options.
        /// ��ȡĬ�ϵ�UOWѡ��
        /// </summary>
        protected IUnitOfWorkDefaultOptions DefaultOptions { get; }

        /// <summary>
        /// Gets the connection string resolver.
        /// ��ȡ�����ַ���������
        /// </summary>
        protected IConnectionStringResolver ConnectionStringResolver { get; }

        /// <summary>
        /// Gets a value indicates that this unit of work is disposed or not.
        /// �˹�����Ԫ�Ƿ�����
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Reference to current ABP session.
        /// ��ǰABPϵͳSession����
        /// </summary>
        public IAbpSession AbpSession { protected get; set; }

        /// <summary>
        /// ������Ԫִ����
        /// </summary>
        protected IUnitOfWorkFilterExecuter FilterExecuter { get; }

        /// <summary>
        /// Is <see cref="Begin"/> method called before?
        /// <see cref="Begin"/> �����Ƿ��ѱ�����
        /// </summary>
        private bool _isBeginCalledBefore;

        /// <summary>
        /// Is <see cref="Complete"/> method called before?
        /// <see cref="Complete"/> �����Ƿ��ѱ�����
        /// </summary>
        private bool _isCompleteCalledBefore;

        /// <summary>
        /// Is this unit of work successfully completed.
        /// �˹�����Ԫ���Ƿ�ɹ����
        /// </summary>
        private bool _succeed;

        /// <summary>
        /// A reference to the exception if this unit of work failed.
        /// ���¹�����Ԫʧ�ܵ��쳣
        /// </summary>
        private Exception _exception;

        /// <summary>
        /// �⻧ID
        /// </summary>
        private int? _tenantId;

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        protected UnitOfWorkBase(
            IConnectionStringResolver connectionStringResolver, 
            IUnitOfWorkDefaultOptions defaultOptions,
            IUnitOfWorkFilterExecuter filterExecuter)
        {
            FilterExecuter = filterExecuter;
            DefaultOptions = defaultOptions;
            ConnectionStringResolver = connectionStringResolver;

            Id = Guid.NewGuid().ToString("N");
            _filters = defaultOptions.Filters.ToList();

            AbpSession = NullAbpSession.Instance;
        }

        /// <summary>
        /// ʹ�ø�����ѡ�����һ��������Ԫ
        /// </summary>
        /// <param name="options">������Ԫѡ��</param>
        public void Begin(UnitOfWorkOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            PreventMultipleBegin();
            Options = options; //TODO: Do not set options like that, instead make a copy?

            SetFilters(options.FilterOverrides);

            SetTenantId(AbpSession.TenantId);

            BeginUow();
        }

        /// <summary>
        /// ���ڣ����湤����Ԫ�����е��޸ġ�
        /// �����������ҪӦ���޸�ʱ���á�
        /// ע�⣬���������Ԫ�������Եģ�����������������������ʧ��ʱ���ع�����
        /// һ�㲻����ʽ����SaveChages����Ϊ������Ԫ���Զ��������б��
        /// </summary>
        public abstract void SaveChanges();

        /// <summary>
        /// ���ڣ����湤����Ԫ�����е��޸ġ�
        /// �����������ҪӦ���޸�ʱ���á�
        /// ע�⣬���������Ԫ�������Եģ�����������������������ʧ��ʱ���ع�����
        /// һ�㲻����ʽ����SaveChages����Ϊ������Ԫ���Զ��������б��
        /// </summary>
        public abstract Task SaveChangesAsync();

        /// <summary>
        /// ����һ���������ݹ�����
        /// ���һ�������������ã��������������κβ���
        /// ʹ��using�����ʹ�ø÷�������������Ҫʱ���ù�����
        /// </summary>
        /// <param name="filterNames">һ��������׼������������</param>
        /// <returns>�ָܻ�����ǰЧ���Ķ���</returns>
        public IDisposable DisableFilter(params string[] filterNames)
        {
            //TODO: Check if filters exists?

            var disabledFilters = new List<string>();

            foreach (var filterName in filterNames)
            {
                var filterIndex = GetFilterIndex(filterName);
                if (_filters[filterIndex].IsEnabled)
                {
                    disabledFilters.Add(filterName);
                    _filters[filterIndex] = new DataFilterConfiguration(_filters[filterIndex], false);
                }
            }

            disabledFilters.ForEach(ApplyDisableFilter);

            return new DisposeAction(() => EnableFilter(disabledFilters.ToArray()));
        }

        /// <summary>
        /// ����һ���������ݹ�����
        /// ���һ�������������ã��������������κβ���
        /// ʹ��using�����ʹ�ø÷�������������Ҫʱ���ù�����
        /// </summary>
        /// <param name="filterNames">һ��������׼������������</param>
        /// <returns>�ָܻ�����ǰЧ���Ķ���</returns>
        public IDisposable EnableFilter(params string[] filterNames)
        {
            //TODO: Check if filters exists?

            var enabledFilters = new List<string>();

            foreach (var filterName in filterNames)
            {
                var filterIndex = GetFilterIndex(filterName);
                if (!_filters[filterIndex].IsEnabled)
                {
                    enabledFilters.Add(filterName);
                    _filters[filterIndex] = new DataFilterConfiguration(_filters[filterIndex], true);
                }
            }

            enabledFilters.ForEach(ApplyEnableFilter);

            return new DisposeAction(() => DisableFilter(enabledFilters.ToArray()));
        }

        /// <summary>
        /// ���һ���������Ƿ񱻽���
        /// </summary>
        /// <param name="filterName">������������</param>
        /// <returns></returns>
        public bool IsFilterEnabled(string filterName)
        {
            return GetFilter(filterName).IsEnabled;
        }

        /// <summary>
        /// ���ã����أ�����������������
        /// </summary>
        /// <param name="filterName">������������</param>
        /// <param name="parameterName">��������</param>
        /// <param name="value">Ҫ���������õ�ֵ</param>
        /// <returns></returns>
        public IDisposable SetFilterParameter(string filterName, string parameterName, object value)
        {
            var filterIndex = GetFilterIndex(filterName);

            var newfilter = new DataFilterConfiguration(_filters[filterIndex]);

            //Store old value / �洢��ֵ
            object oldValue = null;
            var hasOldValue = newfilter.FilterParameters.ContainsKey(parameterName);
            if (hasOldValue)
            {
                oldValue = newfilter.FilterParameters[parameterName];
            }

            newfilter.FilterParameters[parameterName] = value;

            _filters[filterIndex] = newfilter;

            ApplyFilterParameterValue(filterName, parameterName, value);

            return new DisposeAction(() =>
            {
                //Restore old value
                if (hasOldValue)
                {
                    SetFilterParameter(filterName, parameterName, oldValue);
                }
            });
        }

        /// <summary>
        /// �����⻧ID
        /// </summary>
        /// <param name="tenantId">�⻧ID</param>
        /// <returns></returns>
        public IDisposable SetTenantId(int? tenantId)
        {
            var oldTenantId = _tenantId;
            _tenantId = tenantId;

            var mustHaveTenantEnableChange = tenantId == null
                ? DisableFilter(AbpDataFilters.MustHaveTenant)
                : EnableFilter(AbpDataFilters.MustHaveTenant);

            var mayHaveTenantChange = SetFilterParameter(AbpDataFilters.MayHaveTenant, AbpDataFilters.Parameters.TenantId, tenantId);
            var mustHaveTenantChange = SetFilterParameter(AbpDataFilters.MustHaveTenant, AbpDataFilters.Parameters.TenantId, tenantId ?? 0);

            return new DisposeAction(() =>
            {
                mayHaveTenantChange.Dispose();
                mustHaveTenantChange.Dispose();
                mustHaveTenantEnableChange.Dispose();
                _tenantId = oldTenantId;
            });
        }

        /// <summary>
        /// ��ȡ�⻧ID
        /// </summary>
        /// <returns></returns>
        public int? GetTenantId()
        {
            return _tenantId;
        }

        /// <summary>
        /// �����˹�����Ԫ
        /// �������еĸ��䣬��������񣬲��ύ����
        /// </summary>
        public void Complete()
        {
            PreventMultipleComplete();
            try
            {
                CompleteUow();
                _succeed = true;
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        /// <summary>
        /// �����˹�����Ԫ-�첽
        /// �������еĸ��䣬��������񣬲��ύ����
        /// </summary>
        public async Task CompleteAsync()
        {
            PreventMultipleComplete();
            try
            {
                await CompleteUowAsync();
                _succeed = true;
                OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        /// <summary>
        /// ���ٹ�����Ԫ
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            if (!_succeed)
            {
                OnFailed(_exception);
            }

            DisposeUow();
            OnDisposed();
        }

        /// <summary>
        /// Can be implemented by derived classes to start UOW.
        /// ��Ҫ����������ʵ�֣��Ա㿪ʼ������Ԫ
        /// </summary>
        protected virtual void BeginUow()
        {
            
        }

        /// <summary>
        /// Should be implemented by derived classes to complete UOW.
        /// ��Ҫ����������ʵ�֣��Ա���ɹ�����Ԫ
        /// </summary>
        protected abstract void CompleteUow();

        /// <summary>
        /// Should be implemented by derived classes to complete UOW.
        /// ��Ҫ����������ʵ�֣��Ա���ɹ�����Ԫ - �첽
        /// </summary>
        protected abstract Task CompleteUowAsync();

        /// <summary>
        /// Should be implemented by derived classes to dispose UOW.
        /// ��Ҫ����������ʵ�֣��Ա����ٹ�����Ԫ
        /// </summary>
        protected abstract void DisposeUow();

        /// <summary>
        /// ���ڽ��ù�����
        /// </summary>
        /// <param name="filterName">����������</param>
        protected virtual void ApplyDisableFilter(string filterName)
        {
            FilterExecuter.ApplyDisableFilter(this, filterName);
        }

        /// <summary>
        /// �������ù�����
        /// </summary>
        /// <param name="filterName">����������</param>
        protected virtual void ApplyEnableFilter(string filterName)
        {
            FilterExecuter.ApplyEnableFilter(this, filterName);
        }

        /// <summary>
        /// ���ڹ���������ֵ
        /// </summary>
        /// <param name="filterName">����������</param>
        /// <param name="parameterName">������</param>
        /// <param name="value">�û����ò�����ֵ</param>
        protected virtual void ApplyFilterParameterValue(string filterName, string parameterName, object value)
        {
            FilterExecuter.ApplyFilterParameterValue(this, filterName, parameterName, value);
        }

        /// <summary>
        /// ���������ַ���
        /// </summary>
        /// <param name="args">�ַ�����������</param>
        /// <returns></returns>
        protected virtual string ResolveConnectionString(ConnectionStringResolveArgs args)
        {
            return ConnectionStringResolver.GetNameOrConnectionString(args);
        }

        /// <summary>
        /// Called to trigger <see cref="Completed"/> event.
        /// ���ô��� <see cref="Completed"/> �¼�
        /// </summary>
        protected virtual void OnCompleted()
        {
            Completed.InvokeSafely(this);
        }

        /// <summary>
        /// Called to trigger <see cref="Failed"/> event.
        /// ���ô��� <see cref="Failed"/> �¼�
        /// </summary>
        /// <param name="exception">Exception that cause failure / ����ʧ�ܵ��쳣</param>
        protected virtual void OnFailed(Exception exception)
        {
            Failed.InvokeSafely(this, new UnitOfWorkFailedEventArgs(exception));
        }

        /// <summary>
        /// Called to trigger <see cref="Disposed"/> event.
        /// ���ô��� <see cref="Disposed"/> �¼�
        /// </summary>
        protected virtual void OnDisposed()
        {
            Disposed.InvokeSafely(this);
        }

        /// <summary>
        /// ��ֹ��ε���Begin
        /// </summary>
        private void PreventMultipleBegin()
        {
            if (_isBeginCalledBefore)
            {
                throw new AbpException("This unit of work has started before. Can not call Start method more than once.");
            }

            _isBeginCalledBefore = true;
        }

        /// <summary>
        /// ��ֹ��ε���Complete
        /// </summary>
        private void PreventMultipleComplete()
        {
            if (_isCompleteCalledBefore)
            {
                throw new AbpException("Complete is called before!");
            }

            _isCompleteCalledBefore = true;
        }

        /// <summary>
        /// ���ù�����
        /// </summary>
        /// <param name="filterOverrides">���ݹ����������б�</param>
        private void SetFilters(List<DataFilterConfiguration> filterOverrides)
        {
            for (var i = 0; i < _filters.Count; i++)
            {
                var filterOverride = filterOverrides.FirstOrDefault(f => f.FilterName == _filters[i].FilterName);
                if (filterOverride != null)
                {
                    _filters[i] = filterOverride;
                }
            }

            if (AbpSession.TenantId == null)
            {
                ChangeFilterIsEnabledIfNotOverrided(filterOverrides, AbpDataFilters.MustHaveTenant, false);
            }
        }

        /// <summary>
        /// ���û����д���������ã��ı��������״̬���Ƿ񼤻
        /// </summary>
        /// <param name="filterOverrides">���ݹ������б�</param>
        /// <param name="filterName">����������</param>
        /// <param name="isEnabled">�Ƿ�����</param>
        private void ChangeFilterIsEnabledIfNotOverrided(List<DataFilterConfiguration> filterOverrides, string filterName, bool isEnabled)
        {
            if (filterOverrides.Any(f => f.FilterName == filterName))
            {
                return;
            }

            var index = _filters.FindIndex(f => f.FilterName == filterName);
            if (index < 0)
            {
                return;
            }

            if (_filters[index].IsEnabled == isEnabled)
            {
                return;
            }

            _filters[index] = new DataFilterConfiguration(filterName, isEnabled);
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="filterName">����������</param>
        /// <returns></returns>
        private DataFilterConfiguration GetFilter(string filterName)
        {
            var filter = _filters.FirstOrDefault(f => f.FilterName == filterName);
            if (filter == null)
            {
                throw new AbpException("Unknown filter name: " + filterName + ". Be sure this filter is registered before.");
            }

            return filter;
        }

        /// <summary>
        /// ��ȡ����������
        /// </summary>
        /// <param name="filterName">����������</param>
        /// <returns></returns>
        private int GetFilterIndex(string filterName)
        {
            var filterIndex = _filters.FindIndex(f => f.FilterName == filterName);
            if (filterIndex < 0)
            {
                throw new AbpException("Unknown filter name: " + filterName + ". Be sure this filter is registered before.");
            }

            return filterIndex;
        }
    }
}