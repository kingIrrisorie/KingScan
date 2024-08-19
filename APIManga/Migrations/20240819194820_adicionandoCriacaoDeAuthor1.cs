using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIManga.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoCriacaoDeAuthor1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Mangas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mangas_AuthorId",
                table: "Mangas",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mangas_Authors_AuthorId",
                table: "Mangas",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mangas_Authors_AuthorId",
                table: "Mangas");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Mangas_AuthorId",
                table: "Mangas");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Mangas");
        }
    }
}
