using Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UrlShortnerContext : DbContext
    {
        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        public string DbPath { get; }

        public UrlShortnerContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "urlShortner.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedUrl>()
                .HasIndex(u => u.shortString)
                .IsUnique();
        }

    }
}
