using System;
using System.ComponentModel.DataAnnotations;

namespace Documaster.Business.Extensions
{
    public static class EnumTools
    {
        public static string GetDescription(this Enum enumValue)
        {
            var type = enumValue.GetType();
            var memInfo = type.GetMember(enumValue.ToString());
            if (memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                if (attrs.Length > 0)
                {
                    return ((DisplayAttribute) attrs[0]).Name;
                }
            }
            return enumValue.ToString();
        }
    }
}
