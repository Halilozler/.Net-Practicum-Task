using System;
using Movie_Store.DbOperations;
using Movie_Store.Entity;
using Movie_Store.Repository.Abstract;
using Movie_Store.Repository.Concrete;

namespace Store.Data.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
        private readonly MovieDbContext dbContext;
        public bool disposed;

        public IGenericRepository<Genre> GenreRepository { get; private set; }

        public UnitOfWork(MovieDbContext dbContext)
        {
            this.dbContext = dbContext;

            GenreRepository = new GenericRepository<Genre>(dbContext);
        }

        public async Task CompleteAsync()
        {
            using (var dbContextTransaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    //yani şöyle demek istiyoruz aslında mesela ard arda 3 işlem yapıldı hepsi için
                    //ayrı ayrı SaveChanges uygulanacağına 3 işlem sonunda saveChange uyguluyoruz ve memory i siliyoruz.
                    dbContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    // logging                    
                    dbContextTransaction.Rollback();
                }
            }
        }

        protected virtual void Clean(bool disposing)
        {
            //burada clean metodu biz üst üste isteklerde hep yeni bir obje tanımlanacağından memory şişmeye başlar buda bunu önler.
            //Dispose ederek.
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Clean(true);
            GC.SuppressFinalize(this);
        }
    }
}

