using System;
using System.ComponentModel.DataAnnotations.Schema;
using Movie_Store.Base;

namespace Movie_Store.Entity
{
	public class Customer : IEntity
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }

		public int BoughtMoviesId { get; set; }
		//public List<int> BoughtMoviesId { get; set; }
		//public List<Movie> BoughtMovies { get; set; }

		public int FavoriteGenresId { get; set; }
		//public List<int> FavoriteGenresId { get; set; }
		//public List<Genre> FavoriteGenres { get; set; }
	}
}

