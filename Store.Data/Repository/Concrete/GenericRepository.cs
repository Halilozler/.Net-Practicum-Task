using Microsoft.EntityFrameworkCore;
using Movie_Store.Base;
using Movie_Store.DbOperations;
using Movie_Store.Repository.Abstract;

namespace Movie_Store.Repository.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
	{
        private readonly MovieDbContext _context;
        private DbSet<TEntity> _entities;

        public GenericRepository(MovieDbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entities.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int entityId)
        {
            return await _context.FindAsync<TEntity>(entityId);
        }

        public async Task InsertAsync(TEntity entity)
        {
            //Insert
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void RemoveAsync(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            //Update
            _entities.Update(entity);
        }
    }
}

