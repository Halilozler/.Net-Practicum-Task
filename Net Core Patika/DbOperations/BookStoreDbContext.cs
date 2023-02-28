using System;
using Microsoft.EntityFrameworkCore;
using Net_Core_Patika.Entity;

namespace Net_Core_Patika.DbOperations
{
	public class BookStoreDbContext : DbContext
	{
		public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }

		//db ye bu tabloları ekledik.
		public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}

