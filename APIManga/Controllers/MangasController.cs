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
        public async Task<IActionResult> UpdateManga(int id, MangaDTO mangaDto)
        {
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
            mangaToUpdate.Title = mangaDto.Title;
            mangaToUpdate.Status = mangaDto.Status;
            mangaToUpdate.Description = mangaDto.Description;
            mangaToUpdate.Released = mangaDto.Released;
            mangaToUpdate.ThumbnailURL = mangaDto.ThumbnailURL;

            // Verifica se o nome do autor foi fornecido
            if (!string.IsNullOrEmpty(mangaDto.AuthorName))
            {
                // Tenta encontrar o autor pelo nome
                var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == mangaDto.AuthorName);

                if (author == null)
                {
                    // Se o autor não existir, cria um novo
                    author = new Author { Name = mangaDto.AuthorName };
                    _context.Authors.Add(author);
                    await _context.SaveChangesAsync(); // Salva o novo autor no banco de dados
                }

                mangaToUpdate.AuthorId = author.Id;
            }
            else
            {
                // Permite que o campo AuthorId seja nulo se o nome do autor não for fornecido
                mangaToUpdate.AuthorId = null;
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
        public async Task<ActionResult> CreateManga(MangaDTO dto)
        {
			Author? author = null;  // Inicializando author como null

			if (!string.IsNullOrEmpty(dto.AuthorName))
			{
				author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == dto.AuthorName);
				if (author == null)
				{
					author = new Author { Name = dto.AuthorName };
					_context.Authors.Add(author);
					await _context.SaveChangesAsync();
				}
			}

			Manga manga = new Manga
			{
				Title = dto.Title,
				Status = dto.Status,
				Description = dto.Description,
				Released = dto.Released,
				ThumbnailURL = dto.ThumbnailURL,
				Author = author
			};

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
