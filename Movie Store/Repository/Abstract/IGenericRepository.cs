using System;
using Movie_Store.Base;

namespace Movie_Store.Repository.Abstract
{
	public interface IGenericRepository<TEntity> where TEntity : class,IEntity
	{
        Task<TEntity> GetByIdAsync(int entityId);
        Task InsertAsync(TEntity entity);
        void RemoveAsync(TEntity entity);
        void Update(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}

