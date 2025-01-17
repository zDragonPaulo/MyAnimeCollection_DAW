using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAnimeCollection.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserListRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLists_Users_UserId",
                table: "UserLists");

            migrationBuilder.AddColumn<int>(
                name: "UserListModelUserListId",
                table: "AnimeLists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnimeLists_UserListModelUserListId",
                table: "AnimeLists",
                column: "UserListModelUserListId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeLists_UserLists_UserListModelUserListId",
                table: "AnimeLists",
                column: "UserListModelUserListId",
                principalTable: "UserLists",
                principalColumn: "UserListId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLists_Users_UserId",
                table: "UserLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeLists_UserLists_UserListModelUserListId",
                table: "AnimeLists");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLists_Users_UserId",
                table: "UserLists");

            migrationBuilder.DropIndex(
                name: "IX_AnimeLists_UserListModelUserListId",
                table: "AnimeLists");

            migrationBuilder.DropColumn(
                name: "UserListModelUserListId",
                table: "AnimeLists");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLists_Users_UserId",
                table: "UserLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
