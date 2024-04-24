using Microsoft.EntityFrameworkCore;
using Soccer.DAL.Entities;

namespace Soccer.DAL.EF
{   
    public class SoccerContext : DbContext
    { 
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public SoccerContext(DbContextOptions<SoccerContext> options)
                   : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
