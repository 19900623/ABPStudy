using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.BackgroundJobs
{
    /// <summary>
    /// Defines interface to store/get background jobs.
    /// �洢/��ȡ ��̨��ҵ�Ľӿ�
    /// </summary>
    public interface IBackgroundJobStore
    {
        /// <summary>
        /// Inserts a background job.
        /// �첽����һ����̨��ҵ
        /// </summary>
        /// <param name="jobInfo">Job information. / ��ҵ��Ϣ</param>
        Task InsertAsync(BackgroundJobInfo jobInfo);

        /// <summary>
        /// Gets waiting jobs. It should get jobs based on these:
        /// ��ȡ�ȴ���ҵ.��Ӧ�û������»�ȡ:
        /// Conditions: !IsAbandoned And NextTryTime &lt;= Clock.Now.
        /// ����:!IsAbandoned And NextTryTime С�ڵ��� Clock.Now.
        /// Order by: Priority DESC, TryCount ASC, NextTryTime ASC.
        /// ����:Priority DESC, TryCount ASC, NextTryTime ASC.
        /// Maximum result: <paramref name="maxResultCount"/>.
        /// �����:<paramref name="maxResultCount"/>.
        /// </summary>
        /// <param name="maxResultCount">Maximum result count. / ���������</param>
        Task<List<BackgroundJobInfo>> GetWaitingJobsAsync(int maxResultCount);

        /// <summary>
        /// Deletes a job.
        /// ɾ��һ����ҵ
        /// </summary>
        /// <param name="jobInfo">Job information. / ��ҵ��Ϣ</param>
        Task DeleteAsync(BackgroundJobInfo jobInfo);

        /// <summary>
        /// Updates a job.
        /// ����һ����ҵ
        /// </summary>
        /// <param name="jobInfo">Job information. / ��ҵ��Ϣ</param>
        Task UpdateAsync(BackgroundJobInfo jobInfo);
    }
}