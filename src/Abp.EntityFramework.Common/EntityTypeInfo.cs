using System;

namespace Abp.EntityFramework
{
    /// <summary>
    /// ʵ��������Ϣ
    /// </summary>
    public class EntityTypeInfo
    {
        /// <summary>
        /// Type of the entity.
        /// ʵ�������
        /// </summary>
        public Type EntityType { get; private set; }

        /// <summary>
        /// DbContext type that has DbSet property.
        /// ӵ��DbSet���Ե����ݿ�����������
        /// </summary>
        public Type DeclaringType { get; private set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="entityType">ʵ�������</param>
        /// <param name="declaringType">ӵ��DbSet���Ե����ݿ�����������</param>
        public EntityTypeInfo(Type entityType, Type declaringType)
        {
            EntityType = entityType;
            DeclaringType = declaringType;
        }
    }
}