using System;
using Net_Core_Patika.DbOperations;

namespace Net_Core_Patika.Application.GenreOperations.Commands.DeleteGenre
{
	public class DeleteGenreCommand
	{
		public int GenreId { get; set; }

		private readonly IBookStoreDbContext _context;

        public DeleteGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

		public void Handle()
		{
			var genre = _context.Genres.SingleOrDefault(x => x.Id == GenreId);

			if(genre is null)
			{
                throw new InvalidOperationException("Kitap Türü Bulunamadı");
            }

			_context.Genres.Remove(genre);
			_context.SaveChanges();
		}
	}
}

