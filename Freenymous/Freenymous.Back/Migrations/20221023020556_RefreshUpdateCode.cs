using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freenymous.Back.Migrations
{
    public partial class RefreshUpdateCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshUpdateCode",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshUpdateCode",
                table: "Users");
        }
    }
}
