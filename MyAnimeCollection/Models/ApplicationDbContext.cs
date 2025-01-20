using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserListModel> UserLists { get; set; }
        public DbSet<UserListAvaliationModel> UserListAvaliations { get; set; }
        public DbSet<UserAnimeAvaliationModel> UserAnimeAvaliations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relation between UserListAvaliationModel and UserListModel
            modelBuilder.Entity<UserListAvaliationModel>()
                .HasOne(ula => ula.User)
                .WithMany(u => u.UserListAvaliation)
                .HasForeignKey(ula => ula.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relation between UserAnimeAvaliationModel and UserModel
            modelBuilder.Entity<UserAnimeAvaliationModel>()
                .HasOne(uaa => uaa.User)
                .WithMany(u => u.UserAnimeAvaliation)
                .HasForeignKey(uaa => uaa.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Relation between UserListModel and UserModel
            modelBuilder.Entity<UserListModel>()
                .HasOne(ul => ul.User)
                .WithMany(u => u.UserList)
                .HasForeignKey(ul => ul.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}