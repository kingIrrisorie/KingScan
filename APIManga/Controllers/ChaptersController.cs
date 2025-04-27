using APIManga.Services;
using Microsoft.AspNetCore.Mvc;
using APIManga.DTOs;

namespace APIManga.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ChaptersController : ControllerBase
	{
		private readonly MangaService _mangaService;

		public ChaptersController (MangaService mangaService)
		{
			_mangaService = mangaService;
		}

		[HttpGet("{mangaId}")]
		public async Task<ActionResult<IEnumerable<ChapterDTO>>> GetChaptersByMangaId(int mangaId)
		{
			var chapters = await _mangaService.GetChaptersByMangaIdAsync(mangaId);
			return Ok(chapters);
		}

		// POST: api/Chapters?mangaId=1
		[HttpPost]
		public async Task<ActionResult<ChapterDTO>> PostChapter(int mangaId, [FromBody] ChapterDTO chapterDTO)
		{
			try
			{
				var createdChapter = await _mangaService.CreateChapterAsync(mangaId, chapterDTO);
				return CreatedAtAction(nameof(GetChaptersByMangaId), new { mangaId = createdChapter.MangaId }, createdChapter);
			}
			catch (ArgumentException ex)
			{
				return BadRequest(new { error = ex.Message });
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { error = "Erro ao criar o capítulo", details = ex.Message });
			}
		}

		//[HttpGet("{mangaId}/chapters/{chapterId}")]
		//public async Task<ActionResult<ChapterDTO>> GetChapterById(int mangaId, int chapterId)
		//{
		//	var chapter = await _mangaService.GetChapterByIdAsync(mangaId, chapterId);
		//	if (chapter == null)
		//		return NotFound(new { error = "Capítulo não encontrado." });

		//	return Ok(chapter);
		//}
	}
}
