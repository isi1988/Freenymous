using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Freenymous.Back.Migrations
{
    public partial class AvatarToBytes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarName",
                table: "Users");

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                table: "Users",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "AvatarName",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
