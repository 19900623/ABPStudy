using System;
using System.Threading;
using Abp.Dependency;

namespace Abp.Threading.Timers
{
    /// <summary>
    /// A roboust timer implementation that ensures no overlapping occurs. It waits exactly specified <see cref="Period"/> between ticks.
    /// ABP��ܶ�ʱ��
    /// </summary>
    //TODO: Extract interface or make all members virtual to make testing easier.
    public class AbpTimer : RunnableBase, ITransientDependency
    {
        /// <summary>
        /// This event is raised periodically according to Period of Timer.
        /// ���ݼ�ʱ�����ڶ���������¼�
        /// </summary>
        public event EventHandler Elapsed;

        /// <summary>
        /// Task period of timer (as milliseconds).
        /// ��ʱ������������(����)
        /// </summary>
        public int Period { get; set; }

        /// <summary>
        /// Indicates whether timer raises Elapsed event on Start method of Timer for once.
        /// ��ʱ��һ������������ʱָʾ��ʱ���Ƿ��������¼�
        /// Default: False.
        /// </summary>
        public bool RunOnStart { get; set; }

        /// <summary>
        /// This timer is used to perfom the task at spesified intervals.
        /// �˼�ʱ��������ָ�����ִ������
        /// </summary>
        private readonly Timer _taskTimer;

        /// <summary>
        /// Indicates that whether timer is running or stopped.
        /// ָʾ��ʱ���Ƿ��������л�ֹͣ
        /// </summary>
        private volatile bool _running;

        /// <summary>
        /// Indicates that whether performing the task or _taskTimer is in sleep mode.
        /// ָʾ�Ƿ�ִ�������<see cref="_taskTimer"/>��˯��ģʽ
        /// This field is used to wait executing tasks when stopping Timer.
        /// ���ֶ�������ֹͣ��ʱ��ʱ�ȴ�ִ������
        /// </summary>
        private volatile bool _performingTasks;

        /// <summary>
        /// Creates a new Timer.
        /// ���캯��(����һ���µļ�ʱ��)
        /// </summary>
        public AbpTimer()
        {
            _taskTimer = new Timer(TimerCallBack, null, Timeout.Infinite, Timeout.Infinite);
        }

        /// <summary>
        /// Creates a new Timer.
        /// ���캯��(����һ���µļ�ʱ��)
        /// </summary>
        /// <param name="period">Task period of timer (as milliseconds) / ��ʱ������������(����)</param>
        /// <param name="runOnStart">Indicates whether timer raises Elapsed event on Start method of Timer for once / ��ʱ��һ������������ʱָʾ��ʱ���Ƿ��������¼�</param>
        public AbpTimer(int period, bool runOnStart = false)
            : this()
        {
            Period = period;
            RunOnStart = runOnStart;
        }

        /// <summary>
        /// Starts the timer.
        /// ������ʱ��
        /// </summary>
        public override void Start()
        {
            if (Period <= 0)
            {
                throw new AbpException("Period should be set before starting the timer!");
            }

            base.Start();

            _running = true;
            _taskTimer.Change(RunOnStart ? 0 : Period, Timeout.Infinite);
        }

        /// <summary>
        /// Stops the timer.
        /// ֹͣ��ʱ��
        /// </summary>
        public override void Stop()
        {
            lock (_taskTimer)
            {
                _running = false;
                _taskTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }

            base.Stop();
        }

        /// <summary>
        /// Waits the service to stop.
        /// �ȴ�����ֹͣ
        /// </summary>
        public override void WaitToStop()
        {
            lock (_taskTimer)
            {
                while (_performingTasks)
                {
                    Monitor.Wait(_taskTimer);
                }
            }

            base.WaitToStop();
        }

        /// <summary>
        /// This method is called by _taskTimer.
        /// ���������<see cref="_taskTimer"/>����
        /// </summary>
        /// <param name="state">Not used argument / ��ʹ�ò���</param>
        private void TimerCallBack(object state)
        {
            lock (_taskTimer)
            {
                if (!_running || _performingTasks)
                {
                    return;
                }

                _taskTimer.Change(Timeout.Infinite, Timeout.Infinite);
                _performingTasks = true;
            }

            try
            {
                if (Elapsed != null)
                {
                    Elapsed(this, new EventArgs());
                }
            }
            catch
            {

            }
            finally
            {
                lock (_taskTimer)
                {
                    _performingTasks = false;
                    if (_running)
                    {
                        _taskTimer.Change(Period, Timeout.Infinite);
                    }

                    Monitor.Pulse(_taskTimer);
                }
            }
        }
    }
}