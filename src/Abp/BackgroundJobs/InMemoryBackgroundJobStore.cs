using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Abp.Timing;

namespace Abp.BackgroundJobs
{
    /// <summary>
    /// In memory implementation of <see cref="IBackgroundJobStore"/>.
    /// ���ڴ���ʵ��<see cref="IBackgroundJobStore"/>
    /// It's used if <see cref="IBackgroundJobStore"/> is not implemented by actual persistent store
    /// ����ʹ�ã����<see cref="IBackgroundJobStore"/>û�б��־û��洢ʵ��
    /// and job execution is enabled (<see cref="IBackgroundJobConfiguration.IsJobExecutionEnabled"/>) for the application.
    /// ����Ӧ�ó�����ҵ��ִ�������õ�(<see cref="IBackgroundJobConfiguration.IsJobExecutionEnabled"/>)��
    /// </summary>
    public class InMemoryBackgroundJobStore : IBackgroundJobStore
    {
        /// <summary>
        /// ��̨��ҵ��Ϣ�ֵ�洢
        /// </summary>
        private readonly Dictionary<long, BackgroundJobInfo> _jobs;
        private long _lastId;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryBackgroundJobStore"/> class.
        /// ���캯��(��ʼ��<see cref="InMemoryBackgroundJobStore"/>���µ�ʵ��)
        /// </summary>
        public InMemoryBackgroundJobStore()
        {
            _jobs = new Dictionary<long, BackgroundJobInfo>();
        }

        /// <summary>
        /// �첽����һ����̨��ҵ
        /// </summary>
        /// <param name="jobInfo">��ҵ��Ϣ</param>
        /// <returns></returns>
        public Task InsertAsync(BackgroundJobInfo jobInfo)
        {
            jobInfo.Id = Interlocked.Increment(ref _lastId);
            _jobs[jobInfo.Id] = jobInfo;

            return Task.FromResult(0);
        }

        /// <summary>
        /// ��ȡ�ȴ���ҵ.��Ӧ�û������»�ȡ:
        /// ����:!IsAbandoned And NextTryTime С�ڵ��� Clock.Now.
        /// ����:Priority DESC, TryCount ASC, NextTryTime ASC.
        /// �����:<paramref name="maxResultCount"/>.
        /// </summary>
        /// <param name="maxResultCount">���������</param>
        /// <returns></returns>
        public Task<List<BackgroundJobInfo>> GetWaitingJobsAsync(int maxResultCount)
        {
            var waitingJobs = _jobs.Values
                .Where(t => !t.IsAbandoned && t.NextTryTime <= Clock.Now)
                .OrderByDescending(t => t.Priority)
                .ThenBy(t => t.TryCount)
                .ThenBy(t => t.NextTryTime)
                .Take(maxResultCount)
                .ToList();

            return Task.FromResult(waitingJobs);
        }

        /// <summary>
        /// ɾ��һ����ҵ
        /// </summary>
        /// <param name="jobInfo">��ҵ��Ϣ</param>
        /// <returns></returns>
        public Task DeleteAsync(BackgroundJobInfo jobInfo)
        {
            if (!_jobs.ContainsKey(jobInfo.Id))
            {
                return Task.FromResult(0);
            }

            _jobs.Remove(jobInfo.Id);

            return Task.FromResult(0);
        }

        /// <summary>
        /// ����һ����ҵ
        /// </summary>
        /// <param name="jobInfo">��ҵ��Ϣ</param>
        /// <returns></returns>
        public Task UpdateAsync(BackgroundJobInfo jobInfo)
        {
            if (jobInfo.IsAbandoned)
            {
                return DeleteAsync(jobInfo);
            }

            return Task.FromResult(0);
        }
    }
}