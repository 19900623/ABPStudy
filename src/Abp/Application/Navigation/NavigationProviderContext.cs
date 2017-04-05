namespace Abp.Application.Navigation
{
    /// <summary>
    /// �����ṩ��������
    /// </summary>
    internal class NavigationProviderContext : INavigationProviderContext
    {
        /// <summary>
        /// ��������ӿ�
        /// </summary>
        public INavigationManager Manager { get; private set; }

        /// <summary>
        /// ��ʼ�� <see cref="NavigationProviderContext"/>
        /// </summary>
        /// <param name="manager">��������ӿ�</param>
        public NavigationProviderContext(INavigationManager manager)
        {
            Manager = manager;
        }
    }
}