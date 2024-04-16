using MusicPortal.Models;

namespace MusicPortal.Repository
{
    public interface IRepository
    {
        Task<User> GetUserByLoginAsync(string login);
        Task<bool> AddUserAsync(RegisterModel reg);
        Task<bool> VerifyPasswordAsync(User user, string password);

    }
}
