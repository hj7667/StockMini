using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace StockMini.Models
{
    // DB와 연결하고 관리하는 클래스 (EF Core의 핵심)
    public class AppDbContext : DbContext
    {
        // Items 라는 이름으로 Item 클래스와 매칭되는 테이블을 자동으로 다룸
        public DbSet<Item> Items { get; set; }

        // DB 연결 설정 (어떤 서버, 어떤 DB에 붙을지)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                        .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true)
                                        .Build();

                // 파일이 둘 다 없거나 실패하면 기본 연결 문자열 사용
                string connectionString = configuration.GetConnectionString("DefaultConnection")
                    ?? @"Server=localhost\SQLEXPRESS01;Database=StockMiniDB;Trusted_Connection=True;TrustServerCertificate=True;";

                optionsBuilder.UseSqlServer(connectionString);

                // 3. SQL Server 설정 적용
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}