using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAnimeCollection.Migrations
{
    /// <inheritdoc />
    public partial class AlterarNomesAtributosv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Aniversario",
                table: "Utilizadores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aniversario",
                table: "Utilizadores");
        }
    }
}
