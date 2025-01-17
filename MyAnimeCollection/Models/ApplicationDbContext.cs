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
        public DbSet<AnimeListModel> AnimeLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da relação entre UserListAvaliationModel e UserModel
            modelBuilder.Entity<UserListAvaliationModel>()
                .HasOne(ula => ula.User)
                .WithMany(u => u.UserListAvaliation)
                .HasForeignKey(ula => ula.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configuração da relação entre UserAnimeAvaliationModel e UserModel
            modelBuilder.Entity<UserAnimeAvaliationModel>()
                .HasOne(uaa => uaa.User)
                .WithMany(u => u.UserAnimeAvaliation)
                .HasForeignKey(uaa => uaa.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configuração da relação entre UserListModel e UserModel
            // Configurar o relacionamento entre UserListModel e AnimeListModel
            modelBuilder.Entity<UserListModel>()
                .HasOne(ul => ul.AnimeList) // UserListModel tem uma referência a AnimeListModel
                .WithMany(al => al.UserList) // AnimeListModel pode ter muitos UserListModel
                .HasForeignKey(ul => ul.AnimeListId) // Chave estrangeira
                .OnDelete(DeleteBehavior.Cascade); // Defina o comportamento de exclusão (pode ser outro comportamento, dependendo do seu caso)
        }
    }
}