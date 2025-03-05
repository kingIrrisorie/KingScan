using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIManga.Context;
using APIManga.Model;
using APIManga.DTOs;
using APIManga.Services;

namespace APIManga.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MangasController : ControllerBase
	{
		private readonly MangaService _mangaService;
		private const string ADMIN_API_KEY = "admin-secret-key";

		public MangasController(MangaService mangaService)
		{
			_mangaService = mangaService;
		}

		private bool IsAdmin => Request.Headers["X-API-Key"] == ADMIN_API_KEY;

		/// <summary>
		/// Lista todos os mangás disponíveis.
		/// </summary>
		/// <returns>Lista de mangás em formato DTO</returns>
		/// TEMP: Apenas para testes
		// GET: api/Mangas
		[HttpGet]
		public async Task<ActionResult<IEnumerable<MangaDTO>>> GetAllMangas()
		{
			var mangas = await _mangaService.GetAllMangasAsync();
			return Ok(mangas);
		}

		/// <summary>
		/// Obtém um mangá específico pelo ID.
		/// </summary>
		/// <param name="id">ID do mangá</param>
		/// <returns>Detalhes do mangá ou erro se não encontrado</returns>
		// GET: api/Mangas/5
		[HttpGet("{id}")]
		public async Task<ActionResult<MangaDTO>> GetManga(int id)
		{
			var manga = await _mangaService.GetMangaByIdAsync(id);
			if (manga == null)
				return NotFound( new { error = "Mangá não encontrado." });

			return Ok(manga);
		}

		/// <summary>
		/// Cria um novo mangá (restrito a admins).
		/// </summary>
		/// <param name="mangaDTO">Dados do mangá a ser criado</param>
		/// <returns>Mangá criado ou erro se inválido</returns>
		// POST: api/Mangas
		[HttpPost]
		public async Task<ActionResult<MangaDTO>> PostManga([FromBody] MangaDTO mangaDTO)
		{
			if (!IsAdmin)
				return Unauthorized(new { error = "Acesso restrito." });

			if (mangaDTO == null)
				return BadRequest(new { error = "Os dados do mangá não podem ser nulos" });
			if (!ModelState.IsValid)
				return BadRequest(new { error = "Dados do mangá inválidos", details = ModelState });

			try
			{
				var manga = await _mangaService.CreateMangaAsync(mangaDTO);
				var createdMangaDTO = new MangaDTO
				{
					Title = manga.Title,
					Status = manga.Status.Name,
					Description = manga.Description,
					ReleaseDate = manga.ReleaseDate,
					ThumbnailUrl = manga.ThumbnailUrl,
					AuthorNames = manga.Authors.Select(a => a.Name).ToList(),
					GenreNames = manga.Genres.Select(g => g.Name).ToList()
				};
				return CreatedAtAction(nameof(GetAllMangas), new { id = manga.Id }, createdMangaDTO);
			}
			catch (ArgumentException ex)
			{
				return BadRequest(new { error = ex.Message });
			}
			catch (Exception)
			{
				return StatusCode(500, new { error = "Erro interno ao criar o mangá" });
			}
		}
	}
}
/* sugestao:
[Route("api/[controller]")]
[ApiController]
public class CapitulosController : ControllerBase
{
    private readonly MangaService _mangaService;

    public CapitulosController(MangaService mangaService)
    {
        _mangaService = mangaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CapituloDTO>>> GetRecentChapters()
    {
        var chapters = await _mangaService.GetRecentChaptersAsync(); // Implemente no service
        return Ok(chapters);
    }
}

public class CapituloDTO
{
    public int Id { get; set; }
    public string Capitulo { get; set; } // Ex.: "Capítulo 1 - O Início"
    public int MangaId { get; set; }
    public DateTime ReleaseDate { get; set; }
}
*/

/* outra sugestao:
[Route("api/[controller]")]
[ApiController]
public class ResenhasController : ControllerBase
{
    private readonly MangaService _mangaService;

    public ResenhasController(MangaService mangaService)
    {
        _mangaService = mangaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResenhaDTO>>> GetReviews()
    {
        var reviews = await _mangaService.GetReviewsAsync(); // Implemente no service
        return Ok(reviews);
    }
}

public class ResenhaDTO
{
    public int Id { get; set; }
    public string Resenha { get; set; } // Ex.: "Uma obra incrível!"
    public int MangaId { get; set; }
    public DateTime CreatedAt { get; set; }
}*/