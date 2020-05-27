using Microsoft.EntityFrameworkCore.Migrations;

namespace Online_School_Teacher.Migrations
{
    public partial class DatabaseFile7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approve",
                table: "Teacher");

            migrationBuilder.AddColumn<string>(
                name: "JWT_Token",
                table: "Teacher",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JWT_Token",
                table: "Student",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JWT_Token",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "JWT_Token",
                table: "Student");

            migrationBuilder.AddColumn<string>(
                name: "Approve",
                table: "Teacher",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
