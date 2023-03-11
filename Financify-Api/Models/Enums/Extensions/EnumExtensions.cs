using System.ComponentModel;

namespace Financify_Api.Models.Enums.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this ChargeStatus status)
        {
            var type = typeof(ChargeStatus);
            var member = type.GetMember(status.ToString())[0];
            var attribute = (DescriptionAttribute)member.GetCustomAttributes(typeof(DescriptionAttribute), false)[0];
            return attribute.Description;
        }
    }
}
