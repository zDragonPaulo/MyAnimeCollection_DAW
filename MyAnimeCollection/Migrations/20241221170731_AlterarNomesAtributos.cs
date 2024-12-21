using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAnimeCollection.Migrations
{
    /// <inheritdoc />
    public partial class AlterarNomesAtributos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Utilizadores",
                newName: "UtilizadorId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ListasUtilizador",
                newName: "ListaUtilizadorId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ListaAnimes",
                newName: "ListaAnimeId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AvaliacoesListasUtilizador",
                newName: "AvaliacaoListaId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AvaliacoesAnime",
                newName: "AvaliacaoAnimeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UtilizadorId",
                table: "Utilizadores",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ListaUtilizadorId",
                table: "ListasUtilizador",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ListaAnimeId",
                table: "ListaAnimes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AvaliacaoListaId",
                table: "AvaliacoesListasUtilizador",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AvaliacaoAnimeId",
                table: "AvaliacoesAnime",
                newName: "Id");
        }
    }
}
