using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Abp.Logging;

namespace Abp.Configuration
{
    /// <summary>
    /// Implements default behavior for ISettingStore.Only <see cref="GetSettingOrNullAsync"/> method is implemented and it gets setting's value from application's configuration file if exists, or returns null if not.
    /// �ӿ�IsettingStroe��Ĭ��ʵ�֣�ֻʵ����<see cref="GetSettingOrNullAsync"/>������������������ļ�,����Ӧ�õ������ļ��л�ȡ����ֵ,���򷵻�null;
    /// </summary>
    public class DefaultConfigSettingStore : ISettingStore
    {
        /// <summary>
        /// Gets singleton instance.
        /// ��������
        /// </summary>
        public static DefaultConfigSettingStore Instance { get { return SingletonInstance; } }
        private static readonly DefaultConfigSettingStore SingletonInstance = new DefaultConfigSettingStore();

        /// <summary>
        /// ���캯��
        /// </summary>
        private DefaultConfigSettingStore()
        {
        }

        /// <summary>
        /// �첽��ȡ����
        /// </summary>
        /// <param name="tenantId">�⻧ID</param>
        /// <param name="userId">�û�ID</param>
        /// <param name="name">����</param>
        /// <returns></returns>
        public Task<SettingInfo> GetSettingOrNullAsync(int? tenantId, long? userId, string name)
        {
            var value = ConfigurationManager.AppSettings[name];

            if (value == null)
            {
                return Task.FromResult<SettingInfo>(null);
            }

            return Task.FromResult(new SettingInfo(tenantId, userId, name, value));
        }
#pragma warning disable 1998
        /// <summary>
        /// �첽ɾ��
        /// </summary>
        /// <param name="setting">������Ϣ</param>
        /// <returns></returns>
        public async Task DeleteAsync(SettingInfo setting)
        {
            //TODO: Call should be async and use await
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support DeleteAsync.");
        }
#pragma warning restore 1998

#pragma warning disable 1998
        /// <summary>
        /// �첽����
        /// </summary>
        /// <param name="setting">������Ϣ</param>
        /// <returns></returns>
        public async Task CreateAsync(SettingInfo setting)
        {
            //TODO: Call should be async and use await
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support CreateAsync.");
        }
#pragma warning restore 1998

#pragma warning disable 1998
        /// <summary>
        /// �첽����
        /// </summary>
        /// <param name="setting">������Ϣ</param>
        /// <returns></returns>
        public async Task UpdateAsync(SettingInfo setting)
        {
            //TODO: Call should be async and use await
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support UpdateAsync.");
        }
#pragma warning restore 1998

        /// <summary>
        /// �첽��ȡ����Ԫ��
        /// </summary>
        /// <param name="tenantId">�⻧ID</param>
        /// <param name="userId">�û�ID</param>
        /// <returns></returns>
        public Task<List<SettingInfo>> GetAllListAsync(int? tenantId, long? userId)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support GetAllListAsync.");
            return Task.FromResult(new List<SettingInfo>());
        }
    }
}