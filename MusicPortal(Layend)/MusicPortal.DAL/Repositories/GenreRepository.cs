using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Interfaces;
using MusicPortal.Models;

namespace MusicPortal.Repository
{
    public class GenreRepository : IRepositoryEntity<Genre>
    {
        private readonly MusicPortalContext _context;

        public GenreRepository(MusicPortalContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> GetAllAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task<bool> AddAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
                return false;

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, Genre genre)
        {
            var existingGenre = await _context.Genres.FindAsync(id);

            if (existingGenre == null)
                return false;

            existingGenre.Name = genre.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
                    return false;
                else
                    throw;
            }

            return true;
        }

        private bool GenreExists(int id)
        {
            return _context.Genres.Any(e => e.Id == id);
        }

        public Task<bool> ChangeStatusAsync(int userId, int status)
        {
            throw new NotImplementedException();
        }
    }
}
