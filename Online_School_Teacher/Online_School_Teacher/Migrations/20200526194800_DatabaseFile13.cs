using Microsoft.EntityFrameworkCore.Migrations;

namespace Online_School_Teacher.Migrations
{
    public partial class DatabaseFile13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tutorial_Course_CourseID",
                table: "Tutorial");

            migrationBuilder.DropIndex(
                name: "IX_Tutorial_CourseID",
                table: "Tutorial");

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "Tutorial");

            migrationBuilder.AddColumn<int>(
                name: "CID",
                table: "Tutorial",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CID",
                table: "Tutorial");

            migrationBuilder.AddColumn<int>(
                name: "CourseID",
                table: "Tutorial",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tutorial_CourseID",
                table: "Tutorial",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tutorial_Course_CourseID",
                table: "Tutorial",
                column: "CourseID",
                principalTable: "Course",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
