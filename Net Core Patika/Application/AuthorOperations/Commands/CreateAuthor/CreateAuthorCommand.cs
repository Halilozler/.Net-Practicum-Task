using System;
using Net_Core_Patika.DbOperations;

namespace Net_Core_Patika.Application.AuthorOperations.Commands
{
	public class CreateAuthorCommand
	{
		public CreateAuthorModel Model { get; set; }

		private readonly IBookStoreDbContext _context;

        public CreateAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

		public void Handle()
		{
			var author = _context.Authors.SingleOrDefault(x => x.Name == Model.Name && x.Surname == Model.Surname);
			if(author is not null)
			{
                throw new InvalidOperationException("Yazar Zaten Mevcut");
            }

            //mapleme yapmadık burada
            author = new Entity.Author { Name = Model.Name, Surname = Model.Surname, Birthday = Model.Birthday };
			_context.Authors.Add(author);
			_context.SaveChanges();
		}
	}

	public class CreateAuthorModel
	{
        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime Birthday { get; set; }
    }
}

