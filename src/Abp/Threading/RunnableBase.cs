namespace Abp.Threading
{
    /// <summary>
    /// Base implementation of <see cref="IRunnable"/>.
    /// <see cref="IRunnable"/>�Ļ���ʵ��
    /// </summary>
    public abstract class RunnableBase : IRunnable
    {
        /// <summary>
        /// A boolean value to control the running.
        /// ���Ʒ����Ƿ��������е�boolֵ
        /// </summary>
        public bool IsRunning { get { return _isRunning; } }

        private volatile bool _isRunning;

        /// <summary>
        /// ��������
        /// </summary>
        public virtual void Start()
        {
            _isRunning = true;
        }

        /// <summary>
        /// ����ֹͣ������񡣷���Ҳ���������ػ�ֹͣ�첽,�ͻ���Ӧ�õ���<see cref="WaitToStop"/>��������֤����ֹͣ
        /// </summary>
        public virtual void Stop()
        {
            _isRunning = false;
        }

        /// <summary>
        /// �ȴ�����ֹͣ
        /// </summary>
        public virtual void WaitToStop()
        {

        }
    }
}