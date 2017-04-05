using Abp.Configuration;
using Castle.DynamicProxy;

namespace Abp.Dependency
{
    /// <summary>
    /// This class is used to pass configuration/options while registering classes in conventional way.
    /// ��������ڣ���ʹ��Լ��ע����ʱ��������/ѡ��
    /// </summary>
    public class ConventionalRegistrationConfig : DictionaryBasedConfig
    {
        /// <summary>
        /// Install all <see cref="IInterceptor"/> implementations automatically or not.Default: true. 
        /// �Ƿ��Զ���װ���е� <see cref="IInterceptor"/> ʵ��.Ĭ��: ��. 
        /// </summary>
        public bool InstallInstallers { get; set; }

        /// <summary>
        /// Creates a new <see cref="ConventionalRegistrationConfig"/> object.
        /// ����һ���µ� <see cref="ConventionalRegistrationConfig"/> ����.
        /// </summary>
        public ConventionalRegistrationConfig()
        {
            InstallInstallers = true;
        }
    }
}