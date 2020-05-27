using Microsoft.EntityFrameworkCore.Migrations;

namespace Online_School_Teacher.Migrations
{
    public partial class DatabaseFile16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CID",
                table: "Tutorial",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CID",
                table: "Tutorial",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
