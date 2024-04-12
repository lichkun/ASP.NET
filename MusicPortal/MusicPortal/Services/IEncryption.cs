using MusicPortal.Models;

namespace MusicPortal.Services
{
    public interface IEncryption
    {
        string Encryptyion(RegisterModel reg, string salt="");
        string Decryption(User curUser, string pass);
        void SaveSaltToDatabase(RegisterModel reg, string salt);
    }
}
