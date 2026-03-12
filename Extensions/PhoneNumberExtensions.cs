using System.Text.RegularExpressions;

namespace AcentemOto.Extensions
{
    public static class PhoneNumberExtensions
    {
        public static string FormatTurkishPhone(this string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return string.Empty;

            // Rakam olmayan tüm karakterleri temizle
            string cleanNumber = Regex.Replace(phoneNumber, @"[^\d]", "");

            if (cleanNumber.StartsWith("90") && cleanNumber.Length >= 12)
            {
                return "+" + cleanNumber;
            }

            if (cleanNumber.StartsWith("0"))
            {
                cleanNumber = cleanNumber.Substring(1);
            }

            if (cleanNumber.Length == 10)
            {
                return "+90" + cleanNumber;
            }

            return "+" + cleanNumber;
        }
    }
}
