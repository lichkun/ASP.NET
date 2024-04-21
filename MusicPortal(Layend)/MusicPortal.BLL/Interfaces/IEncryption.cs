using MusicPortal.BLL.DTO;
using MusicPortal.Models;

namespace MusicPortal.Services
{
    public interface IEncryption
    {
        string Encryptyion(UserDTO reg, string salt="");
        string Decryption(UserDTO curUser, string pass);
        void SaveSaltToDatabase(UserDTO reg, string salt);
    }
}
