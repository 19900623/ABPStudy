using System;

namespace Abp.Timing
{
    /// <summary>
    /// Used to perform some common date-time operations.
    /// ����ִ��һЩ�����date-time����
    /// </summary>
    public static class Clock
    {
        /// <summary>
        /// This object is used to perform all <see cref="Clock"/> operations.Default value: <see cref="UnspecifiedClockProvider"/>.
        /// �����������ִ�����е�<see cref="Clock"/>����.Default value: <see cref="UnspecifiedClockProvider"/>.
        /// </summary>
        public static IClockProvider Provider
        {
            get { return _provider; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Can not set Clock.Provider to null!");
                }

                _provider = value;
            }
        }

        private static IClockProvider _provider;

        /// <summary>
        /// ���캯��
        /// </summary>
        static Clock()
        {
            Provider = ClockProviders.Unspecified;
        }

        /// <summary>
        /// Gets Now using current <see cref="Provider"/>.
        /// ʹ�� <see cref="Provider"/>��ȡ��ǰʱ��.
        /// </summary>
        public static DateTime Now => Provider.Now;

        public static DateTimeKind Kind => Provider.Kind;

        /// <summary>
        /// Returns true if multiple timezone is supported, returns false if not.
        /// �Ƿ�֧�ֶ�ʱ��
        /// </summary>
        public static bool SupportsMultipleTimezone => Provider.SupportsMultipleTimezone;

        /// <summary>
        /// Normalizes given <see cref="DateTime"/> using current <see cref="Provider"/>.
        /// �淶�������� <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dateTime">DateTime to be normalized. / Ҫ�淶����ʱ��</param>
        /// <returns>Normalized DateTime / �淶����ʱ��</returns>
        public static DateTime Normalize(DateTime dateTime)
        {
            return Provider.Normalize(dateTime);
        }
    }
}