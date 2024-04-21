using AutoMapper;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Infastructure;
using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Interfaces;
using MusicPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Services
{
    public class SongService : IService<SongDTO>
    {
        IUnitOfWork Database { get; set; }

        public SongService(IUnitOfWork uow)
        {
            Database = uow;
        }
       
        public async Task<bool> AddAsync(SongDTO entity)
        {
            var artist = new Song
            {
                Id = entity.Id,
                Title = entity.Title,
                FilePath = entity.FilePath,
                ArtistId = entity.ArtistId,
                GenreId = entity.GenreId,
                UserId = entity.UserId,
            };
            await Database.Songs.AddAsync(artist);
            await Database.Save();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await Database.Songs.DeleteAsync(id);
            await Database.Save();
            return true;
        }

        public async Task<List<SongDTO>> GetAllAsync()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Song, SongDTO>()
                .ForMember("User", opt=> opt.MapFrom(c=>c.User.Login))
                .ForMember("Artist", opt=> opt.MapFrom(c=>c.Artist.Name))
                .ForMember("Genre", opt=> opt.MapFrom(c=>c.Genre.Name)));
            var mapper = new Mapper(config);
            return mapper.Map<List<Song>, List<SongDTO>>(await Database.Songs.GetAllAsync());
        }

        public async Task<SongDTO> GetByIdAsync(int id)
        {
            var entity = await Database.Songs.GetByIdAsync(id);
            if (entity == null)
                throw new ValidationException("Wrong artist!", "");
            return new SongDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                FilePath = entity.FilePath,
                ArtistId = entity.ArtistId,
                Artist = entity.Artist.Name,
                GenreId = entity.GenreId,
                Genre = entity.Genre.Name,
                UserId = entity.UserId,
                User = entity.User.Login,
            };
        }

        public async Task<bool> UpdateAsync(int id, SongDTO entity)
        {
            var artist = new Song
            {
                Id = entity.Id,
                Title = entity.Title,
                FilePath = entity.FilePath,
                ArtistId = entity.ArtistId

            };
            await Database.Songs.UpdateAsync(id, artist);
            await Database.Save();
            return true;
        }

        public Task<bool> ChangeStatusAsync(int userId, int status)
        {
            throw new NotImplementedException();
        }
    }
}
