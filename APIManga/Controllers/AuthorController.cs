using APIManga.Context;
using APIManga.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Humanizer;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var authors = await _context.Authors.ToListAsync();
            if (authors == null)
            {
                return NotFound();
            }
            else
            {
                return authors;
            }
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
            return author;
        }

        // POST: api/Author
        [HttpPost("{name}")]
        public async Task<IActionResult> AddAuthor(string name, bool ReturnAuthor = false)
        {
            try
            {
                Author? author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == name);
                if (author == null)
                {
                    author = new Author { Name = name };
                    _context.Authors.Add(author);
                    await _context.SaveChangesAsync();
                }

                if (ReturnAuthor)
                {
                    return Ok(author);
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar o autor: {ex.Message}");
                return StatusCode(500, "Ocorreu um erro ao criar o autor."); 
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            Author? author = _context.Authors.Find(id);
            if (author == null)
                return NotFound();

            try
            {
                _context.Authors.Remove(author);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nao foi possivel deletar author {author.Name}", ex.ToString());
            }

            return NoContent();
        }
    }
}
