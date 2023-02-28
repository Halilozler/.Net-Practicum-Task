using System;
using FluentValidation;
using Net_Core_Patika.BookOperations.DeleteBook;
using Net_Core_Patika.DbOperations;

namespace Net_Core_Patika.Application.AuthorOperations.Commands
{
	public class DeleteAuthor
	{
        public int Id { get; set; }

        private readonly BookStoreDbContext _context;

        public DeleteAuthor(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            //önce kitabı silinecek sonra yazar silinecek.
            var author = _context.Authors.SingleOrDefault(x => x.Id == Id);

            if(author is null)
            {
                throw new InvalidOperationException("Yazar Mevcut Değil");
            }

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.Id = Id;
            DeleteBookCommandValidator validations = new DeleteBookCommandValidator();
            validations.ValidateAndThrow(command);

            command.Handle();

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}

