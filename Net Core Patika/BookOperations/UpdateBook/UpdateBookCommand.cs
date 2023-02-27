using System;
using Microsoft.EntityFrameworkCore;
using Net_Core_Patika.DbOperations;

namespace Net_Core_Patika.BookOperations.UpdateBook
{
	public class UpdateBookCommand
	{
        private readonly BookStoreDbContext context;
        public int Id { get; set; }
        public UpdateBookModel bookModel { get; set; }
        public UpdateBookCommand(BookStoreDbContext context)
        {
            this.context = context;
        }

        public void Handle()
        {
            var book = context.Books.SingleOrDefault(x => x.Id == Id);

            if (book is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }
            book.GenreId = bookModel.GenreId != default ? bookModel.GenreId : book.GenreId;
            book.PageCount = bookModel.PageCount != default ? bookModel.PageCount : book.PageCount;
            book.PublishDate = bookModel.PublishDate != default ? bookModel.PublishDate : book.PublishDate;
            book.Title = bookModel.Title != default ? bookModel.Title : book.Title;

            context.SaveChanges();
        }

        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}

