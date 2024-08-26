using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIManga.Context;
using APIManga.Model;
using APIManga.DTOs;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NuGet.Versioning;
using Humanizer.Localisation;
using APIManga.Services;

namespace APIManga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MangasController : ControllerBase
    {
        private readonly KingIrrisorieScanContext _context;

        public MangasController(KingIrrisorieScanContext context)
        {
            _context = context;
        }

        // GET: api/Mangas/{title}
        [HttpGet("search/{title}")]
        public async Task<ActionResult<IEnumerable<Manga>>> GetMangas(string title)
        {
            var mangas = await _context.Mangas
            .Include(m => m.Author)
            .Include(m => m.Genres)
            .Where(m => m.Title.Contains(title))
            .Select(m => new MangaDTO
            {
                Id = m.Id, // Inclua o Id na projeção
                Title = m.Title,
                Status = m.Status,
                Description = m.Description,
                Released = m.Released,
                ThumbnailURL = m.ThumbnailURL,
                AuthorName = m.Author != null ? m.Author.Name : null,
                GenreNames = m.Genres.Select(g => g.Name).ToList()
            })
            .ToListAsync();


            if (mangas == null || !mangas.Any())
                return NotFound();

            return Ok(mangas);
        }


        // GET: api/Manga/5
        [HttpGet("id:{id}")]
        public async Task<ActionResult<MangaDTO>> GetManga(int id)
        {
			if (!MangaExists(id))
				return NotFound();

            var manga = await _context.Mangas
                .Include(a => a.Author)
                .Include(g => g.Genres)
                .Where(i => i.Id == id)
                .Select(m => new MangaDTO
                {
                    Id = m.Id,
                    Title = m.Title,
                    Status = m.Status,
                    Description = m.Description,
                    Released = m.Released,
                    ThumbnailURL = m.ThumbnailURL,
                    AuthorName = m.Author != null ? m.Author.Name : null,
                    GenreNames = m.Genres.Select(g => g.Name).ToList()
                }).FirstOrDefaultAsync();

            if (manga == null)
                return NotFound();

            return manga;
        }

        // PUT: api/Manga/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateManga(int id, MangaUpCreate mangaDto)
        {
            var authorController = new AuthorController(_context);
            // Verifica se o Manga existe
            var mangaToUpdate = await _context.Mangas
                .Include(m => m.Genres)
                .Include(m => m.Author).
                FirstOrDefaultAsync(m => m.Id == id);
            if (mangaToUpdate == null)
            {
                return NotFound();
            }

            // Atualiza as propriedades do Manga
            MangaServices.AddBasicParameters(mangaToUpdate, mangaDto);

            if (!string.IsNullOrEmpty(mangaDto.AuthorName))
            {
                IActionResult authorResult = await authorController.AddAuthor(mangaDto.AuthorName, ReturnAuthor: true);

                if (authorResult is OkObjectResult okResult && okResult.Value is Author author)
                {
                    mangaToUpdate.Author = author;
                }
            }

            if (mangaDto.GenreNames != null)
            {
                var genreNames = mangaDto.GenreNames.Distinct().ToList(); // Remove nomes duplicados

                // Obtém todos os gêneros existentes no banco
                var existingGenres = await _context.Genres
                    .Where(g => genreNames.Contains(g.Name))
                    .ToListAsync();

                // Obtém os gêneros que não estão no banco e cria novos
                var newGenreNames = genreNames.Except(existingGenres.Select(g => g.Name)).ToList();
                var newGenres = newGenreNames.Select(name => new Gender { Name = name }).ToList();

                if (newGenres.Any())
                {
                    _context.Genres.AddRange(newGenres);
                    await _context.SaveChangesAsync(); // Salva novos gêneros no banco de dados
                }

                // Atualiza a coleção de gêneros do manga
                mangaToUpdate.Genres = existingGenres.Concat(newGenres).ToList();
            }

            // Marca a entidade como modificada
            _context.Entry(mangaToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return StatusCode(StatusCodes.Status204NoContent);
        }


        // POST: api/Manga
        [HttpPost]
        public async Task<ActionResult> CreateManga(MangaUpCreate dto)
        {
            var authorController = new AuthorController(_context);
            Manga manga = new Manga();

            MangaServices.AddBasicParameters(manga, dto);

            if (!string.IsNullOrEmpty(dto.AuthorName))
            {
                IActionResult authorResult = await authorController.AddAuthor(dto.AuthorName, ReturnAuthor: true);

                if (authorResult is OkObjectResult okResult && okResult.Value is Author author)
                {
                    manga.Author = author;
                }
            }

            // Tratamento dos gêneros
            if (dto.GenreNames != null)
            {
                foreach (var genreName in dto.GenreNames)
                {
                    var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == genreName);
                    if (genre == null)
                    {
                        genre = new Gender { Name = genreName};
                        _context.Genres.Add(genre);
                        await _context.SaveChangesAsync();
                    }
                    manga.Genres.Add(genre);
                }
            }
            // "acao", "gore", "drama"
            // Adiciona o Manga ao contexto e salva as alterações
            _context.Mangas.Add(manga);
            await _context.SaveChangesAsync();

            return NoContent();
		}

        // DELETE: api/Manga/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManga(int id)
        {
			if (!MangaExists(id))
				return NotFound();
			var manga = await _context.Mangas.FindAsync(id);

            _context.Mangas.Remove(manga);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MangaExists(int id)
        {
            return _context.Mangas.Any(e => e.Id == id);
        }
    }
}
