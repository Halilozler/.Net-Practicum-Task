using System;
using Microsoft.EntityFrameworkCore;
using Net_Core_Patika.DbOperations;

namespace Net_Core_Patika.BookOperations.UpdateBook
{
	public class UpdateBookCommand
	{
        private readonly IBookStoreDbContext context;
        public int Id { get; set; }
        public UpdateBookModel Model { get; set; }
        public UpdateBookCommand(IBookStoreDbContext context)
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
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
            book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate;
            book.Title = Model.Title != default ? Model.Title : book.Title;

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

