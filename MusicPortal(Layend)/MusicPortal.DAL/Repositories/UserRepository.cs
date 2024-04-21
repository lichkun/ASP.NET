using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Interfaces;
using MusicPortal.Models;

namespace MusicPortal.Repository
{
    public class UserRepository : IRepositoryEntity<User>
    {
        private readonly MusicPortalContext _context;

        public UserRepository(MusicPortalContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, User user)
        {
            if (id != user.Id)
                return false;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                    return false;
                else
                    throw;
            }

            return true;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public async Task<bool> ChangeStatusAsync(int userId, int status)
        {
            var user = await GetByIdAsync(userId);
            if (user == null)
                return false;

            user.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
