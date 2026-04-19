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
            
            // SQLite aynı anda birden fazla iş parcacağının veri tabanına erişimi halinde kitlenmemesi için 5 saniye bekleme süresi
            optionsBuilder.UseSqlite($"Data Source={dbPath};Cache=Shared;Mode=ReadWriteCreate;BusyTimeout=5000;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageLog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Status).HasConversion<string>();
                entity.HasIndex(e => e.PhoneNumber);
                entity.HasIndex(e => e.Status);
            });
        }
    }
}
