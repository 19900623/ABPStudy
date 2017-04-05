using System;
using System.Threading.Tasks;
using Abp.Threading.BackgroundWorkers;

namespace Abp.BackgroundJobs
{
    //TODO: Create a non-generic EnqueueAsync extension method to IBackgroundJobManager which takes types as input parameters rather than generic parameters.
    /// <summary>
    /// Defines interface of a job manager.
    /// һ����ҵ������ӿ�
    /// </summary>
    public interface IBackgroundJobManager : IBackgroundWorker
    {
        /// <summary>
        /// Enqueues a job to be executed.
        /// ��ӵ���ҵ��ִ��
        /// </summary>
        /// <typeparam name="TJob">Type of the job. / ��ҵ������</typeparam>
        /// <typeparam name="TArgs">Type of the arguments of job. / ��ҵ����������</typeparam>
        /// <param name="args">Job arguments. / ��ҵ����</param>
        /// <param name="priority">Job priority. / ��ҵ����</param>
        /// <param name="delay">Job delay (wait duration before first try). / ��ҵ�ӳ�(����ǰ�ȴ����)</param>
        Task EnqueueAsync<TJob, TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
            where TJob : IBackgroundJob<TArgs>;
    }
}