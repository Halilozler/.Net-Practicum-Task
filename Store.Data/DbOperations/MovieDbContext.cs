using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Movie_Store.Entity;

namespace Movie_Store.DbOperations
{
	public class MovieDbContext : DbContext
    { 
		public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) { }
        /*
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Director> Director { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Player> Player { get; set; }
        */
        public DbSet<Genre> Genre { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Movie>()
                .HasOne(p => p.Director)
                .WithMany(b => b.DirectedMovies);
            modelBuilder.Entity<Movie>()
                .HasOne(p => p.Genre)
                .WithOne();
            modelBuilder.Entity<Movie>()
                .HasMany(p => p.Players)
                .WithMany(b => b.StarringMovies);

            modelBuilder.Entity<Customer>()
                .HasMany(p => p.BoughtMovies)
                .WithOne();
            modelBuilder.Entity<Customer>()
                .HasMany(p => p.FavoriteGenres)
                .WithOne();
            */
            //modelBuilder.Entity<Movie>().Property(x => x.Price).HasColumnType("decimal(18,2)");

            //Migration için
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        //Program.cs kısmına db tanımladık
    }
}

