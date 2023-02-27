using System;
using Microsoft.EntityFrameworkCore;

namespace Net_Core_Patika.DbOperations
{
	public class BookStoreDbContext : DbContext
	{
		public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }

		//db ye bu tabloları ekledik.
		public DbSet<Book> Books { get; set; }
	}
}

