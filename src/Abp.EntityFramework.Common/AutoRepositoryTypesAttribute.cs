using System;

namespace Abp.EntityFramework
{
    /// <summary>
    /// Used to define auto-repository types for entities.This can be used for DbContext types.
    /// ���ڶ���ʵ����Զ��ִ����ͣ�����Ա����ݿ�����������ʹ��
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRepositoryTypesAttribute : Attribute
    {
        /// <summary>
        /// �ִ��ӿ�
        /// </summary>
        public Type RepositoryInterface { get; private set; }

        /// <summary>
        /// ���������Ĳִ��ӿ�
        /// </summary>
        public Type RepositoryInterfaceWithPrimaryKey { get; private set; }

        /// <summary>
        /// �ִ�ʵ��
        /// </summary>
        public Type RepositoryImplementation { get; private set; }

        /// <summary>
        /// ���������Ĳִ�ʵ��
        /// </summary>
        public Type RepositoryImplementationWithPrimaryKey { get; private set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="repositoryInterface">�ִ��ӿ�</param>
        /// <param name="repositoryInterfaceWithPrimaryKey">���������Ĳִ��ӿ�</param>
        /// <param name="repositoryImplementation">�ִ�ʵ��</param>
        /// <param name="repositoryImplementationWithPrimaryKey">���������Ĳִ�ʵ��</param>
        public AutoRepositoryTypesAttribute(
            Type repositoryInterface,
            Type repositoryInterfaceWithPrimaryKey,
            Type repositoryImplementation,
            Type repositoryImplementationWithPrimaryKey)
        {
            RepositoryInterface = repositoryInterface;
            RepositoryInterfaceWithPrimaryKey = repositoryInterfaceWithPrimaryKey;
            RepositoryImplementation = repositoryImplementation;
            RepositoryImplementationWithPrimaryKey = repositoryImplementationWithPrimaryKey;
        }
    }
}