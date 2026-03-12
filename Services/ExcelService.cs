using AcentemOto.Extensions;
using AcentemOto.Models;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AcentemOto.Services
{
    public class ExcelService
    {
        public ExcelService()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async Task<List<MessageLog>> ReadPhoneNumbersAsync(string filePath)
        {
            return await Task.Run(() =>
            {
                var messageLogs = new List<MessageLog>();

                if (!File.Exists(filePath))
                    throw new FileNotFoundException("Excel dosyası bulunamadı.");

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 1; row <= rowCount; row++)
                    {
                        var cellValue = worksheet.Cells[row, 1].Value?.ToString();

                        if (!string.IsNullOrWhiteSpace(cellValue))
                        {
                            string formattedNumber = cellValue.FormatTurkishPhone();
                            if (!string.IsNullOrEmpty(formattedNumber))
                            {
                                messageLogs.Add(new MessageLog
                                {
                                    PhoneNumber = formattedNumber,
                                    Status = MessageStatus.Pending
                                });
                            }
                        }
                    }
                }

                return messageLogs;
            });
        }
    }
}
