namespace MusicPortal.DAL.Interfaces
{
    public interface IRepositoryEntity<TEntity> 
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<bool> AddAsync(TEntity entity);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(int id, TEntity entity);
        Task<bool> ChangeStatusAsync(int userId, int status);
    }
}
