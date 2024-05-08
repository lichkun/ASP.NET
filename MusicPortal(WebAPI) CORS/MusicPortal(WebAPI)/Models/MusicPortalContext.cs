using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MusicPortal_WebAPI_.Models
{
    public class MusicPortalContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public MusicPortalContext(DbContextOptions<MusicPortalContext> options)
           : base(options)
        {
           // Database.EnsureDeleted();
            if (Database.EnsureCreated())
            {
                Users.Add(new User { Login = "Иван", Password = "Иваненко"});
                Users.Add(new User { Login = "Сергей", Password = "Алексеенко"});
                Users.Add(new User { Login = "Петр", Password = "Петренко"});
                SaveChanges();
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasIndex(u => u.Login)
               .IsUnique();
        }
    }
}
