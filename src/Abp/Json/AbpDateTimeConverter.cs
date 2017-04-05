using System;
using Abp.Timing;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Abp.Json
{
    /// <summary>
    /// ABPʱ��ת����
    /// </summary>
    public class AbpDateTimeConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// ȷ����������ת����ʱ�����
        /// </summary>
        /// <param name="objectType">Ҫת���Ķ���</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(DateTime) || objectType == typeof(DateTime?))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// ��ȡJson
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var date = base.ReadJson(reader, objectType, existingValue, serializer) as DateTime?;

            if (date.HasValue)
            {
                return Clock.Normalize(date.Value);
            }

            return null;
        }

        /// <summary>
        /// дJson
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = value as DateTime?;
            base.WriteJson(writer, date.HasValue ? Clock.Normalize(date.Value) : value, serializer);
        }
    }
}