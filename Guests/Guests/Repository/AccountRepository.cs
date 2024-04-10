using Guests.Models;
using Microsoft.EntityFrameworkCore;
using StoringPassword.Models;
using System.Security.Cryptography;
using System.Text;

namespace AccountMVC.Repository
{
    public class AccountRepository : IRepository
    {
        private readonly MessengerContext _context;

        public AccountRepository(MessengerContext context)
        {
            _context = context;
        }
        public async Task<User> GetUser(LoginModel login)
        {
            return await _context.Users.Where(x=> x.Login == login.Login).SingleAsync();
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<string> Decryption(User curUser, LoginModel logon)
        {
            string? salt = curUser.Salt;

            byte[] password = Encoding.Unicode.GetBytes(salt + logon.Password);

            byte[] byteHash = SHA256.HashData(password);

            StringBuilder hash = new StringBuilder(byteHash.Length);
            for (int i = 0; i < byteHash.Length; i++)
                hash.Append(string.Format("{0:X2}", byteHash[i]));

            return hash.ToString();
        }
        public async Task<MessagesViewModel> GetMessagesList()
        {
            var messagesViewModel = new MessagesViewModel
            {
                AllMessages = _context.Messages.Include(m => m.User).ToList(),
                NewMessage = new Messages()
            };
            return messagesViewModel;
        }

        public async Task Register(RegisterModel reg)
        {
            User user = new User();
            user.FirstName = reg.FirstName;
            user.LastName = reg.LastName;
            user.Login = reg.Login;

            byte[] saltbuf = new byte[16];

            RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(saltbuf);

            StringBuilder sb = new StringBuilder(16);
            for (int i = 0; i < 16; i++)
                sb.Append(string.Format("{0:X2}", saltbuf[i]));
            string salt = sb.ToString();

            byte[] password = Encoding.Unicode.GetBytes(salt + reg.Password);

            byte[] byteHash = SHA256.HashData(password);

            StringBuilder hash = new StringBuilder(byteHash.Length);
            for (int i = 0; i < byteHash.Length; i++)
                hash.Append(string.Format("{0:X2}", byteHash[i]));

            user.Password = hash.ToString();
            user.Salt = salt;

            await _context.Users.AddAsync(user);
        }

        public async Task<List<User>> GetUsersList()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddMessage(Messages mes)
        {
            await _context.Messages.AddAsync(mes);
        }
    }
}