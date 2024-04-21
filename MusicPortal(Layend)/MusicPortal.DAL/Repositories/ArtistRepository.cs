using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Interfaces;
using MusicPortal.Models;

namespace MusicPortal.Repository
{
    public class ArtistRepository : IRepositoryEntity<Artist>
    {
        private readonly MusicPortalContext _context;

        public ArtistRepository(MusicPortalContext context)
        {
            _context = context;
        }

        public async Task<List<Artist>> GetAllAsync()
        {
            return await _context.Artists.ToListAsync();
        }

        public async Task<Artist> GetByIdAsync(int id)
        {
            return await _context.Artists.FindAsync(id);
        }

        public async Task<bool> AddAsync(Artist artist)
        {
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
                return false;

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, Artist artist)
        {
            var existingArtist = await _context.Artists.FindAsync(id);

            if (existingArtist == null)
                return false;

            existingArtist.Name = artist.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
                    return false;
                else
                    throw;
            }

            return true;
        }

        private bool ArtistExists(int id)
        {
            return _context.Artists.Any(e => e.Id == id);
        }

        public Task<bool> ChangeStatusAsync(int userId, int status)
        {
            throw new NotImplementedException();
        }
    }
}
