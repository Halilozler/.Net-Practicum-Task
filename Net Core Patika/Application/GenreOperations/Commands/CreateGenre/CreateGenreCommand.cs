using System;
using Net_Core_Patika.DbOperations;

namespace Net_Core_Patika.Application.GenreOperations.Commands
{
	public class CreateGenreCommand
	{
		public CreateGenreModel Model { get; set; }

		private readonly IBookStoreDbContext _context;

        public CreateGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

		public void Handle()
		{
			var genre = _context.Genres.SingleOrDefault(x => x.Name == Model.Name);
			if(genre is not null)
			{
                throw new InvalidOperationException("Kitap Türü Zaten Mevcut");
            }

			//mapleme yapmadık burada
			genre = new Entity.Genre { Name = Model.Name };
			_context.Genres.Add(genre);
			_context.SaveChanges();
		}
	}

	public class CreateGenreModel
	{
		public string Name { get; set; }
	}
}

