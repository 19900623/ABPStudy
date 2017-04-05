using System;
using Abp.Collections.Extensions;

namespace Abp.Application.Navigation
{
    /// <summary>
    /// <see cref="IHasMenuItemDefinitions"/>��չ����
    /// </summary>
    public static class HasMenuItemDefinitionsExtensions
    {
        /// <summary>
        /// ͨ�����ƻ�ȡ<see cref="MenuItemDefinition"/> ����.
        /// ������������׳��쳣.
        /// </summary>
        /// <param name="source">Դ����</param>
        /// <param name="name">�˵�������</param>
        public static MenuItemDefinition GetItemByName(this IHasMenuItemDefinitions source, string name)
        {
            var item = GetItemByNameOrNull(source, name);
            if (item == null)
            {
                throw new ArgumentException("There is no source item with given name: " + name, "name");
            }

            return item;
        }

        /// <summary>
        /// ͨ���ݹ�ķ�ʽ�������ƻ�ȡ <see cref="MenuItemDefinition"/>���� .
        /// ���û�ҵ��ͷ���Null.
        /// </summary>
        /// <param name="source">Դ����</param>
        /// <param name="name">Դ����˵�������</param>
        public static MenuItemDefinition GetItemByNameOrNull(this IHasMenuItemDefinitions source, string name)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (source.Items.IsNullOrEmpty())
            {
                return null;
            }

            foreach (var subItem in source.Items)
            {
                if (subItem.Name == name)
                {
                    return subItem;
                }

                var subItemSearchResult = GetItemByNameOrNull(subItem, name);
                if (subItemSearchResult != null)
                {
                    return subItemSearchResult;
                }
            }

            return null;
        }
    }
}