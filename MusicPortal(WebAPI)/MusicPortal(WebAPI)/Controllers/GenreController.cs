using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using MusicPortal_WebAPI_.Models;

namespace MusicPortal_WebAPI_.Controllers
{
    [ApiController]
    [Route("api/Genres")]
    public class GenreController : Controller
    {
        private readonly MusicPortalContext _context;

        public GenreController(MusicPortalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return await _context.Genres.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var genre = await _context.Genres.SingleOrDefaultAsync(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }
            return new ObjectResult(genre);
        }

        [HttpPut]
        public async Task<ActionResult<Genre>> PutGenre(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_context.Genres.Any(e => e.Id == genre.Id))
            {
                return NotFound();
            }

            _context.Update(genre);
            await _context.SaveChangesAsync();
            return Ok(genre);
        }

        [HttpPost]
        public async Task<ActionResult<Genre>> PostStudent(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Genre>> Genres(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genre = await _context.Genres.SingleOrDefaultAsync(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return Ok(genre);
        }
    }
}
