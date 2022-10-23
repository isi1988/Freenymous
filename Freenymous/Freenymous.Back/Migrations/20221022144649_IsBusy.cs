using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freenymous.Back.Migrations
{
    public partial class IsBusy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBusy",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBusy",
                table: "Users");
        }
    }
}
