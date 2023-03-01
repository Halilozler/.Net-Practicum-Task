using System;
using Microsoft.EntityFrameworkCore;
using Net_Core_Patika.DbOperations;
using static Net_Core_Patika.BookOperations.UpdateBook.UpdateBookCommand;

namespace Net_Core_Patika.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly IBookStoreDbContext context;
        public int Id { get; set; }
        public DeleteBookCommand(IBookStoreDbContext context)
        {
            this.context = context;
        }

        public void Handle()
        {
            var book = context.Books.SingleOrDefault(x => x.Id == Id);
            if (book is null)
            {
                throw new InvalidOperationException("Silinecek Kitap Bulunamadı");
            }

            context.Books.Remove(book);
            context.SaveChanges();
        }
    }
}

