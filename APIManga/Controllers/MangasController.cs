using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIManga.Context;
using APIManga.Model;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        // GET: api/Mangas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manga>>> GetMangas()
        {
            return await _context.Mangas.ToListAsync();
        }

        // GET: api/Manga/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Manga>> GetManga(int id)
        {
			if (!MangaExists(id))
				return NotFound();

			var manga = await _context.Mangas.FindAsync(id);

            return manga;
        }

        // PUT: api/Manga/5
        [HttpPut("{id}")]
		public async Task<IActionResult> PutManga(int id, Manga manga)
        {
			if (MangaExists(id))
				return NotFound();

			var mangaToUpdate = await _context.Mangas.FindAsync(id);

			mangaToUpdate.Title = manga.Title;
			mangaToUpdate.Status = manga.Status;
			mangaToUpdate.Description = manga.Description;
			mangaToUpdate.ThumbnailURL = manga.ThumbnailURL;
			mangaToUpdate.Released = manga.Released;

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
        public async Task<ActionResult<Manga>> PostManga(Manga manga)
        {
            _context.Mangas.Add(manga);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetManga", new { id = manga.Id }, manga);
        }

        // DELETE: api/Manga/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManga(int id)
        {
			if (MangaExists(id))
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
