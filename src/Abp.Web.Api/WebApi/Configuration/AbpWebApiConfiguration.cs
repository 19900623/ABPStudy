using System.Collections.Generic;
using System.Web.Http;
using Abp.Domain.Uow;
using Abp.Web.Models;
using Abp.WebApi.Controllers.Dynamic.Builders;

namespace Abp.WebApi.Configuration
{
    /// <summary>
    /// ��������ABP WebApiģ��
    /// </summary>
    internal class AbpWebApiConfiguration : IAbpWebApiConfiguration
    {
        /// <summary>
        /// ����Action��Ĭ�Ϲ�����Ԫ���
        /// </summary>
        public UnitOfWorkAttribute DefaultUnitOfWorkAttribute { get; }

        /// <summary>
        /// ����Action��Ĭ��Result��װ���
        /// </summary>
        public WrapResultAttribute DefaultWrapResultAttribute { get; }

        /// <summary>
        /// Ϊ���ж�̬web api action��Ĭ��Result��װ���
        /// </summary>
        public WrapResultAttribute DefaultDynamicApiWrapResultAttribute { get; }

        /// <summary>
        /// ���԰�װ�����Url�б�
        /// </summary>
        public List<string> ResultWrappingIgnoreUrls { get; }

        /// <summary>
        /// ��ȡ/���� <see cref="HttpConfiguration"/>
        /// </summary>
        public HttpConfiguration HttpConfiguration { get; set; }

        /// <summary>
        /// ���п������Ƿ���֤���á�Ĭ�ϣ�true
        /// </summary>
        public bool IsValidationEnabledForControllers { get; set; }

        /// <summary>
        /// �Ƿ��Զ���α��֤���á�Ĭ�ϣ�true
        /// </summary>
        public bool IsAutomaticAntiForgeryValidationEnabled { get; set; }

        /// <summary>
        /// Ϊ���е���Ӧ������Cache��Ĭ�ϣ�true
        /// </summary>
        public bool SetNoCacheForAllResponses { get; set; }

        /// <summary>
        /// �������ö�̬Web API������
        /// </summary>
        public IDynamicApiControllerBuilder DynamicApiControllerBuilder { get; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="dynamicApiControllerBuilder">��̬API������������</param>
        public AbpWebApiConfiguration(IDynamicApiControllerBuilder dynamicApiControllerBuilder)
        {
            DynamicApiControllerBuilder = dynamicApiControllerBuilder;

            HttpConfiguration = GlobalConfiguration.Configuration;
            DefaultUnitOfWorkAttribute = new UnitOfWorkAttribute();
            DefaultWrapResultAttribute = new WrapResultAttribute(false);
            DefaultDynamicApiWrapResultAttribute = new WrapResultAttribute();
            ResultWrappingIgnoreUrls = new List<string>();
            IsValidationEnabledForControllers = true;
            IsAutomaticAntiForgeryValidationEnabled = true;
            SetNoCacheForAllResponses = true;
        }
    }
}