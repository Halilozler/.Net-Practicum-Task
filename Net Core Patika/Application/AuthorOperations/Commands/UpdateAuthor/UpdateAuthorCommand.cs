using System;
using Net_Core_Patika.DbOperations;

namespace Net_Core_Patika.Application.AuthorOperations.Commands
{
	public class UpdateAuthorCommand
	{
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }

        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);

            if (author is null)
            {
                throw new InvalidOperationException("Yazar Bulunamadı");
            }

            author.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? author.Name : Model.Name;
            author.Surname = string.IsNullOrEmpty(Model.Surname.Trim()) ? author.Surname : Model.Surname;
            author.Birthday = author.Birthday != default ? author.Birthday : Model.Birthday;

            _context.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime Birthday { get; set; }
    }
}

