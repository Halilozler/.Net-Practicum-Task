using System;
using _1.Odev.Context;
using Microsoft.EntityFrameworkCore;

namespace _1.Odev.Service
{
    public class GenericService<Entity> : IGenericService<Entity> where Entity : class
    {
        protected readonly AppDbContext Context;
        private DbSet<Entity> entities;


        public GenericService(AppDbContext dbContext)
        {
            this.Context = dbContext;
            this.entities = Context.Set<Entity>();
        }

        //bizim burada normalde her işlemden sonra SaveChange() ile veritabanında belirtmemiz lazım ama biz
        //bu işlemi şöyle yapıyoruz:
        //UnitOfWork.cs içinde CompleteAsync() metodu içinde gerçekleştiriyoruz.
        public async Task<IEnumerable<Entity>> GetAllAsync()
        {
            //AsNoTracking = kullanmadanda aynı şekilde bunu çalıştırabilirz.
            return await entities.AsNoTracking().ToListAsync();
        }

        public virtual async Task<Entity> GetByIdAsync(int entityId)
        {
            return await entities.FindAsync(entityId);
        }

        public async Task InsertAsync(Entity entity)
        {
            await entities.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public void RemoveAsync(Entity entity)
        {
            var column = entity.GetType().GetProperty("IsDeleted");
            if (column is not null)
            {
                entity.GetType().GetProperty("IsDeleted").SetValue(entity, true);
            }
            else
            {
                entities.Remove(entity);
            }
            Context.SaveChanges();
        }

        public void Update(Entity entity)
        {
            entities.Update(entity);
            Context.SaveChanges();
        }
    }
}

