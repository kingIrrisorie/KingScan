using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIManga.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoGeneros2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MangaGenre_Genres_GenresId",
                table: "MangaGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_MangaGenre_Mangas_MangasId",
                table: "MangaGenre");

            migrationBuilder.RenameColumn(
                name: "MangasId",
                table: "MangaGenre",
                newName: "MangaId");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "MangaGenre",
                newName: "GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_MangaGenre_MangasId",
                table: "MangaGenre",
                newName: "IX_MangaGenre_MangaId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_MangaGenre_Genres_GenreId",
                table: "MangaGenre",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MangaGenre_Mangas_MangaId",
                table: "MangaGenre",
                column: "MangaId",
                principalTable: "Mangas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MangaGenre_Genres_GenreId",
                table: "MangaGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_MangaGenre_Mangas_MangaId",
                table: "MangaGenre");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "MangaId",
                table: "Genres");

            migrationBuilder.RenameColumn(
                name: "MangaId",
                table: "MangaGenre",
                newName: "MangasId");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "MangaGenre",
                newName: "GenresId");

            migrationBuilder.RenameIndex(
                name: "IX_MangaGenre_MangaId",
                table: "MangaGenre",
                newName: "IX_MangaGenre_MangasId");

            migrationBuilder.AddForeignKey(
                name: "FK_MangaGenre_Genres_GenresId",
                table: "MangaGenre",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MangaGenre_Mangas_MangasId",
                table: "MangaGenre",
                column: "MangasId",
                principalTable: "Mangas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
