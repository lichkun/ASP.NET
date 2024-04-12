using MusicPortal.Models;

namespace MusicPortal.Repository
{
    public interface IRepository
    {
        Task<MessagesViewModel> GetMessagesList();
        Task<List<User>> GetUsersList();
        Task<User> GetUser(LoginModel login);
        Task<string> Decryption(User curUser, LoginModel logon);
        Task Register(RegisterModel register);
        Task Save();
        Task AddMessage(Messages mes);
    }
}
