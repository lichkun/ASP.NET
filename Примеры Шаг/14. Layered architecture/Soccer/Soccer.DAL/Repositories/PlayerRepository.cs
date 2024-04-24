using Microsoft.EntityFrameworkCore;
using Soccer.DAL.Entities;
using Soccer.DAL.Interfaces;
using Soccer.DAL.EF;


namespace Soccer.DAL.Repositories
{
    public class PlayerRepository : IRepository<Player>
    {
        private SoccerContext db;

        public PlayerRepository(SoccerContext context)
        {
            this.db = context;
        }

        public async Task<IEnumerable<Player>> GetAll()
        {
            return await db.Players.Include(o => o.Team).ToListAsync();
        }

        public async Task<Player> Get(int id)
        {
            var players = await db.Players.Include(o => o.Team).Where(a => a.Id == id).ToListAsync();
            Player? player = players?.FirstOrDefault();
            return player!;
        }

        public async Task<Player> Get(string name)
        {         
            var players = await db.Players.Where(a => a.Name == name).ToListAsync();
            Player? player = players?.FirstOrDefault();
            return player!;
        }

        public async Task Create(Player player)
        {
            await db.Players.AddAsync(player);
        }

        public void Update(Player player)
        {
            db.Entry(player).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Player? player = await db.Players.FindAsync(id);
            if (player != null)
                db.Players.Remove(player);
        }

    }
}
