using Soccer.BLL.DTO;

namespace Soccer.BLL.Interfaces
{
    public interface IPlayerService 
    {
        Task CreatePlayer(PlayerDTO teamDto);
        Task UpdatePlayer(PlayerDTO teamDto);
        Task DeletePlayer(int id);
        Task<PlayerDTO> GetPlayer(int id);
        Task<IEnumerable<PlayerDTO>> GetPlayers();
    }
}

