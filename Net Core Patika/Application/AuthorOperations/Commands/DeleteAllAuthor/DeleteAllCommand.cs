using System;
using Net_Core_Patika.DbOperations;

namespace Net_Core_Patika.Application.AuthorOperations.Commands
{
	public class DeleteAllAuthor
	{
        private readonly IBookStoreDbContext _context;

        public DeleteAllAuthor(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            _context.Authors.RemoveRange(_context.Authors);
            _context.SaveChanges();
        }
    }
}

