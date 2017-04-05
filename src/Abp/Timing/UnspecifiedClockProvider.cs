using System;

namespace Abp.Timing
{
    /// <summary>
    /// δָ��ʱ������
    /// </summary>
    public class UnspecifiedClockProvider : IClockProvider
    {
        /// <summary>
        /// ��ȡ��ǰʱ��
        /// </summary>
        public DateTime Now => DateTime.Now;

        /// <summary>
        /// ��ʾ��ʱ���δָ��Ϊ����ʱ�䣬Ҳδָ��ΪЭ��ͨ��ʱ�� (UTC)
        /// </summary>
        public DateTimeKind Kind => DateTimeKind.Unspecified;

        /// <summary>
        /// ��֧�ֶ�ʱ��
        /// </summary>
        public bool SupportsMultipleTimezone => false;

        /// <summary>
        /// �淶�������� <see cref="DateTime"/>
        /// </summary>
        /// <param name="dateTime">Ҫ�淶����ʱ��</param>
        /// <returns>�淶����ʱ��</returns>
        public DateTime Normalize(DateTime dateTime)
        {
            return dateTime;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        internal UnspecifiedClockProvider()
        {
            
        }
    }
}