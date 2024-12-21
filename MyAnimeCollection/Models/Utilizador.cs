using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
public class Utilizador
{
    [Key]
    public int UtilizadorId { get; set; } 
    public string Nome { get; set; }
    public int Idade { get; set; }
    public string Email { get; set; }
    public string Biografia { get; set; }
    public string Aniversario { get; set; }

    public ICollection<ListaUtilizador> Listas { get; set; }
    public ICollection<AvaliacaoAnime> AvaliacoesAnime { get; set; }
    public ICollection<AvaliacaoListaUtilizador> AvaliacoesListas { get; set; }
}
