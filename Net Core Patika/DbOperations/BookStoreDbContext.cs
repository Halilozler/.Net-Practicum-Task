using System;
using Microsoft.EntityFrameworkCore;
using Net_Core_Patika.Entity;

namespace Net_Core_Patika.DbOperations
{
	public class BookStoreDbContext : DbContext, IBookStoreDbContext
	{
		public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }

		//db ye bu tabloları ekledik.
		public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            //burada farklı işlem yapabiliriz ama biz burada normal SaveChanges() eris dedik.
            //Amacımız BookStore üzerindende erişebilir olsun diye.
            return base.SaveChanges();
        }

        //bunu biz program.cs'e IBookStoreDbContext üzerinden ekledik yani IBookStoreDbContext nee zaman çağrılırsa otomatik BookStoreDbContext nesnesi bizim için yaratıcak
    }
}

