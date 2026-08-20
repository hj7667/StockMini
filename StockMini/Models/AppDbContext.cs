using Microsoft.EntityFrameworkCore;

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
            optionsBuilder.UseSqlServer(
                @"Server=localhost\SQLEXPRESS;Database=StockMiniDB;Trusted_Connection=True;TrustServerCertificate=True;"
            // Server: 아까 SSMS에서 접속했던 그 서버
            // Database: 새로 만들 DB 이름 (마이그레이션 시 자동 생성됨)
            // Trusted_Connection: Windows 인증 사용
            // TrustServerCertificate: 로컬 인증서 그냥 신뢰하겠다는 설정
            );
        }
    }
}