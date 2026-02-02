using Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UrlShortnerContext : DbContext
    {
        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        
        public UrlShortnerContext(DbContextOptions<UrlShortnerContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedUrl>()
                .HasIndex(u => u.shortString)
                .IsUnique();
        }

    }
}
