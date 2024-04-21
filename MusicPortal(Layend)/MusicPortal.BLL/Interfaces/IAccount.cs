using MusicPortal.BLL.DTO;
using MusicPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Interfaces
{
    public  interface IAccount
    {
        Task<UserDTO> GetUserByLoginAsync(string login);
        Task<bool> AddUserAsync(UserDTO reg);
        Task<bool> VerifyPasswordAsync(UserDTO user, string password);
    }
}
