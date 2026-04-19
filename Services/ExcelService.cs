using AcentemOto.Extensions;
using AcentemOto.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExcelDataReader;

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

                List<List<string>> grid = new List<List<string>>();

                try
                {
                    using (var package = string.IsNullOrEmpty(password)
                        ? new ExcelPackage(new FileInfo(filePath))
                        : new ExcelPackage(new FileInfo(filePath), password))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        if (worksheet.Dimension != null)
                        {
                            int rCount = worksheet.Dimension.Rows;
                            int cCount = worksheet.Dimension.Columns;
                            for (int r = 1; r <= rCount; r++)
                            {
                                var rowObj = new List<string>();
                                for (int c = 1; c <= cCount; c++)
                                {
                                    rowObj.Add(worksheet.Cells[r, c].Text?.Trim() ?? "");
                                }
                                grid.Add(rowObj);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (string.IsNullOrEmpty(password))
                    {
                        throw; // Parola verilmediyse (örneğin ilk deneme) exception UI'a gitsin ki şifre istesin
                    }

                    // EPPlus şifre olmasına rağmen patladıysa ExcelDataReader'a geç (Daha kapsamlı şifreleme tiplerini destekler)
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        var conf = new ExcelDataReader.ExcelReaderConfiguration() { Password = password };
                        try
                        {
                            using (var reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream, conf))
                            {
                                var dataSet = reader.AsDataSet();
                                var table = dataSet.Tables[0];
                                for (int r = 0; r < table.Rows.Count; r++)
                                {
                                    var rowObj = new List<string>();
                                    for (int c = 0; c < table.Columns.Count; c++)
                                    {
                                        rowObj.Add(table.Rows[r][c]?.ToString()?.Trim() ?? "");
                                    }
                                    grid.Add(rowObj);
                                }
                            }
                        }
                        catch
                        {
                            throw new Exception("Şifre hatalı veya Excel formatı okunamadı.");
                        }
                    }
                }

                if (grid.Count == 0) return messageLogs;

                int rowCount = grid.Count;
                int colCount = grid[0].Count;

                int phoneColIndex = 0;
                var headers = new Dictionary<int, string>();
                bool hasHeaders = false;

                for (int c = 0; c < colCount; c++)
                {
                    var cellValue = grid[0][c];
                    if (!string.IsNullOrEmpty(cellValue))
                    {
                        headers[c] = cellValue;
                        hasHeaders = true;
                        if (cellValue.Equals("Telefon", StringComparison.OrdinalIgnoreCase) ||
                            cellValue.Equals("Numara", StringComparison.OrdinalIgnoreCase) ||
                            cellValue.Equals("Phone", StringComparison.OrdinalIgnoreCase) ||
                            cellValue.Equals("Tel", StringComparison.OrdinalIgnoreCase) ||
                            cellValue.Equals("Referans / Telefon", StringComparison.OrdinalIgnoreCase) ||
                            cellValue.Equals("Referans", StringComparison.OrdinalIgnoreCase))
                        {
                            phoneColIndex = c;
                        }
                    }
                }

                int startRow = hasHeaders ? 1 : 0;
                int consecutiveEmptyRows = 0;

                for (int row = startRow; row < rowCount; row++)
                {
                    bool isRowEmpty = true;
                    for (int c = 0; c < colCount; c++)
                    {
                        if (!string.IsNullOrWhiteSpace(grid[row][c])) { isRowEmpty = false; break; }
                    }

                    if (isRowEmpty)
                    {
                        consecutiveEmptyRows++;
                        if (consecutiveEmptyRows > 10) break;
                        continue;
                    }

                    consecutiveEmptyRows = 0;

                    var phoneValue = grid[row][phoneColIndex];
                    string formattedNumber = string.IsNullOrWhiteSpace(phoneValue) ? "" : phoneValue.FormatTurkishPhone();

                    var log = new MessageLog
                    {
                        PhoneNumber = formattedNumber ?? "",
                        Status = MessageStatus.Pending
                    };

                    var offerList = new List<string>();
                    decimal minPrice = decimal.MaxValue;
                    string minCompany = "";
                    string rawMinPriceStr = "";

                    var excludedExact = new[] { "S.N", "S.No", "Sıra", "Ad", "Soyad", "TC", "VKN", "Yıl" };
                    var excludedContains = new[] { "Tarih", "tarih", "İsim", "isim", "Tür", "Plaka", "Belge", "Marka", "Referans", "Telefon", "Tel", "Durum", "Teklif No", "Teklif", "Şirket", "Sirket", "TC/VRG", "VRG" };

                    for (int c = 0; c < colCount; c++)
                    {
                        if (c == phoneColIndex) continue;

                        string headerKey = headers.ContainsKey(c) ? headers[c] : $"Sütun_{c}";
                        string cellValue = grid[row][c];
                        log.Parameters[headerKey] = cellValue;

                        if (!string.IsNullOrWhiteSpace(cellValue))
                        {
                            bool isExcluded = excludedExact.Any(eh => headerKey.Equals(eh, StringComparison.OrdinalIgnoreCase));
                            if (!isExcluded)
                            {
                                isExcluded = excludedContains.Any(eh => headerKey.IndexOf(eh, StringComparison.OrdinalIgnoreCase) >= 0);
                            }

                            if (!isExcluded)
                            {
                                if (TryParsePrice(cellValue, out decimal price))
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
                        }
                    }

                    if (offerList.Any())
                    {
                        log.Parameters["Teklifler"] = string.Join("\n", offerList);
                        log.Parameters["EnUygunFiyat"] = rawMinPriceStr;
                        log.Parameters["EnUygunSirket"] = minCompany;
                        log.Parameters["EnUygunTeklif"] = $"{minCompany}: {rawMinPriceStr}";
                    }

                    messageLogs.Add(log);
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
        /// Çapraz Satış Raporu — Eksik Poliçe sütunu dahil, kampanya türüne özgü formatlı Excel raporu yazar.
        /// ExportToExcelAsync'e dokunmaz (overload pattern).
        /// </summary>
        public async Task ExportCrossSellReportAsync(
            List<MessageLog> logs,
            string savePath,
            string campaignType)
        {
            await Task.Run(() =>
            {
                using var package = new ExcelPackage();
                var ws = package.Workbook.Worksheets.Add("Çapraz Satış Raporu");

                ws.Cells[1, 1].Value = "Telefon";
                ws.Cells[1, 2].Value = "Ad Soyad";
                ws.Cells[1, 3].Value = "Mevcut Poliçe";
                ws.Cells[1, 4].Value = "Eksik Poliçe (Teklif Edilecek)";
                ws.Cells[1, 5].Value = "En Uygun Fiyat";
                ws.Cells[1, 6].Value = "En Uygun Şirket";
                ws.Cells[1, 7].Value = "Plaka";
                ws.Cells[1, 8].Value = "Durum";

                string missingPolicy = campaignType switch
                {
                    "Trafik'ten İMM'ye"   => "İMM",
                    "Trafik'ten Kasko'ya" => "Kasko",
                    "DASK'tan Konut'a"    => "Konut Sigortası",
                    "TSS Kampanyası"      => "TSS",
                    _                     => "Bilinmiyor"
                };

                using (var hdr = ws.Cells[1, 1, 1, 8])
                {
                    hdr.Style.Font.Bold = true;
                    hdr.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    hdr.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(55, 71, 79));
                    hdr.Style.Font.Color.SetColor(System.Drawing.Color.White);
                }

                int row = 2;
                foreach (var log in logs)
                {
                    ws.Cells[row, 1].Value = log.PhoneNumber;

                    string adSoyad = "";
                    foreach (var kvp in log.Parameters)
                    {
                        if (kvp.Key.IndexOf("Ad", StringComparison.OrdinalIgnoreCase) >= 0 &&
                            (kvp.Key.IndexOf("Soyad", StringComparison.OrdinalIgnoreCase) >= 0 ||
                             kvp.Key.Length <= 5))
                        { adSoyad = kvp.Value ?? ""; break; }
                    }
                    ws.Cells[row, 2].Value = adSoyad;

                    string tur = "";
                    if (log.Parameters.TryGetValue("Tür", out var turVal)) tur = turVal;
                    else if (log.Parameters.TryGetValue("Tur", out var turVal2)) tur = turVal2;
                    ws.Cells[row, 3].Value = tur;
                    ws.Cells[row, 4].Value = missingPolicy;
                    ws.Cells[row, 5].Value = log.Parameters.TryGetValue("EnUygunFiyat",  out var fiyat)  ? fiyat  : "";
                    ws.Cells[row, 6].Value = log.Parameters.TryGetValue("EnUygunSirket", out var sirket) ? sirket : "";

                    string plaka = "";
                    if (log.Parameters.TryGetValue("Plaka", out var plakaVal)) plaka = plakaVal;
                    ws.Cells[row, 7].Value = plaka;
                    ws.Cells[row, 8].Value = log.Status.ToString();

                    if (row % 2 == 0)
                    {
                        using var rowBg = ws.Cells[row, 1, row, 8];
                        rowBg.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        rowBg.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(245, 245, 250));
                    }
                    row++;
                }

                ws.Cells.AutoFitColumns();
                package.SaveAs(new FileInfo(savePath));
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

                        if (!TryParseTurkishDate(dateStr, out DateTime parsedDate)) return false;
                        return parsedDate.Date >= startDate.Value.Date && parsedDate.Date <= endDate.Value.Date;
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

        private string GetCustomerGroupingKey(MessageLog log)
        {
            string tc = "";
            string vkn = "";
            string adi = "";
            string telefon = log.PhoneNumber ?? "";

            foreach (var kvp in log.Parameters)
            {
                var k = kvp.Key?.Trim() ?? "";
                var v = kvp.Value?.Trim() ?? "";

                if (k.Equals("TC", StringComparison.OrdinalIgnoreCase) ||
                    k.IndexOf("TC Kimlik", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    k.Equals("TC/VRG", StringComparison.OrdinalIgnoreCase) ||
                    k.Equals("TCNO", StringComparison.OrdinalIgnoreCase) ||
                    k.IndexOf("Kimlik", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (!string.IsNullOrWhiteSpace(v)) tc = v;
                }

                if (k.Equals("VKN", StringComparison.OrdinalIgnoreCase) ||
                    k.IndexOf("Vergi", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (!string.IsNullOrWhiteSpace(v)) vkn = v;
                }

                if (k.Equals("Müşteri", StringComparison.OrdinalIgnoreCase) ||
                    k.IndexOf("Ad Soyad", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    k.IndexOf("Adı Soyadı", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    k.Equals("Ad", StringComparison.OrdinalIgnoreCase) ||
                    k.Equals("İsim", StringComparison.OrdinalIgnoreCase) ||
                    k.Equals("Müşteri Adı", StringComparison.OrdinalIgnoreCase) ||
                    k.Equals("Sigortalı Adı Soyadı/Unvanı", StringComparison.OrdinalIgnoreCase))
                {
                    if (!string.IsNullOrWhiteSpace(v)) adi = v;
                }
            }

            // Öncelik: TC > VKN > Telefon > İsim (isim tek başına yeterince benzersiz değil)
            if (!string.IsNullOrWhiteSpace(tc)) return "TC_" + tc;
            if (!string.IsNullOrWhiteSpace(vkn)) return "VKN_" + vkn;
            if (!string.IsNullOrWhiteSpace(telefon) && telefon.Length >= 10) return "TEL_" + telefon;
            if (!string.IsNullOrWhiteSpace(adi)) return "AD_" + adi.ToLowerInvariant();

            return Guid.NewGuid().ToString();
        }

        private DateTime ParseLogDate(MessageLog log)
        {
            foreach (var kvp in log.Parameters)
            {
                var k = kvp.Key?.Trim() ?? "";
                if (k.IndexOf("Tarih", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    k.IndexOf("Vade", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    k.IndexOf("Tanzim", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    k.IndexOf("Bitiş", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    k.IndexOf("Son", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    k.IndexOf("Başlangıç", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    if (TryParseTurkishDate(kvp.Value, out DateTime parsedDt))
                    {
                        return parsedDt;
                    }
                }
            }
            // Bulamazsa en eskiye atsın ki yeni olanlar üste çıksın
            return DateTime.MinValue;
        }

        /// <summary>
        /// Çapraz Satış ve Kampanya hedeflerini filtreler.
        /// </summary>
        public List<MessageLog> GetCrossSellTargets(List<MessageLog> allLogs, string campaignType)
        {
            var targets = new List<MessageLog>();

            if (string.IsNullOrWhiteSpace(campaignType))
                return targets;

            var groupedLogs = allLogs.GroupBy(l => GetCustomerGroupingKey(l));

            foreach (var rawGroup in groupedLogs)
            {
                // Grubu en yeni tarihe göre (Descending) sırala.
                // Böylece hem değerlendirme hem de Excel'e aktarılacak hedef log (targetLog) en güncel veri olur.
                var group = rawGroup.OrderByDescending(l => ParseLogDate(l)).ToList();

                bool hasTrafik = false;
                bool hasIMM = false;
                bool hasKasko = false;
                bool hasTSS = false;
                bool hasDask = false;
                bool hasKonut = false;

                foreach (var log in group)
                {
                    bool isTrafikRow = false;
                    bool isIMMRow = false;
                    bool isKaskoRow = false;
                    bool isTSSRow = false;
                    bool isDaskRow = false;
                    bool isKonutRow = false;

                    foreach (var kvp in log.Parameters)
                    {
                        var k = kvp.Key ?? "";
                        var v = kvp.Value ?? "";

                        // Yeni "Üretim Listesi" formatı (Tür veya Branş kolonunda poliçe tipi yazması durumu)
                        if (k.IndexOf("Tür", StringComparison.OrdinalIgnoreCase) >= 0 || k.IndexOf("Tur", StringComparison.OrdinalIgnoreCase) >= 0 || k.IndexOf("Branş", StringComparison.OrdinalIgnoreCase) >= 0 || k.IndexOf("Brans", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            if (v.IndexOf("Trafik", StringComparison.OrdinalIgnoreCase) >= 0) isTrafikRow = true;
                            if (v.IndexOf("İMM", StringComparison.OrdinalIgnoreCase) >= 0 || v.IndexOf("IMM", StringComparison.OrdinalIgnoreCase) >= 0) isIMMRow = true;
                            if (v.IndexOf("Kasko", StringComparison.OrdinalIgnoreCase) >= 0) isKaskoRow = true;
                            if (v.IndexOf("TSS", StringComparison.OrdinalIgnoreCase) >= 0 || v.IndexOf("Sağlık", StringComparison.OrdinalIgnoreCase) >= 0 || v.IndexOf("Saglik", StringComparison.OrdinalIgnoreCase) >= 0) isTSSRow = true;
                            if (v.IndexOf("DASK", StringComparison.OrdinalIgnoreCase) >= 0 || v.IndexOf("Deprem", StringComparison.OrdinalIgnoreCase) >= 0) isDaskRow = true;
                            if (v.IndexOf("Konut", StringComparison.OrdinalIgnoreCase) >= 0) isKonutRow = true;
                        }
                        else
                        {
                            // Eski format veya başlıkta poliçe isminin geçmesi durumu
                            if (k.IndexOf("Trafik", StringComparison.OrdinalIgnoreCase) >= 0 && !string.IsNullOrWhiteSpace(v)) isTrafikRow = true;
                            if ((k.IndexOf("İMM", StringComparison.OrdinalIgnoreCase) >= 0 || k.IndexOf("IMM", StringComparison.OrdinalIgnoreCase) >= 0) && !string.IsNullOrWhiteSpace(v)) isIMMRow = true;
                            if (k.IndexOf("Kasko", StringComparison.OrdinalIgnoreCase) >= 0 && !string.IsNullOrWhiteSpace(v)) isKaskoRow = true;
                            if ((k.IndexOf("TSS", StringComparison.OrdinalIgnoreCase) >= 0 || k.IndexOf("Sağlık", StringComparison.OrdinalIgnoreCase) >= 0 || k.IndexOf("Saglik", StringComparison.OrdinalIgnoreCase) >= 0) && !string.IsNullOrWhiteSpace(v)) isTSSRow = true;
                            if (k.IndexOf("DASK", StringComparison.OrdinalIgnoreCase) >= 0 && !string.IsNullOrWhiteSpace(v)) isDaskRow = true;
                            if (k.IndexOf("Konut", StringComparison.OrdinalIgnoreCase) >= 0 && !string.IsNullOrWhiteSpace(v)) isKonutRow = true;
                        }

                    }

                    if (isTrafikRow) hasTrafik = true;
                    if (isIMMRow) hasIMM = true;
                    if (isKaskoRow) hasKasko = true;
                    if (isTSSRow) hasTSS = true;
                    if (isDaskRow) hasDask = true;
                    if (isKonutRow) hasKonut = true;
                }

                bool isTarget = false;

                if (campaignType == "Trafik'ten İMM'ye")
                {
                    if (hasTrafik && !hasIMM) isTarget = true;
                }
                else if (campaignType == "Trafik'ten Kasko'ya")
                {
                    if (hasTrafik && !hasKasko) isTarget = true;
                }
                else if (campaignType == "TSS Kampanyası")
                {
                    if (!hasTSS) isTarget = true;
                }
                else if (campaignType == "DASK'tan Konut'a")
                {
                    if (hasDask && !hasKonut) isTarget = true;
                }

                if (isTarget)
                {
                    // Raporlama ve gönderim için müşteriyi temsil edecek mantıklı bir satır seçelim.
                    MessageLog? representativeLog = null;
                    if (campaignType == "Trafik'ten İMM'ye" || campaignType == "Trafik'ten Kasko'ya")
                        representativeLog = group.FirstOrDefault(l => l.Parameters.Values.Any(v => v?.IndexOf("Trafik", StringComparison.OrdinalIgnoreCase) >= 0));
                    else if (campaignType == "DASK'tan Konut'a")
                        representativeLog = group.FirstOrDefault(l => l.Parameters.Values.Any(v => v?.IndexOf("DASK", StringComparison.OrdinalIgnoreCase) >= 0));

                    if (representativeLog == null) representativeLog = group.First();

                    // Original listeyi etkilememek için clone oluşturalım
                    var targetLog = new MessageLog
                    {
                        PhoneNumber = representativeLog.PhoneNumber,
                        Status = MessageStatus.Pending,
                        Parameters = new Dictionary<string, string>(representativeLog.Parameters)
                    };
                    targets.Add(targetLog);
                }
            }

            return targets;
        }
        /// <summary>
        /// Hücre değerini fiyat olarak parse eder. 100'den büyükse true döner ve fiyatı verir.
        /// Önce TR kültürü (nokta=bin ayrımcı, virgül=ondalık), sonra InvariantCulture dener.
        /// </summary>
        private static bool TryParsePrice(string cellValue, out decimal price)
        {
            price = 0;
            string clean = cellValue
                .Replace("₺", "").Replace("TL", "").Replace("tl", "")
                .Replace(" ", "").Trim();
            clean = new string(clean.Where(ch => char.IsDigit(ch) || ch == ',' || ch == '.').ToArray());

            if (string.IsNullOrEmpty(clean)) return false;

            if (decimal.TryParse(clean, System.Globalization.NumberStyles.Any,
                new System.Globalization.CultureInfo("tr-TR"), out price))
                return price > 100;

            if (decimal.TryParse(clean, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out price))
                return price > 100;

            return false;
        }

        /// <summary>
        /// Türk tarih formatlarını destekleyen genişletilmiş tarih parse metodu.
        /// dd.MM.yyyy, d.M.yyyy, dd/MM/yyyy, yyyy-MM-dd ve kısa yıl formatlarını dener.
        /// </summary>
        private static bool TryParseTurkishDate(string dateStr, out DateTime result)
        {
            result = default;
            if (string.IsNullOrWhiteSpace(dateStr)) return false;

            string[] formats =
            {
                "dd.MM.yyyy", "d.M.yyyy", "dd/MM/yyyy", "d/M/yyyy",
                "yyyy-MM-dd", "MM/dd/yyyy", "dd.MM.yy", "d.M.yy"
            };

            var cultures = new[]
            {
                new System.Globalization.CultureInfo("tr-TR"),
                System.Globalization.CultureInfo.InvariantCulture
            };

            foreach (var culture in cultures)
            {
                if (DateTime.TryParseExact(dateStr.Trim(), formats, culture,
                    System.Globalization.DateTimeStyles.None, out result))
                    return true;
                if (DateTime.TryParse(dateStr.Trim(), culture,
                    System.Globalization.DateTimeStyles.None, out result))
                    return true;
            }
            return false;
        }
    }
}
