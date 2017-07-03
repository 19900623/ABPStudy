using System.Security.Claims;
using Abp.MultiTenancy;

namespace Abp.Authorization.Users
{
    /// <summary>
    /// ABP��¼���
    /// </summary>
    /// <typeparam name="TTenant"></typeparam>
    /// <typeparam name="TUser"></typeparam>
    public class AbpLoginResult<TTenant, TUser>
        where TTenant : AbpTenant<TUser>
        where TUser : AbpUser<TUser>
    {
        /// <summary>
        /// ��¼�������
        /// </summary>
        public AbpLoginResultType Result { get; private set; }
        /// <summary>
        /// �̻�
        /// </summary>
        public TTenant Tenant { get; private set; }
        /// <summary>
        /// �û�
        /// </summary>
        public TUser User { get; private set; }
        /// <summary>
        /// �û��ı�ʶ
        /// </summary>
        public ClaimsIdentity Identity { get; private set; }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="result">��¼�������</param>
        /// <param name="tenant">�̻�</param>
        /// <param name="user">�û�</param>
        public AbpLoginResult(AbpLoginResultType result, TTenant tenant = null, TUser user = null)
        {
            Result = result;
            Tenant = tenant;
            User = user;
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="tenant">�̻�</param>
        /// <param name="user">�û�</param>
        /// <param name="identity">�û��ı�ʶ</param>
        public AbpLoginResult(TTenant tenant, TUser user, ClaimsIdentity identity)
            : this(AbpLoginResultType.Success, tenant)
        {
            User = user;
            Identity = identity;
        }
    }
}