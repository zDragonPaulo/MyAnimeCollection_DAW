using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAnimeCollection.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAnimeListModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLists_AnimeLists_AnimeListId",
                table: "UserLists");

            migrationBuilder.DropTable(
                name: "AnimeLists");

            migrationBuilder.DropIndex(
                name: "IX_UserLists_AnimeListId",
                table: "UserLists");

            migrationBuilder.DropColumn(
                name: "AnimeListId",
                table: "UserLists");

            migrationBuilder.AddColumn<string>(
                name: "AnimeIds",
                table: "UserLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnimeIds",
                table: "UserLists");

            migrationBuilder.AddColumn<int>(
                name: "AnimeListId",
                table: "UserLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AnimeLists",
                columns: table => new
                {
                    AnimeListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimeId = table.Column<int>(type: "int", nullable: false),
                    UserListModelUserListId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeLists", x => x.AnimeListId);
                    table.ForeignKey(
                        name: "FK_AnimeLists_UserLists_UserListModelUserListId",
                        column: x => x.UserListModelUserListId,
                        principalTable: "UserLists",
                        principalColumn: "UserListId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLists_AnimeListId",
                table: "UserLists",
                column: "AnimeListId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeLists_UserListModelUserListId",
                table: "AnimeLists",
                column: "UserListModelUserListId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLists_AnimeLists_AnimeListId",
                table: "UserLists",
                column: "AnimeListId",
                principalTable: "AnimeLists",
                principalColumn: "AnimeListId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
