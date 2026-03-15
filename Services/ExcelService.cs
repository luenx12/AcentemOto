using AcentemOto.Extensions;
using AcentemOto.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AcentemOto.Services
{
    public class ExcelService
    {
        public ExcelService()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async Task<List<MessageLog>> ReadPhoneNumbersAsync(string filePath, string? password = null)
        {
            return await Task.Run(() =>
            {
                var messageLogs = new List<MessageLog>();

                if (!File.Exists(filePath))
                    throw new FileNotFoundException("Excel dosyası bulunamadı.");

                using (var package = string.IsNullOrEmpty(password)
                    ? new ExcelPackage(new FileInfo(filePath))
                    : new ExcelPackage(new FileInfo(filePath), password))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    if (worksheet.Dimension == null) return messageLogs;

                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    // Determine headers and phone column
                    int phoneColIndex = 1;
                    var headers = new Dictionary<int, string>();
                    bool hasHeaders = false;

                    // Check if first row looks like headers (string values)
                    for (int c = 1; c <= colCount; c++)
                    {
                        var cellValue = worksheet.Cells[1, c].Text?.Trim();
                        if (!string.IsNullOrEmpty(cellValue))
                        {
                            headers[c] = cellValue;
                            hasHeaders = true;
                            if (cellValue.Equals("Telefon", StringComparison.OrdinalIgnoreCase) ||
                                cellValue.Equals("Numara", StringComparison.OrdinalIgnoreCase) ||
                                cellValue.Equals("Phone", StringComparison.OrdinalIgnoreCase) ||
                                cellValue.Contains("Referans / Telefon", StringComparison.OrdinalIgnoreCase) ||
                                cellValue.Contains("Referans", StringComparison.OrdinalIgnoreCase))
                            {
                                phoneColIndex = c;
                            }
                        }
                    }

                    int startRow = hasHeaders ? 2 : 1;
                    int consecutiveEmptyRows = 0;

                    for (int row = startRow; row <= rowCount; row++)
                    {
                        var phoneValue = worksheet.Cells[row, phoneColIndex].Text?.Trim();
                        if (string.IsNullOrWhiteSpace(phoneValue)) 
                        {
                            consecutiveEmptyRows++;
                            if (consecutiveEmptyRows > 10) break; // Çok fazla boş satır gelirse dosyayı sonlandır (performans için)
                            continue;
                        }
                        
                        consecutiveEmptyRows = 0;

                        string formattedNumber = phoneValue.FormatTurkishPhone();
                        if (!string.IsNullOrEmpty(formattedNumber))
                        {
                            var log = new MessageLog
                            {
                                PhoneNumber = formattedNumber,
                                Status = MessageStatus.Pending
                            };

                            // Add other columns as parameters
                            var offerList = new List<string>();
                            decimal minPrice = decimal.MaxValue;
                            string minCompany = "";
                            string rawMinPriceStr = "";

                            // Kesinlikle teklif kolonu OLMAYAN başlıklar (exact match ve contains için ayrı listeler)
                            var excludedExact = new[] { "S.N", "S.No", "Sıra", "Ad", "Soyad", "TC", "VKN", "Yıl" };
                            var excludedContains = new[] { "Tarih", "tarih", "İsim", "isim", "Tür", "Plaka", "Belge", "Marka", "Referans", "Telefon", "Tel", "Durum", "Teklif No", "Teklif", "Şirket", "Sirket", "TC/VRG", "VRG" };

                            for (int c = 1; c <= colCount; c++)
                            {
                                if (c == phoneColIndex) continue;

                                string headerKey = headers.ContainsKey(c) ? headers[c] : $"Sütun_{c}";
                                string cellValue = worksheet.Cells[row, c].Text?.Trim() ?? "";
                                log.Parameters[headerKey] = cellValue;

                                // Akıllı Teklif Çıkarımı
                                if (!string.IsNullOrWhiteSpace(cellValue))
                                {
                                    // Önce exact match kontrolü
                                    bool isExcluded = excludedExact.Any(eh => headerKey.Equals(eh, StringComparison.OrdinalIgnoreCase));
                                    // Sonra contains kontrolü
                                    if (!isExcluded)
                                    {
                                        isExcluded = excludedContains.Any(eh => headerKey.IndexOf(eh, StringComparison.OrdinalIgnoreCase) >= 0);
                                    }

                                    if (!isExcluded)
                                    {
                                        // ₺, TL, boşluk ve diğer karakter temizliği
                                        string cleanValue = cellValue
                                            .Replace("₺", "")
                                            .Replace("TL", "")
                                            .Replace("tl", "")
                                            .Replace(" ", "")
                                            .Trim();
                                        // Sadece rakam, virgül ve nokta bırak
                                        cleanValue = new string(cleanValue.Where(ch => char.IsDigit(ch) || ch == ',' || ch == '.').ToArray());

                                        // Türkçe formatı dene (10.573 veya 1.550,00 gibi)
                                        if (decimal.TryParse(cleanValue, System.Globalization.NumberStyles.Any, new System.Globalization.CultureInfo("tr-TR"), out decimal price))
                                        {
                                            if (price > 100)
                                            {
                                                offerList.Add($"{headerKey}: {cellValue}");
                                                if (price < minPrice)
                                                {
                                                    minPrice = price;
                                                    minCompany = headerKey;
                                                    rawMinPriceStr = cellValue;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // İngilizce formatı da dene (1550.50 gibi)
                                            if (decimal.TryParse(cleanValue, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal priceEn))
                                            {
                                                if (priceEn > 100)
                                                {
                                                    offerList.Add($"{headerKey}: {cellValue}");
                                                    if (priceEn < minPrice)
                                                    {
                                                        minPrice = priceEn;
                                                        minCompany = headerKey;
                                                        rawMinPriceStr = cellValue;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            // Özel Parametreleri Ekle
                            if (offerList.Any())
                            {
                                log.Parameters["Teklifler"] = string.Join("\n", offerList);
                                log.Parameters["EnUygunFiyat"] = rawMinPriceStr;
                                log.Parameters["EnUygunSirket"] = minCompany;
                                log.Parameters["EnUygunTeklif"] = $"{minCompany}: {rawMinPriceStr}";
                            }

                            messageLogs.Add(log);
                        }
                    }
                }

                return messageLogs;
            });
        }

        public async Task ExportToExcelAsync(List<MessageLog> logs, string savePath)
        {
            await Task.Run(() =>
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Rapor");

                    // Gather all distinct parameter keys
                    var parameterKeys = logs.SelectMany(l => l.Parameters.Keys).Distinct().ToList();

                    // Standard Headers
                    worksheet.Cells[1, 1].Value = "Telefon";
                    worksheet.Cells[1, 2].Value = "Durum";
                    worksheet.Cells[1, 3].Value = "Hata Mesajı";
                    worksheet.Cells[1, 4].Value = "Gönderim Zamanı";

                    // Dynamic Headers
                    int col = 5;
                    foreach (var key in parameterKeys)
                    {
                        worksheet.Cells[1, col].Value = key;
                        col++;
                    }

                    // Format headers
                    using (var range = worksheet.Cells[1, 1, 1, col - 1])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    }

                    // Rows
                    int row = 2;
                    foreach (var log in logs)
                    {
                        worksheet.Cells[row, 1].Value = log.PhoneNumber;
                        worksheet.Cells[row, 2].Value = log.Status.ToString();
                        worksheet.Cells[row, 3].Value = log.ErrorMessage;
                        worksheet.Cells[row, 4].Value = log.SentAt?.ToString("yyyy-MM-dd HH:mm:ss");

                        col = 5;
                        foreach (var key in parameterKeys)
                        {
                            worksheet.Cells[row, col].Value = log.Parameters.ContainsKey(key) ? log.Parameters[key] : "";
                            col++;
                        }
                        row++;
                    }

                    worksheet.Cells.AutoFitColumns();
                    package.SaveAs(new FileInfo(savePath));
                }
            });
        }

        /// <summary>
        /// Yüklenen verileri kategoriye göre filtreler. Mevcut listeyi değiştirmez, yeni liste döner.
        /// </summary>
        public List<MessageLog> FilterByCategory(
            List<MessageLog> allLogs,
            FilterCategory category,
            string? filterValue = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? typeFilter = null)
        {
            var logs = new List<MessageLog>();

            switch (category)
            {
                case FilterCategory.SigortaTarihi:
                    if (startDate == null || endDate == null)
                        return new List<MessageLog>();

                    logs = allLogs.Where(log =>
                    {
                        string dateStr = "";
                        foreach (var key in log.Parameters.Keys)
                        {
                            if (key.IndexOf("Sigorta Tar", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                key.IndexOf("Sigorta_Tar", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                dateStr = log.Parameters[key];
                                break;
                            }
                        }

                        if (string.IsNullOrWhiteSpace(dateStr)) return false;

                        if (DateTime.TryParse(dateStr, new System.Globalization.CultureInfo("tr-TR"), System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                        {
                            return parsedDate.Date >= startDate.Value.Date && parsedDate.Date <= endDate.Value.Date;
                        }

                        return false;
                    }).ToList();
                    break;

                case FilterCategory.Durum:
                    if (string.IsNullOrWhiteSpace(filterValue))
                        return new List<MessageLog>();

                    logs = allLogs.Where(log =>
                    {
                        foreach (var key in log.Parameters.Keys)
                        {
                            if (key.IndexOf("Durum", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                return log.Parameters[key].IndexOf(filterValue, StringComparison.OrdinalIgnoreCase) >= 0;
                            }
                        }
                        return false;
                    }).ToList();
                    break;

                case FilterCategory.Sirket:
                    if (string.IsNullOrWhiteSpace(filterValue))
                        return new List<MessageLog>();

                    logs = allLogs.Where(log =>
                    {
                        foreach (var key in log.Parameters.Keys)
                        {
                            if (key.IndexOf("Şirket", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                key.IndexOf("Sirket", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                return log.Parameters[key].IndexOf(filterValue, StringComparison.OrdinalIgnoreCase) >= 0;
                            }
                        }
                        return false;
                    }).ToList();
                    break;

                case FilterCategory.Turu:
                    if (string.IsNullOrWhiteSpace(filterValue))
                        return new List<MessageLog>();

                    logs = allLogs.Where(log =>
                    {
                        foreach (var key in log.Parameters.Keys)
                        {
                            if (key.IndexOf("Tür", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                key.IndexOf("Tur", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                return log.Parameters[key].IndexOf(filterValue, StringComparison.OrdinalIgnoreCase) >= 0;
                            }
                        }
                        return false;
                    }).ToList();
                    break;

                default:
                    return new List<MessageLog>();
            }

            // Apply type sub-filter if provided (useful for SigortaTarihi + Tür combination)
            if (!string.IsNullOrWhiteSpace(typeFilter))
            {
                logs = logs.Where(log =>
                {
                    foreach (var key in log.Parameters.Keys)
                    {
                        if (key.IndexOf("Tür", StringComparison.OrdinalIgnoreCase) >= 0 ||
                            key.IndexOf("Tur", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            return log.Parameters[key].IndexOf(typeFilter, StringComparison.OrdinalIgnoreCase) >= 0;
                        }
                    }
                    return false;
                }).ToList();
            }

            return logs;
        }

        /// <summary>
        /// Yüklenen verilerden belirli bir parametrenin benzersiz değerlerini çıkarır (ComboBox doldurmak için).
        /// </summary>
        public List<string> GetDistinctValues(List<MessageLog> allLogs, string parameterKeyContains)
        {
            var values = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var log in allLogs)
            {
                foreach (var key in log.Parameters.Keys)
                {
                    if (key.IndexOf(parameterKeyContains, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        string val = log.Parameters[key]?.Trim() ?? "";
                        if (!string.IsNullOrWhiteSpace(val))
                        {
                            values.Add(val);
                        }
                    }
                }
            }

            return values.OrderBy(v => v).ToList();
        }
    }
}
