using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using MusicPortal_WebAPI_.Models;

namespace MusicPortal_WebAPI_.Controllers
{
    [ApiController]
    [Route("api/Artists")]
    public class ArtistController : Controller
    {
        private readonly MusicPortalContext _context;

        public ArtistController(MusicPortalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
        {
            return await _context.Artists.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtist(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var artist = await _context.Artists.SingleOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }
            return new ObjectResult(artist);
        }

        [HttpPut]
        public async Task<ActionResult<Artist>> PutArtist(Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_context.Artists.Any(e => e.Id == artist.Id))
            {
                return NotFound();
            }

            _context.Update(artist);
            await _context.SaveChangesAsync();
            return Ok(artist);
        }

        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist(Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            return Ok(artist);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Artist>> Artists(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var artist = await _context.Artists.SingleOrDefaultAsync(m => m.Id == id);
            if (artist == null)
            {
                return NotFound();
            }

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            return Ok(artist);
        }
    }
}
