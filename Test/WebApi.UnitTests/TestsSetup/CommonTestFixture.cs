using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Net_Core_Patika.DbOperations;
using Net_Core_Patika.Mapping;

namespace WebApi.UnitTests.TestsSetup
{
	public class CommonTestFixture
	{
		public BookStoreDbContext Context { get; set; }
		public IMapper Mapper { get; set; }

		public CommonTestFixture()
		{
			//yeni bir db yaratık.
			var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDB").Options;
			Context = new BookStoreDbContext(options);
			Context.Database.EnsureCreated();

			//Dataları direk olarak oluşturmak için.
			Context.AddBooks();
			Context.AddGenres();

			//Mapperı Configre edicez uygulama içindeki configleri buraya tanımlamamız lazım.
			Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();

			//bütün configleri verdik test yazmaya artık hazırız.
		}
	}
}

