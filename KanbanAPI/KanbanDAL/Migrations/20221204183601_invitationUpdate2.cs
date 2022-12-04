using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanbanDAL.Migrations
{
    public partial class invitationUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Invitations_BoardId",
                table: "Invitations",
                column: "BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Boards_BoardId",
                table: "Invitations",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Boards_BoardId",
                table: "Invitations");

            migrationBuilder.DropIndex(
                name: "IX_Invitations_BoardId",
                table: "Invitations");
        }
    }
}
