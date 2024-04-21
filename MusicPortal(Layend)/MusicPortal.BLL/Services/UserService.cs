using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Infastructure;
using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Interfaces;
using MusicPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Services
{
    public class UserService : IService<UserDTO>
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<List<User>, List<UserDTO>>(await Database.Users.GetAllAsync());
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            var entity = await Database.Users.GetByIdAsync(id);
            if (entity == null)
                throw new ValidationException("Wrong artist!", "");
            return new UserDTO
            {
                Id = entity.Id,
                Login = entity.Login,
                Password = entity.Password,
                Status = entity.Status
            };
        }

        public async Task<bool> AddAsync(UserDTO entity)
        {
            var artist = new User
            {
                Id = entity.Id,
                Login = entity.Login,
                Password = entity.Password,
                Status = entity.Status

            };
            await Database.Users.AddAsync(artist);
            await Database.Save();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await Database.Users.DeleteAsync(id);
            await Database.Save();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, UserDTO entity)
        {
            var artist = new User
            {
                Id = entity.Id,
                Login = entity.Login,
                Password = entity.Password,
                Status = entity.Status

            };
            await Database.Users.UpdateAsync(id, artist);
            await Database.Save();
            return true;
        }

        public async Task<bool> ChangeStatusAsync(int userId, int status)
        {
            await Database.Users.ChangeStatusAsync(userId, status);
            return true;
        }
    }
}
