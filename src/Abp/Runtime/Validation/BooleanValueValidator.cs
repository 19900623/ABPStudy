using System;

namespace Abp.Runtime.Validation
{
    /// <summary>
    /// Boolֵ��֤��
    /// </summary>
    [Serializable]
    [Validator("BOOLEAN")]
    public class BooleanValueValidator : ValueValidatorBase
    {
        /// <summary>
        /// �Ƿ���Ч
        /// </summary>
        /// <param name="value">ֵ</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            if (value is bool)
            {
                return true;
            }

            bool b;
            return bool.TryParse(value.ToString(), out b);
        }
    }
}