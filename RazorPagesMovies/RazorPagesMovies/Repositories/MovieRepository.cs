using Microsoft.EntityFrameworkCore;
using MyTopMovies.Models;

namespace RazorPagesMovies.Repositories
{
    public class MovieRepository : IRepository<Movie>
    {
        private readonly MyTopMovieContext _context;

        public MovieRepository(MyTopMovieContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetAll()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public async Task<Movie> Add(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie> Update(int id, Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return movie;
        }

        public async Task<bool> Delete(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return false;
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
