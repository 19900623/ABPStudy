using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.BackgroundJobs
{
    /// <summary>
    /// Null pattern implementation of <see cref="IBackgroundJobStore"/>.
    /// <see cref="IBackgroundJobStore"/>��NULLģʽʵ��
    /// It's used if <see cref="IBackgroundJobStore"/> is not implemented by actual persistent store
    /// ����ʹ�ã����<see cref="IBackgroundJobStore"/>û�б��־û��洢ʵ��
    /// and job execution is not enabled (<see cref="IBackgroundJobConfiguration.IsJobExecutionEnabled"/>) for the application.
    /// ����Ӧ�ó�����ҵ��ִ���ǽ��õ�(<see cref="IBackgroundJobConfiguration.IsJobExecutionEnabled"/>)��
    /// </summary>
    public class NullBackgroundJobStore : IBackgroundJobStore
    {
        /// <summary>
        /// �첽����һ����̨��ҵ
        /// </summary>
        /// <param name="jobInfo">��ҵ��Ϣ</param>
        /// <returns></returns>
        public Task InsertAsync(BackgroundJobInfo jobInfo)
        {
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
            return Task.FromResult(new List<BackgroundJobInfo>());
        }

        /// <summary>
        /// ɾ��һ����ҵ
        /// </summary>
        /// <param name="jobInfo">��ҵ��Ϣ</param>
        /// <returns></returns>
        public Task DeleteAsync(BackgroundJobInfo jobInfo)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// ����һ����ҵ
        /// </summary>
        /// <param name="jobInfo">��ҵ��Ϣ</param>
        /// <returns></returns>
        public Task UpdateAsync(BackgroundJobInfo jobInfo)
        {
            return Task.FromResult(0);
        }
    }
}