using System.ComponentModel.DataAnnotations;
public class ListaAnime
{
    [Key]
    public int ListaAnimeId { get; set; }
    public string AnimeId { get; set; } // ID do anime vindo da API Jikan
    public int ListaUtilizadorId { get; set; }
    public ListaUtilizador ListaUtilizador { get; set; }
}
