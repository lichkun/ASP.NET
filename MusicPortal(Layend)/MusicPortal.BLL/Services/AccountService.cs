using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Infastructure;
using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Interfaces;
using MusicPortal.Models;
using MusicPortal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using IAccount = MusicPortal.BLL.Interfaces.IAccount;

namespace MusicPortal.BLL.Services
{
    public class AccountService : IAccount
    {
        IUnitOfWork Database { get; set; }
        IEncryption Encryption { get; set; }
        public AccountService(IUnitOfWork uow, IEncryption Enc)
        {
            Database = uow;
            Encryption = Enc;
        }
        public async Task<UserDTO> GetUserByLoginAsync(string login)
        {
            var user = await Database.Accounts.GetUserByLoginAsync(login);
            if (user == null) return null;
            return new UserDTO
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Status = user.Status
            };
        }

        public async Task<bool> AddUserAsync(UserDTO reg)
        {
            string salt = Encryption.Encryptyion(reg);
            User user = new User
            {
                Login = reg.Login!,
                Status = (reg.Login == "admin") ? 2 : 0,
                Password = Encryption.Encryptyion(reg, salt)
            };

            await Database.Users.AddAsync(user);
            await Database.Save();

            Encryption.SaveSaltToDatabase(reg, salt);

            await Database.Save();
            return true;
        }

        public async Task<bool> VerifyPasswordAsync(UserDTO user, string password)
        {
            string hash = Encryption.Decryption(user, password);
            var usr = await Database.Users.GetByIdAsync(user.Id);
            return usr.Password == hash;
        }
    }
}
