using System;
using Movie_Store.Entity;
using Movie_Store.Repository.Abstract;

namespace Store.Data.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		IGenericRepository<Genre> GenreRepository { get;  }

		Task CompleteAsync();
	}
}

