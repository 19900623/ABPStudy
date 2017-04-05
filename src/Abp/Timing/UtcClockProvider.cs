using System;

namespace Abp.Timing
{
    /// <summary>
    /// Implements <see cref="IClockProvider"/> to work with UTC times.
    /// ʵ�ֽӿ� <see cref="IClockProvider"/>�����ṩUTCʱ��
    /// </summary>
    public class UtcClockProvider : IClockProvider
    {
        /// <summary>
        /// ��ȡ��ǰʱ��
        /// </summary>
        public DateTime Now => DateTime.UtcNow;

        /// <summary>
        /// UTCʱ��
        /// </summary>
        public DateTimeKind Kind => DateTimeKind.Utc;

        /// <summary>
        /// ֧�ֶ�ʱ��
        /// </summary>
        public bool SupportsMultipleTimezone => true;

        /// <summary>
        /// �淶�������� <see cref="DateTime"/>
        /// </summary>
        /// <param name="dateTime">Ҫ�淶����ʱ��</param>
        /// <returns>�淶����ʱ��</returns>
        public DateTime Normalize(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            }

            if (dateTime.Kind == DateTimeKind.Local)
            {
                return dateTime.ToUniversalTime();
            }

            return dateTime;
        }

        internal UtcClockProvider()
        {

        }
    }
}