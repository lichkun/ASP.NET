using Soccer.DAL.Entities;

namespace Soccer.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Team> Teams { get; }
        IRepository<Player> Players { get; }
        Task Save();
    }
}
