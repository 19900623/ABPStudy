using System;
using System.Collections.Generic;
using Abp.Collections.Extensions;

namespace Abp.Configuration
{
    /// <summary>
    /// Used to set/get custom configuration.
    /// ��ȡ�������Զ�������
    /// </summary>
    public class DictionaryBasedConfig : IDictionaryBasedConfig
    {
        /// <summary>
        /// Dictionary of custom configuration.
        /// �Զ��������ֵ�
        /// </summary>
        protected Dictionary<string, object> CustomSettings { get; private set; }

        /// <summary>
        /// Gets/sets a config value.Returns null if no config with given name.
        /// ��ȡ������һ������ֵ������������Ƶ����ò����ڣ�����null
        /// </summary>
        /// <param name="name">Name of the config / ��������</param>
        /// <returns>Value of the config / ����ֵ</returns>
        public object this[string name]
        {
            get { return CustomSettings.GetOrDefault(name); }
            set { CustomSettings[name] = value; }
        }

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        protected DictionaryBasedConfig()
        {
            CustomSettings = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets a configuration value as a specific type.
        /// ��ȡһ��ָ�����͵�����ֵ
        /// </summary>
        /// <param name="name">Name of the config / ��������</param>
        /// <typeparam name="T">Type of the config / ��������</typeparam>
        /// <returns>Value of the configuration or null if not found / ����ֵ����������ڷ���null</returns>
        public T Get<T>(string name)
        {
            var value = this[name];
            return value == null
                ? default(T)
                : (T) Convert.ChangeType(value, typeof (T));
        }

        /// <summary>
        /// Used to set a string named configuration.If there is already a configuration with same <paramref name="name"/>, it's overwritten.
        /// ����һ���ַ����������Ƶ����ã������������<paramref name="name"/>�Ѿ����ڣ�������д
        /// </summary>
        /// <param name="name">Unique name of the configuration / Ψһ����������</param>
        /// <param name="value">Value of the configuration / ����ֵ</param>
        public void Set<T>(string name, T value)
        {
            this[name] = value;
        }

        /// <summary>
        /// Gets a configuration object with given name.
        /// ��ȡ�������Ƶ����ö���
        /// </summary>
        /// <param name="name">Unique name of the configuration / Ψһ����������</param>
        /// <returns>Value of the configuration or null if not found / ����ֵ,����������Ʋ����ڷ���null</returns>
        public object Get(string name)
        {
            return Get(name, null);
        }

        /// <summary>
        /// Gets a configuration object with given name.
        /// ��ȡ�������Ƶ����ö���
        /// </summary>
        /// <param name="name">Unique name of the configuration / Ψһ����������</param>
        /// <param name="defaultValue">Default value of the object if can not found given configuration / ����������������Ʋ����ڣ����ص�Ĭ��ֵ</param>
        /// <returns>Value of the configuration or null if not found / ����ֵ,����������Ʋ����ڷ���Ĭ��ֵ</returns>
        public object Get(string name, object defaultValue)
        {
            var value = this[name];
            if (value == null)
            {
                return defaultValue;
            }

            return this[name];
        }

        /// <summary>
        /// Gets a configuration object with given name.
        /// ��ȡ�������Ƶ����ö���
        /// </summary>
        /// <typeparam name="T">Type of the object / ��������</typeparam>
        /// <param name="name">Unique name of the configuration / Ψһ����������</param>
        /// <param name="defaultValue">Default value of the object if can not found given configuration / ����������������Ʋ����ڣ����ص�Ĭ��ֵ</param>
        /// <returns>Value of the configuration or null if not found / ����ֵ,����������Ʋ����ڷ���Ĭ��ֵ</returns>
        public T Get<T>(string name, T defaultValue)
        {
            return (T)Get(name, (object)defaultValue);
        }

        /// <summary>
        /// Gets a configuration object with given name.
        /// ��ȡ�������Ƶ����ö���
        /// </summary>
        /// <typeparam name="T">Type of the object / ��������</typeparam>
        /// <param name="name">Unique name of the configuration / Ψһ����������</param>
        /// <param name="creator">The function that will be called to create if given configuration is not found / ί�У�����������������Ʋ����ڣ���������</param>
        /// <returns>Value of the configuration or null if not found / ����ֵ,����������Ʋ����ڷ���ί�д�����ֵ</returns>
        public T GetOrCreate<T>(string name, Func<T> creator)
        {
            var value = Get(name);
            if (value == null)
            {
                value = creator();
                Set(name, value);
            }
            return (T) value;
        }
    }
}