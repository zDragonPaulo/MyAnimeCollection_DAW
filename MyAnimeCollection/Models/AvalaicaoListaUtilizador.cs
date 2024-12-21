using System.ComponentModel.DataAnnotations;
public class AvaliacaoListaUtilizador
{
    [Key]
    public int AvaliacaoListaId { get; set; }
    public int UtilizadorId { get; set; }
    public Utilizador Utilizador { get; set; }
    public int ListaUtilizadorId { get; set; }
    public ListaUtilizador ListaUtilizador { get; set; }
    public int Avaliacao { get; set; }
}
