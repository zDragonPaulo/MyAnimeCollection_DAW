using Microsoft.EntityFrameworkCore;
using AnimeAPI.Models;

namespace AnimeAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Anime> Animes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
