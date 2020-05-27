using Microsoft.EntityFrameworkCore.Migrations;

namespace Online_School_Teacher.Migrations
{
    public partial class DatabaseFile11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CID",
                table: "Tutorial",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CID",
                table: "Tutorial");
        }
    }
}
