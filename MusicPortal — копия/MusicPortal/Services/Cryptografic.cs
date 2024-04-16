using Microsoft.AspNetCore.Mvc;
using MusicPortal.Models;
using System.Security.Cryptography;
using System.Text;

namespace MusicPortal.Services
{
    public class Cryptografic : IEncryption
    {
        private readonly MusicPortalContext _context;

        public Cryptografic(MusicPortalContext context)
        {
            _context = context;
        }
        public string Decryption(User curUser, string pass)
        {
            var user = _context.Salts.FirstOrDefault(u => u.UserId == curUser.Id);
            string? salt = user?.Salt;

            byte[] password = Encoding.Unicode.GetBytes(salt + pass);

            byte[] byteHash = SHA256.HashData(password);

            StringBuilder hash = new StringBuilder(byteHash.Length);
            for (int i = 0; i < byteHash.Length; i++)
                hash.Append(string.Format("{0:X2}", byteHash[i]));

            return hash.ToString();
        }

        public string Encryptyion(RegisterModel reg, string salt="")
        {
            if (string.IsNullOrEmpty(salt))
            {
                byte[] saltbuf = new byte[16];

                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);

                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));

                salt = sb.ToString();
                return salt;
            }

            byte[] password = Encoding.Unicode.GetBytes(salt + reg.Password);

            byte[] byteHash = SHA256.HashData(password);

            StringBuilder hash = new StringBuilder(byteHash.Length);
            for (int i = 0; i < byteHash.Length; i++)
                hash.Append(string.Format("{0:X2}", byteHash[i]));

            return hash.ToString();

        }

        public void SaveSaltToDatabase(RegisterModel reg, string salt)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == reg.Login);
            if (user != null)
            {
                var userSalt = new Salts { UserId = user.Id, Salt = salt };
                _context.Salts.Add(userSalt);
                _context.SaveChanges();
            }
        }
    } 
}
