using Abp.BackgroundJobs;
using Abp.Modules;
using Hangfire;
using HangfireGlobalConfiguration = Hangfire.GlobalConfiguration;

namespace Abp.Hangfire.Configuration
{
    /// <summary>
    /// <see cref="IAbpHangfireConfiguration"/>ʵ��
    /// </summary>
    public class AbpHangfireConfiguration : IAbpHangfireConfiguration
    {
        /// <summary>
        /// ��ȡ������Hangfire��<see cref="BackgroundJobServer"/>����
        /// ʵ�֣������<see cref="AbpModule.PreInitialize"/>��Ϊnull������Դ��������������Զ������Ĵ�����
        /// ����㲻��������������ͨ��Abp.Hangfireģ���Ĭ�Ϲ��캯��<see cref="AbpModule.PreInitialize"/>���Զ����ã�
        /// �����̨��ҵ��ִ��ʱ���õģ��鿴(see <see cref="IBackgroundJobConfiguration.IsJobExecutionEnabled"/>)
        /// ��ˣ�������Լ����������������μ���̨��ҵִ���Ƿ����á�(see <see cref="IBackgroundJobConfiguration.IsJobExecutionEnabled"/>).
        /// </summary>
        public BackgroundJobServer Server { get; set; }

        /// <summary>
        /// Hangfire��ȫ������
        /// </summary>
        public IGlobalConfiguration GlobalConfiguration
        {
            get { return HangfireGlobalConfiguration.Configuration; }
        }
    }
}