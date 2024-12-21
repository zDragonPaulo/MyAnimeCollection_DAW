using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // Tabelas do banco de dados
    public DbSet<Utilizador> Utilizadores { get; set; }
    public DbSet<ListaUtilizador> ListasUtilizador { get; set; }
    public DbSet<ListaAnime> ListaAnimes { get; set; }
    public DbSet<AvaliacaoAnime> AvaliacoesAnime { get; set; }
    public DbSet<AvaliacaoListaUtilizador> AvaliacoesListasUtilizador { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração de relacionamentos e restrições
        modelBuilder.Entity<ListaUtilizador>()
            .HasOne(l => l.Utilizador)
            .WithMany(u => u.Listas)
            .HasForeignKey(l => l.UtilizadorId);

        modelBuilder.Entity<ListaAnime>()
            .HasOne(la => la.ListaUtilizador)
            .WithMany(l => l.ListaAnimes)
            .HasForeignKey(la => la.ListaUtilizadorId);

        modelBuilder.Entity<AvaliacaoAnime>()
            .HasOne(aa => aa.Utilizador)
            .WithMany(u => u.AvaliacoesAnime)
            .HasForeignKey(aa => aa.UtilizadorId);

        modelBuilder.Entity<AvaliacaoListaUtilizador>()
            .HasOne(al => al.Utilizador)
            .WithMany(u => u.AvaliacoesListas)
            .HasForeignKey(al => al.UtilizadorId);

        modelBuilder.Entity<AvaliacaoListaUtilizador>()
            .HasOne(al => al.ListaUtilizador)
            .WithMany(l => l.Avaliacoes)
            .HasForeignKey(al => al.ListaUtilizadorId);
    }
}
