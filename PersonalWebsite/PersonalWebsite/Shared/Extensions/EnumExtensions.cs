using System;
using System.Linq;

namespace PersonalWebsite.Shared.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var genericEnumType = value.GetType();
            var memberInfo = genericEnumType.GetMember(value.ToString());

            if (memberInfo != null && memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attributes != null && attributes.Length > 0)
                    return ((System.ComponentModel.DescriptionAttribute)attributes.ElementAt(0)).Description;
            }

            return value.ToString();
        }
    }
}
