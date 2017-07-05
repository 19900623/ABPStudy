using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace Abp.EntityFramework.Extensions
{
    //TODO: MOVE TO ABP
    //TODO: We can create simpler extension methods to create indexes
    //TODO: Check https://github.com/mj1856/EntityFramework.IndexingExtensions for example
    /// <summary>
    /// EF Model��������չ
    /// </summary>
    internal static class EntityFrameworkModelBuilderExtensions
    {
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="propertyConfiguration">ԭ��������</param>
        /// <returns></returns>
        public static PrimitivePropertyConfiguration CreateIndex(this PrimitivePropertyConfiguration propertyConfiguration)
        {
            return propertyConfiguration.HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute()
                    )
                );
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="propertyConfiguration">ԭ��������</param>
        /// <param name="name">����</param>
        /// <returns></returns>
        public static PrimitivePropertyConfiguration CreateIndex(this PrimitivePropertyConfiguration propertyConfiguration, string name)
        {
            return propertyConfiguration.HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute(name)
                    )
                );
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="propertyConfiguration">ԭ��������</param>
        /// <param name="name">����</param>
        /// <param name="order">�����</param>
        /// <returns></returns>
        public static PrimitivePropertyConfiguration CreateIndex(this PrimitivePropertyConfiguration propertyConfiguration, string name, int order)
        {
            return propertyConfiguration.HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute(name, order)
                    )
                );
        }
    }
}