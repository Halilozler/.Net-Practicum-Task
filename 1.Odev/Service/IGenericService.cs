using System;
namespace _1.Odev.Service
{
	public interface IGenericService<TEntity> where TEntity : class 
	{
        Task<TEntity> GetByIdAsync(int entityId);
        Task InsertAsync(TEntity entity);
        void RemoveAsync(TEntity entity);
        void Update(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}

