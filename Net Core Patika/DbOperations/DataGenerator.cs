using System;
using Microsoft.EntityFrameworkCore;

namespace Net_Core_Patika.DbOperations
{
	//Biz burada ilk başta serverimizi açarken burada belirtiğimiz nesnelerin db ye eklenmesini istiyoruz.
	public class DataGenerator
	{
        //IServiceProvider = ile program.cs kısmında biz bu metodu çağıracağız sonra ise uygulama her ilk ayağa kalkışta bu metodumuzu çalıştırmaya yarayacak.
        public static void Initialize(IServiceProvider serviceProvider)
		{
			using(var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
			{
				//her ihtimale karşı belki db de verimiz var kontrol ediyoruz.
				if (context.Books.Any())
				{
					return;
				}

				//addrange ile multiple veri ekleyebiliyoruz.
                context.Books.AddRange(
					new Book{
						//Id = 1,
						Title = "Lean Startup",GenreId = 1, //Personel
                        PageCount = 200,PublishDate = new DateTime(2001, 06, 12) },
					new Book{
						//Id = 2,
						Title = "Herland",GenreId = 2, //Science
                        PageCount = 250,PublishDate = new DateTime(2010, 06, 12)},
					new Book{
						//Id = 3,
						Title = "Dune",GenreId = 2, //Science
                        PageCount = 550,PublishDate = new DateTime(2005, 06, 12)}
				);

				//şimdi saveChange() ile kaydet biz diyoruz.
				context.SaveChanges();

                //bunuda yaptıktan sonra biz bu DataGenerator classını tetiklememiz lazım onuda program.cs kısmından yapıyoruz.
            }
        }
	}
}

