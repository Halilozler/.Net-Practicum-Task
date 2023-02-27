using System;
using Microsoft.EntityFrameworkCore;
using Net_Core_Patika.DbOperations;

namespace Net_Core_Patika.BookOperations.GetBookById
{
	public class GetBookByIdCommand
	{
		private readonly BookStoreDbContext context;
		public GetBookByIdCommand(BookStoreDbContext context)
		{
			this.context = context;
		}

		public Book Handle(string Id)
		{
            var book = context.Books.Where(x => x.Id == Convert.ToInt32(Id)).SingleOrDefault();
			if(book is null)
			{
				throw new InvalidOperationException("Kitap Bulunamadı");
            }
            return book;
        }
	}
}

