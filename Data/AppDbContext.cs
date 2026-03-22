using AcentemOto.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace AcentemOto.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<MessageLog> MessageLogs { get; set; }

        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Veritabanını uygulamanın çalıştığı klasörde oluşturur
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "messages.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath};Cache=Shared;Mode=ReadWriteCreate;Journal Mode=WAL;Default Timeout=5;");
        }
    }
}
