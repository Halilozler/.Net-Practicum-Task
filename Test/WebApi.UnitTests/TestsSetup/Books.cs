using System;
using Net_Core_Patika;
using Net_Core_Patika.DbOperations;

namespace WebApi.UnitTests.TestsSetup
{
	public static class Books
	{
		public static void AddBooks(this BookStoreDbContext context)
		{
            context.Books.AddRange(
                    new Book
                    {
                        //Id = 1,
                        Title = "Lean Startup",
                        GenreId = 1, //Personel
                        PageCount = 200,
                        PublishDate = new DateTime(2001, 06, 12),
                        AuthorId = 1
                    },
                    new Book
                    {
                        //Id = 2,
                        Title = "Herland",
                        GenreId = 2, //Science
                        PageCount = 250,
                        PublishDate = new DateTime(2010, 06, 12),
                        AuthorId = 2
                    },
                    new Book
                    {
                        //Id = 3,
                        Title = "Dune",
                        GenreId = 2, //Science
                        PageCount = 550,
                        PublishDate = new DateTime(2005, 06, 12),
                        AuthorId = 3
                    }
                );
        } 
	}
}

