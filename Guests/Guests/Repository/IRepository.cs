
using Guests.Models;
using Microsoft.EntityFrameworkCore;
using StoringPassword.Models;

namespace AccountMVC.Repository
{
    public interface IRepository
    {
        Task<MessagesViewModel> GetMessagesList();
        Task<List<User>> GetUsersList();
        Task<User> GetUser(LoginModel login);
        Task<string> Decryption(User curUser, LoginModel logon);
        Task Login(LoginModel login);
        Task Register(RegisterModel register);
        Task Save();
        Task AddMessage(Messages mes);
       
    }
}
