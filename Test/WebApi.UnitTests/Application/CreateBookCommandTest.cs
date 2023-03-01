using System;
using AutoMapper;
using FluentAssertions;
using Net_Core_Patika;
using Net_Core_Patika.BookOperations.CreateBook;
using Net_Core_Patika.DbOperations;
using WebApi.UnitTests.TestsSetup;
using static Net_Core_Patika.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateCommand
{
	//library özelliği ile direk olarak oluşturduğumuz config dosyasını burada belirtiyoruz.
	public class CreateBookCommandTest : IClassFixture<CommonTestFixture>
	{
		//birim testlerde bir db ye yazarmış gibi yapmamız lazım aslında
		private readonly BookStoreDbContext _context;
		private readonly IMapper _mapper;

		public CreateBookCommandTest(CommonTestFixture testFixture)
		{
            //direk olarak CommonTestFixture üzerinden erişebiliyoruz.
            _context = testFixture.Context;
			_mapper = testFixture.Mapper;
		}

		//isimlendirmeler çok önemli.
		//var olan kitap ismi verdiğimizde InvalidOperationException döndürmeli
		[Fact]	//Test olduğunu belirtik.
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
		{
			//arrange	(Hazırlık) ********************
			//burada veri ekleyecez.
			var book = new Book
			{
				Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
				GenreId = 1, //Personel
				PageCount = 200,
				PublishDate = new DateTime(2001, 06, 12),
				AuthorId = 1
			};
			_context.Books.Add(book);
			_context.SaveChanges();

			CreateBookCommand command = new CreateBookCommand(_context, _mapper);
			command.Model = new CreateBookModel() { Title = book.Title };   //Aynı title gönderdik zaten biz bunu db ye eklemiştik kızması için.

			//act &	assert	(çalıştırma & Doğrulama) ********************
			FluentActions
				.Invoking(() => command.Handle())
				.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut");

            //Invoking => hangi metodu çalıştırıcaz otomatik çalışıtırır.
            //Should => Metod sonucu ne olmalı.
        }

        //HappyTest Yazalım
        [Fact]	
        public void WhenValidInputsAreGiven_Book_ShouldNBeCreated()
        {
            //arrange ********************
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel() { Title = "sadasd", PageCount = 100, PublishDate = DateTime.Now.Date.AddYears(-2) };


			//act  ********************
			FluentActions
				.Invoking(() => command.Handle()).Invoke();

			//asserts ********************
			var book = _context.Books.SingleOrDefault(x => x.Title == command.Model.Title);

			book.Should().NotBeNull();  //null olmamamlı
            book.PageCount.Should().Be(command.Model.PageCount);
			book.PublishDate.Should().Be(command.Model.PublishDate);
            book.GenreId.Should().Be(command.Model.GenreId);
        }
    }
}

