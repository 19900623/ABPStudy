namespace Abp.Events.Bus
{
    /// <summary>
    /// This interface must be implemented by event data classes that has a single generic argument and this argument will be used by inheritance. 
    /// �¼����������ʵ�ִ˽ӿڣ� �¼���������һ�������ķ��Ͳ����������������̳�ʹ��
    /// For example;
    /// Assume that Student inherits From Person. When trigger an EntityCreatedEventData{Student},
    /// EntityCreatedEventData{Person} is also triggered if EntityCreatedEventData implements this interface.
    /// ����;����Student�̳���Person.������һ��EntityCreatedEventData{Student}�¼���
    /// ���EntityCreatedEventDataʵ���˴˽ӿڣ�EntityCreatedEventData{Person}�¼�Ҳ�ᱻ����
    /// </summary>
    public interface IEventDataWithInheritableGenericArgument
    {
        /// <summary>
        /// Gets arguments to create this class since a new instance of this class is created.
        /// ��ȡ���������ʵ���Ĳ�������Ϊ������һ���µ�ʵ��������
        /// </summary>
        /// <returns>Constructor arguments / ���캯������</returns>
        object[] GetConstructorArgs();
    }
}