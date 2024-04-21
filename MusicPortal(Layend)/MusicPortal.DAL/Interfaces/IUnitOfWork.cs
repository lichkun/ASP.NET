using MusicPortal.Models;
using MusicPortal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryEntity<Artist> Artists { get; }
        IRepositoryEntity<Genre> Genres { get; }
        IRepositoryEntity<Song> Songs { get; }
        IRepositoryEntity<User> Users { get; }
        IRepository Accounts { get; }
        Task Save();
    }
}
