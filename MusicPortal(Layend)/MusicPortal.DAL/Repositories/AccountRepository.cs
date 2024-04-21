using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Interfaces;
using MusicPortal.Models;

namespace MusicPortal.Repository
{
    public class AccountRepository : IRepository
    {
        private readonly MusicPortalContext _context;

        public AccountRepository(MusicPortalContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<bool> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> VerifyPasswordAsync(User user, string password)
        {
            return await _context.Users.AnyAsync(u => u.Id == user.Id && u.Password == password);
        }
    }
}
