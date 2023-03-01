using System;
using AutoMapper;
using FluentAssertions;
using Net_Core_Patika.BookOperations.DeleteBook;
using Net_Core_Patika.BookOperations.GetBookById;
using Net_Core_Patika.DbOperations;
using WebApi.UnitTests.TestsSetup;

namespace WebApi.UnitTests.Application
{
	public class GetBookIdCommandTest : IClassFixture<CommonTestFixture>
    {
        //birim testlerde bir db ye yazarmış gibi yapmamız lazım aslında
        private readonly BookStoreDbContext _context;

        public GetBookIdCommandTest(CommonTestFixture testFixture)
        {
            //direk olarak CommonTestFixture üzerinden erişebiliyoruz.
            _context = testFixture.Context;
        }

        [Fact]	//Test olduğunu belirtik.
        public void WhenNotExistBookId_InvalidOperationException_ShouldBeReturn()
        {
            GetBookByIdCommand command = new GetBookByIdCommand(_context);
            command.Id = "20"; //olmayan Id gönderdik.

            //act &	assert	(çalıştırma & Doğrulama) ********************
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek Kitap Bulunamadı");
        }

        //HappyTest Yazalım
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldNBeCreated()
        {
            //arrange ********************
            GetBookByIdCommand command = new GetBookByIdCommand(_context);
            command.Id = "2"; //olan Id gönderdik.


            //act  ********************
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //asserts ********************
            var book = _context.Books.SingleOrDefault(x => x.Id == Int32.Parse(command.Id));
            book.Should().NotBeNull();  //null olmamamlı
        }
    }
}

