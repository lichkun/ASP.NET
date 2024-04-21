using AutoMapper;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Infastructure;
using MusicPortal.DAL.Interfaces;
using MusicPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Services
{
    public class GenreService : IService<GenreDTO>
    {
        IUnitOfWork Database { get; set; }

        public GenreService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            await Database.Genres.DeleteAsync(id);
            await Database.Save();
            return true;
        }

        public async Task<List<GenreDTO>> GetAllAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Genre, GenreDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<List<Genre>, List<GenreDTO>>(await Database.Genres.GetAllAsync());
        }

        public async Task<GenreDTO> GetByIdAsync(int id)
        {
            var player = await Database.Genres.GetByIdAsync(id);
            if (player == null)
                throw new ValidationException("Wrong artist!", "");
            return new GenreDTO
            {
                Id = player.Id,
                Name = player.Name,
            };
        }

        public async Task<bool> AddAsync(GenreDTO entity)
        {
            var artist = new Genre
            {
                Id = entity.Id,
                Name = entity.Name,
            };
            await Database.Genres.AddAsync(artist);
            await Database.Save();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, GenreDTO entity)
        {
            var genre = new Genre
            {
                Id = entity.Id,
                Name = entity.Name,
            };
            await Database.Genres.UpdateAsync(id, genre);
            await Database.Save();
            return true;
        }

        public Task<bool> ChangeStatusAsync(int userId, int status)
        {
            throw new NotImplementedException();
        }
    }
}
