using Microsoft.EntityFrameworkCore.Migrations;

namespace E_library.Lib.Data.Migrations
{
    public partial class AddGenreTypeToGenreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GenreType",
                table: "Genres",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenreType",
                table: "Genres");
        }
    }
}
