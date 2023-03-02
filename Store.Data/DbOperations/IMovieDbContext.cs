using System;
using Microsoft.EntityFrameworkCore;
using Movie_Store.Entity;

namespace Movie_Store.DbOperations
{
	public interface IMovieDbContext
	{
        DbSet<Customer> Customer { get; set; }
        DbSet<Director> Director { get; set; }
        DbSet<Genre> Genre { get; set; }
        DbSet<Movie> Movie { get; set; }
        DbSet<Player> Player { get; set; }
    }
}

