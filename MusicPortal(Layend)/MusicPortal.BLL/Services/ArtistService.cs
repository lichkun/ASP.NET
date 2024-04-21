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
    public class ArtistService : IService<ArtistDTO>
    {
        IUnitOfWork Database { get; set; }

        public ArtistService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<List<ArtistDTO>> GetAllAsync() //  Task<IEnumerable<ArtistDTO>>
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Artist, ArtistDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<List<Artist>, List<ArtistDTO>>(await Database.Artists.GetAllAsync());
        }

        public async Task<ArtistDTO> GetByIdAsync(int id)
        {
            var player = await Database.Artists.GetByIdAsync(id);
            if (player == null)
                throw new ValidationException("Wrong artist!", "");
            return new ArtistDTO
            {
                Id = player.Id,
                Name = player.Name,
            };
        }

        public async Task<bool> AddAsync(ArtistDTO entity)
        {
            var artist = new Artist
            {
                Id = entity.Id,
                Name = entity.Name,
            };
            await Database.Artists.AddAsync(artist);
            await Database.Save();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await Database.Artists.DeleteAsync(id); 
            await Database.Save();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, ArtistDTO entity)
        {
            var artist = new Artist
            {
                Id = entity.Id,
                Name = entity.Name,
            };
            await Database.Artists.UpdateAsync(id, artist);
            await Database.Save();
            return true;
        }

        public Task<bool> ChangeStatusAsync(int userId, int status)
        {
            throw new NotImplementedException();
        }
    }
}
