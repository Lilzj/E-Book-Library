using Microsoft.EntityFrameworkCore.Migrations;

namespace E_library.Lib.Data.Migrations
{
    public partial class removedDescriptionfromGenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Genres");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Genres",
                type: "TEXT",
                nullable: true);
        }
    }
}
