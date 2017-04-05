using System;

namespace Abp.Timing
{
    /// <summary>
    /// Implements <see cref="IClockProvider"/> to work with local times.
    /// <see cref="IClockProvider"/>�ڱ���ʱ���ʵ��
    /// </summary>
    public class LocalClockProvider : IClockProvider
    {
        /// <summary>
        /// ��ȡ��ǰʱ��
        /// </summary>
        public DateTime Now => DateTime.Now;

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTimeKind Kind => DateTimeKind.Local;

        /// <summary>
        /// ��֧��ʱ������
        /// </summary>
        public bool SupportsMultipleTimezone => false;

        /// <summary>
        /// �淶�������� <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">Ҫ�淶����ʱ��</param>
        /// <returns>�淶����ʱ��</returns>
        public DateTime Normalize(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
            }

            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return dateTime.ToLocalTime();
            }

            return dateTime;
        }

        internal LocalClockProvider()
        {

        }
    }
}