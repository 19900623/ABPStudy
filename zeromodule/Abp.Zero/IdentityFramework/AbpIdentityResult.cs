using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace Abp.IdentityFramework
{
    /// <summary>
    /// ABP ��ʶ���
    /// </summary>
    public class AbpIdentityResult : IdentityResult
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public AbpIdentityResult()
        {
            
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="errors"></param>
        public AbpIdentityResult(IEnumerable<string> errors)
            : base(errors)
        {
            
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="errors"></param>
        public AbpIdentityResult(params string[] errors)
            :base(errors)
        {
            
        }
        /// <summary>
        /// ʧ�ܽ��
        /// </summary>
        /// <param name="errors">������Ϣ����</param>
        /// <returns></returns>
        public static AbpIdentityResult Failed(params string[] errors)
        {
            return new AbpIdentityResult(errors);
        }
    }
}