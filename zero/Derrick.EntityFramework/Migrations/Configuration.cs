using System.Data.Entity.Migrations;
using Abp.Events.Bus;
using Abp.Events.Bus.Entities;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using EntityFramework.DynamicFilters;
using Derrick.Migrations.Seed.Host;
using Derrick.Migrations.Seed.Tenants;

namespace Derrick.Migrations
{
    /// <summary>
    /// ���ݿ�Ǩ������
    /// </summary>
    public sealed class Configuration : DbMigrationsConfiguration<EntityFramework.AbpZeroTemplateDbContext>, IMultiTenantSeed
    {
        /// <summary>
        /// ABP�̻�����
        /// </summary>
        public AbpTenantBase Tenant { get; set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "AbpZeroTemplate";
        }
        /// <summary>
        /// ��ʼ����������
        /// </summary>
        /// <param name="context">DB������</param>
        protected override void Seed(EntityFramework.AbpZeroTemplateDbContext context)
        {
            context.DisableAllFilters();

            context.EntityChangeEventHelper = NullEntityChangeEventHelper.Instance;
            context.EventBus = NullEventBus.Instance;

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantBuilder(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases using Tenant property...
            }

            context.SaveChanges();
        }
    }
}
