using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAnimeCollection.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Biografia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AvaliacoesAnime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilizadorId = table.Column<int>(type: "int", nullable: false),
                    Nota = table.Column<int>(type: "int", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvaliacoesAnime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvaliacoesAnime_Utilizadores_UtilizadorId",
                        column: x => x.UtilizadorId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListasUtilizador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilizadorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListasUtilizador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListasUtilizador_Utilizadores_UtilizadorId",
                        column: x => x.UtilizadorId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvaliacoesListasUtilizador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilizadorId = table.Column<int>(type: "int", nullable: false),
                    ListaUtilizadorId = table.Column<int>(type: "int", nullable: false),
                    Avaliacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvaliacoesListasUtilizador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvaliacoesListasUtilizador_ListasUtilizador_ListaUtilizadorId",
                        column: x => x.ListaUtilizadorId,
                        principalTable: "ListasUtilizador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvaliacoesListasUtilizador_Utilizadores_UtilizadorId",
                        column: x => x.UtilizadorId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction); // Alterado para NoAction
                });

            migrationBuilder.CreateTable(
                name: "ListaAnimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListaUtilizadorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaAnimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListaAnimes_ListasUtilizador_ListaUtilizadorId",
                        column: x => x.ListaUtilizadorId,
                        principalTable: "ListasUtilizador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacoesAnime_UtilizadorId",
                table: "AvaliacoesAnime",
                column: "UtilizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacoesListasUtilizador_ListaUtilizadorId",
                table: "AvaliacoesListasUtilizador",
                column: "ListaUtilizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacoesListasUtilizador_UtilizadorId",
                table: "AvaliacoesListasUtilizador",
                column: "UtilizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ListaAnimes_ListaUtilizadorId",
                table: "ListaAnimes",
                column: "ListaUtilizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ListasUtilizador_UtilizadorId",
                table: "ListasUtilizador",
                column: "UtilizadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvaliacoesAnime");

            migrationBuilder.DropTable(
                name: "AvaliacoesListasUtilizador");

            migrationBuilder.DropTable(
                name: "ListaAnimes");

            migrationBuilder.DropTable(
                name: "ListasUtilizador");

            migrationBuilder.DropTable(
                name: "Utilizadores");
        }
    }
}
