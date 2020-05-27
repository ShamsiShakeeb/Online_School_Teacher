using Microsoft.EntityFrameworkCore.Migrations;

namespace Online_School_Teacher.Migrations
{
    public partial class DatabaseFile3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Teacher_Basic_Information",
                table: "Teacher_Basic_Information");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student_Basic_Information",
                table: "Student_Basic_Information");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Teacher_Basic_Information");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Student_Basic_Information");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Teacher_Basic_Information",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Student_Basic_Information",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teacher_Basic_Information",
                table: "Teacher_Basic_Information",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student_Basic_Information",
                table: "Student_Basic_Information",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Teacher_Basic_Information",
                table: "Teacher_Basic_Information");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student_Basic_Information",
                table: "Student_Basic_Information");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Teacher_Basic_Information");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Student_Basic_Information");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Teacher_Basic_Information",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Student_Basic_Information",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teacher_Basic_Information",
                table: "Teacher_Basic_Information",
                column: "Email");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student_Basic_Information",
                table: "Student_Basic_Information",
                column: "Email");
        }
    }
}
