using System;
using AutoMapper;
using Castle.Components.DictionaryAdapter;
using FluentAssertions;
using Microsoft.AspNetCore.Routing;
using Net_Core_Patika;
using Net_Core_Patika.BookOperations.CreateBook;
using Net_Core_Patika.DbOperations;
using Net_Core_Patika.Entity;
using WebApi.UnitTests.TestsSetup;
using Xunit;
using static Net_Core_Patika.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateCommand
{
	public class CreateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
	{
		//Fact => Bir kez çalıştırmak istersek veririz.
		//Theory => Birden fazla veri verip n kez çalışmasını istiyorsak veririz.
		[Theory]
		[InlineData("Lord Of The Rings", 0, 0)]
		[InlineData("Lord Of The Rings", 0, 1)]
		[InlineData("Lord Of The Rings", 100, 0)]
		[InlineData("", 0, 0)]
		[InlineData("", 100, 1)]
		[InlineData("", 0, 1)]
		[InlineData("Lor", 100, 1)]
		[InlineData("Lord", 100, 0)]
		[InlineData("Lord", 0, 1)]
		[InlineData(" ", 100, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
		{
			//burada context ve mappera gerek yok.
			//arrange
			CreateBookCommand command = new CreateBookCommand(null, null);//çalıştırmıcaz null gönderebiliriz.
			command.Model = new CreateBookModel()
			{
				Title = title,
				PageCount = pageCount,
				PublishDate = DateTime.Now.Date.AddYears(-1),
				GenreId = genreId

                //PublishDate patlayabilir o yüzden biz verdik.
            };

			//act
			CreateBookCommandValidator validations = new CreateBookCommandValidator();
			var result = validations.Validate(command);

			//assert
			result.Errors.Count.Should().BeGreaterThan(0);
			//error sayımızı 0 dan büyük olmalı dedik.
		}

		//datetime sadece kontrol edicez burada önemli nokta Datetime kontrol edeceğimiz için diğer nesneler testen geçmeli sadece datetime sınamalaıyız.
		[Fact]
		public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
		{
			CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };

            CreateBookCommandValidator validations = new CreateBookCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        //Her koşulun doğru çalıştığı testide yazmalıyız buna HappyTest denir.
        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1
            };

            CreateBookCommandValidator validations = new CreateBookCommandValidator();
            var result = validations.Validate(command);

            result.Errors.Count.Should().Be((int)0);
        }
    }
}

