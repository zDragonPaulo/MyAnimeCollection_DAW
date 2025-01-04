using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Animes_AnimeId",
                table: "Genres");

            migrationBuilder.AlterColumn<int>(
                name: "AnimeId",
                table: "Genres",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnimeId1",
                table: "Genres",
                type: "int",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Animes_AnimeId",
                table: "Genres");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Animes_AnimeId1",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Genres_AnimeId1",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "AnimeId1",
                table: "Genres");

            migrationBuilder.AlterColumn<int>(
                name: "AnimeId",
                table: "Genres",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Animes_AnimeId",
                table: "Genres",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "AnimeId");
        }
    }
}
