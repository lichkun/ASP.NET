using Microsoft.EntityFrameworkCore;
using Soccer.DAL.Entities;
using Soccer.DAL.Interfaces;
using Soccer.DAL.EF;

namespace Soccer.DAL.Repositories
{
    public class TeamRepository : IRepository<Team>
    {
        private SoccerContext db;

        public TeamRepository(SoccerContext context)
        {
            this.db = context;
        }

        public async Task<IEnumerable<Team>> GetAll()
        {
            return await db.Teams.ToListAsync();
        }

        public async Task<Team> Get(int id)
        {
            Team? team = await db.Teams.FindAsync(id);
            return team!;
        }

        public async Task<Team> Get(string name)
        {
            var teams = await db.Teams.Where(a => a.Name == name).ToListAsync(); 
            Team? team = teams?.FirstOrDefault();
            return team!;
        }

        public async Task Create(Team team)
        {
            await db.Teams.AddAsync(team);
        }

        public void Update(Team team)
        {
            db.Entry(team).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Team? team = await db.Teams.FindAsync(id);
            if (team != null)
                db.Teams.Remove(team);
        }
    }
}
