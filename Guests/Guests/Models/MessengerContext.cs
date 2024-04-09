using Microsoft.EntityFrameworkCore;
using StoringPassword.Models;
using System.Collections.Generic;

namespace Guests.Models
{
    public class MessengerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public MessengerContext(DbContextOptions<MessengerContext> options)
           : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
