using System;
using System.ComponentModel.DataAnnotations.Schema;
using Movie_Store.Base;

namespace Movie_Store.Entity
{
	public class Player : IEntity
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }

		public int StarringMoviesId { get; set; }
		//public List<int> StarringMoviesId { get; set; }
		//public List<Movie> StarringMovies { get; set; }
	}
}

