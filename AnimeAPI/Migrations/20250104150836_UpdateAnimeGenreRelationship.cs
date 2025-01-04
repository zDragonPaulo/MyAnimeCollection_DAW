using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAnimeGenreRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Animes_AnimeId",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Animes_AnimeId1",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_AnimeId",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_AnimeId1",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "AnimeId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "AnimeId1",
                table: "Genres");

            migrationBuilder.CreateTable(
                name: "AnimeGenres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    AnimeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeGenres", x => new { x.AnimeId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_AnimeGenres_Animes_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Animes",
                        principalColumn: "AnimeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeGenres_GenreId",
                table: "AnimeGenres",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeGenres");

            migrationBuilder.AddColumn<int>(
                name: "AnimeId",
                table: "Genres",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AnimeId1",
                table: "Genres",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_AnimeId",
                table: "Genres",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_AnimeId1",
                table: "Genres",
                column: "AnimeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Animes_AnimeId",
                table: "Genres",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "AnimeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Animes_AnimeId1",
                table: "Genres",
                column: "AnimeId1",
                principalTable: "Animes",
                principalColumn: "AnimeId");
        }
    }
}
