using System;

namespace Abp.Configuration
{
    /// <summary>
    /// Defines interface to use a dictionary to make configurations.
    /// ����һ���ӿڣ�ʹ���ֵ���ʵ������
    /// </summary>
    public interface IDictionaryBasedConfig
    {
        /// <summary>
        /// Used to set a string named configuration.If there is already a configuration with same <paramref name="name"/>, it's overwritten.
        /// ����һ���������ƣ��������<paramref name="name"/>�Ѿ����ڣ�����д��
        /// </summary>
        /// <param name="name">Unique name of the configuration / ��������</param>
        /// <param name="value">Value of the configuration / ����ֵ</param>
        /// <returns>Returns the passed <paramref name="value"/>  / ���ش���ֵ</returns>
        void Set<T>(string name, T value);

        /// <summary>
        /// Gets a configuration object with given name.
        /// ��ȡ�������Ƶ����ö���
        /// </summary>
        /// <param name="name">Unique name of the configuration / ��������</param>
        /// <returns>Value of the configuration or null if not found / ����ֵ����������ڣ�����null</returns>
        object Get(string name);

        /// <summary>
        /// Gets a configuration object with given name.
        /// ��ȡ�������Ƶ����ö���
        /// </summary>
        /// <typeparam name="T">Type of the object / ���ö�������</typeparam>
        /// <param name="name">Unique name of the configuration / ��������</param>
        /// <returns>Value of the configuration or null if not found / ����ֵ����������ڣ�����null</returns>
        T Get<T>(string name);

        /// <summary>
        /// Gets a configuration object with given name.
        /// ��ȡ�������Ƶ����ö���
        /// </summary>
        /// <param name="name">Unique name of the configuration / ��������</param>
        /// <param name="defaultValue">Default value of the object if can not found given configuration / ��������ڣ����ص�Ĭ��ֵ</param>
        /// <returns>Value of the configuration or null if not found / ����ֵ����������ڣ����ظ�����Ĭ��ֵ</returns>
        object Get(string name, object defaultValue);

        /// <summary>
        /// Gets a configuration object with given name.
        /// ��ȡ�������Ƶ����ö���
        /// </summary>
        /// <typeparam name="T">Type of the object / ���ö�������</typeparam>
        /// <param name="name">Unique name of the configuration / ��������</param>
        /// <param name="defaultValue">Default value of the object if can not found given configuration / ��������ڣ����ص�Ĭ��ֵ</param>
        /// <returns>Value of the configuration or null if not found / ����ֵ����������ڣ����ظ�����Ĭ��ֵ</returns>
        T Get<T>(string name, T defaultValue);

        /// <summary>
        /// Gets a configuration object with given name.
        /// ��ȡ�������Ƶ����ö���
        /// </summary>
        /// <typeparam name="T">Type of the object / ���ö�������</typeparam>
        /// <param name="name">Unique name of the configuration / ��������</param>
        /// <param name="creator">The function that will be called to create if given configuration is not found / ��������ڣ����ø�ί��</param>
        /// <returns>Value of the configuration or null if not found / ����ֵ����������ڣ�����ί�д�����ֵ</returns>
        T GetOrCreate<T>(string name, Func<T> creator);
    }
}