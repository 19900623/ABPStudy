using Abp;

namespace Derrick
{
    /// <summary>
    /// This class can be used as a base class for services in this application.
    /// ���౻������ǰӦ�ó���ķ�����ࡣ
    /// It has some useful objects property-injected and has some basic methods most of services may need to.
    /// ����һЩ���õĶ�������ע�룬�����д󲿷ַ��������Ҫ�Ļ���������
    /// It's suitable for non domain nor application service classes.
    /// ��ֻ���ڷ������Ӧ�ó��������
    /// For domain services inherit <see cref="AbpZeroTemplateDomainServiceBase"/>.
    /// �������̳���<see cref="AbpZeroTemplateDomainServiceBase"/>.
    /// For application services inherit AbpZeroTemplateAppServiceBase.
    /// Ӧ�ó������̳���<see cref="AbpZeroTemplateAppServiceBase"/>
    /// </summary>
    public abstract class AbpZeroTemplateServiceBase : AbpServiceBase
    {
        protected AbpZeroTemplateServiceBase()
        {
            LocalizationSourceName = AbpZeroTemplateConsts.LocalizationSourceName;
        }
    }
}