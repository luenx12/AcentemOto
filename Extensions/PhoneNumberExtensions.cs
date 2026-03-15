using System.Text.RegularExpressions;

namespace AcentemOto.Extensions
{
    public static class PhoneNumberExtensions
    {
        public static string FormatTurkishPhone(this string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return string.Empty;

            // Sadece rakamları al
            string cleanNumber = Regex.Replace(phoneNumber, @"[^\d]", "");

            // Türkiye için (90) başı kontrolü
            if (cleanNumber.StartsWith("90"))
            {
                // Uzunluk doğrulama (örn: 905551234567 -> 12 hane)
                if (cleanNumber.Length == 12) return cleanNumber;
                return cleanNumber; // Olduğu gibi bırak
            }

            // Başında 0 varsa at (örn: 05551234567 -> 5551234567)
            if (cleanNumber.StartsWith("0") && cleanNumber.Length == 11)
            {
                return "90" + cleanNumber.Substring(1);
            }

            // Sadece alan kodu ve numara varsa (örn: 5551234567 -> 10 hane)
            if (cleanNumber.Length == 10)
            {
                return "90" + cleanNumber;
            }

            // Bilinmeyen format, olduğu gibi döndür
            return cleanNumber;
        }
    }
}
