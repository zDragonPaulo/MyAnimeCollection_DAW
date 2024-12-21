using System.ComponentModel.DataAnnotations;
public class ListaUtilizador
{
    [Key]
    public int ListaUtilizadorId { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }

    public int UtilizadorId { get; set; }
    public Utilizador? Utilizador { get; set; }

    public ICollection<ListaAnime> ListaAnimes { get; set; } = new List<ListaAnime>();
    public ICollection<AvaliacaoListaUtilizador> Avaliacoes { get; set; } = new List<AvaliacaoListaUtilizador>();
}
