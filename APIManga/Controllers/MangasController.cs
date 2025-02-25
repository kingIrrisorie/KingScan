using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIManga.Context;
using APIManga.Model;

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
        public async Task<ActionResult<IEnumerable<Manga>>> GetAllManga()
        {
            var mangas = await _context.Mangas.ToListAsync();

            return mangas;
		}
	}
}
