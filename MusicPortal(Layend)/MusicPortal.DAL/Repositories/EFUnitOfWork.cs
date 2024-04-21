using MusicPortal.DAL.Interfaces;
using MusicPortal.Models;
using MusicPortal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        MusicPortalContext db;
        AccountRepository accountRepository;
        ArtistRepository artistRepository;
        GenreRepository genreRepository;
        SongRepository songRepository;
        UserRepository userRepository;

        public EFUnitOfWork(MusicPortalContext context)
        {
            db = context;
        }

        public IRepositoryEntity<User> Users
        {
            get
            {
                if (userRepository == null) userRepository = new UserRepository(db);
                return userRepository;
            }
        }


        public IRepositoryEntity<Artist> Artists
        {
            get
            {
                if (artistRepository == null)  artistRepository = new ArtistRepository(db);
                return artistRepository;
            }
        }

        public IRepositoryEntity<Genre> Genres
        {
            get
            {
                if (genreRepository == null)   genreRepository = new GenreRepository(db);
                return genreRepository;
            }
        }

        public IRepositoryEntity<Song> Songs
        {
            get
            {
                if (songRepository == null) songRepository = new SongRepository(db);
                return songRepository;
            }
        }

        public IRepository Accounts
        {
            get
            {
                if(accountRepository==null) accountRepository = new AccountRepository(db);
                return accountRepository;
            }
        }
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

    }
}
