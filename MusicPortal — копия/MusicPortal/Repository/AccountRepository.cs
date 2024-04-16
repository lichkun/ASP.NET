using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using MusicPortal.Services;

namespace MusicPortal.Repository
{
    public class AccountRepository : IRepository
    {
        private readonly MusicPortalContext _context;
        private readonly IEncryption _sender;

        public AccountRepository(MusicPortalContext context, IEncryption sender)
        {
            _context = context;
            _sender = sender;
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        }

        public async Task<bool> AddUserAsync(RegisterModel reg)
        {
            string salt = _sender.Encryptyion(reg);
            User user = new User
            {
                Login = reg.Login!,
                Status = (reg.Login == "admin") ? 2 : 0,
                Password = _sender.Encryptyion(reg, salt)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _sender.SaveSaltToDatabase(reg, salt);

            return true;
        }

        public async Task<bool> VerifyPasswordAsync(User user, string password)
        {
            string hash = _sender.Decryption(user, password);
            return await _context.Users.AnyAsync(u => u.Id == user.Id && u.Password == hash);
        }
    }
}
