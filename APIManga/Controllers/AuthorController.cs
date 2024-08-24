using APIManga.Context;
using APIManga.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace APIManga.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly KingIrrisorieScanContext _context;

        public AuthorController(KingIrrisorieScanContext context)
        { 
            _context = context;
        }

        // GET: api/Author/{name}
        [HttpGet("{name}")]
        public async Task<ActionResult<Author>> GetAuthor(string name)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == name);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }
    }
}
