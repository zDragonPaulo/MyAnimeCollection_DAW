using System.ComponentModel.DataAnnotations;
public class AvaliacaoAnime
{
    [Key]
    public int AvaliacaoAnimeId { get; set; }
    public string AnimeId { get; set; } // ID do anime vindo da API Jikan
    public int UtilizadorId { get; set; }
    public Utilizador Utilizador { get; set; }
    public int Nota { get; set; }
    public string Comentario { get; set; }
}
