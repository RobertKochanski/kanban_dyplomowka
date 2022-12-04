using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanbanDAL.Migrations
{
    public partial class invitationUpdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InvitedAt",
                table: "Invitations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvitedAt",
                table: "Invitations");
        }
    }
}
