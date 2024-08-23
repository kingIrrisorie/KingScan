using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIManga.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoGeneros4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "MangaId",
                table: "Genres");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MangaId",
                table: "Genres",
                type: "int",
                nullable: true);
        }
    }
}
