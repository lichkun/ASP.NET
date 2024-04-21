using MusicPortal.Models;

namespace MusicPortal.DAL.Interfaces
{
    public interface IRepository
    {
        Task<User> GetUserByLoginAsync(string login);
        Task<bool> AddUserAsync(User reg);
        Task<bool> VerifyPasswordAsync(User user, string password);

    }
}
