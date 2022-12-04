using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanbanDAL.Migrations
{
    public partial class invitationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvitingEmail",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvitingEmail",
                table: "Invitations");
        }
    }
}
