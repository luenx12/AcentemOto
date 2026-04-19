using System;
using System.Windows.Forms;
using AcentemOto.Data;
using AcentemOto.Forms;

namespace AcentemOto
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Veritabanı şemasını uygulama başlarken bir kez oluştur/doğrula
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}