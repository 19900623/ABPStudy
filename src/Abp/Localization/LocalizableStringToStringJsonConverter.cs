using System;
using Newtonsoft.Json;

namespace Abp.Localization
{
    /// <summary>
    /// This class can be used to serialize <see cref="ILocalizableString"/> to <see cref="string"/> during serialization.
    /// �������������л��ڼ����л�<see cref="ILocalizableString"/> ���ַ���
    /// It does not work for deserialization.
    /// ���������ڷ����л�
    /// </summary>
    public class LocalizableStringToStringJsonConverter : JsonConverter
    {
        /// <summary>
        /// дJson
        /// </summary>
        /// <param name="writer">Json��д��</param>
        /// <param name="value">ֵ</param>
        /// <param name="serializer">Json���л���</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var localizableString = (ILocalizableString) value;
            writer.WriteValue(localizableString.Localize(new LocalizationContext(LocalizationHelper.Manager)));
        }

        /// <summary>
        /// ��Json
        /// </summary>
        /// <param name="reader">Json��ȡ��</param>
        /// <param name="objectType">��������</param>
        /// <param name="existingValue">���ڵ�ֵ</param>
        /// <param name="serializer">���л���</param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// �ܷ�ת��
        /// </summary>
        /// <param name="objectType">����</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof (ILocalizableString).IsAssignableFrom(objectType);
        }
    }
}