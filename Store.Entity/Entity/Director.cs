using System;
using System.ComponentModel.DataAnnotations.Schema;
using Movie_Store.Base;

namespace Movie_Store.Entity
{
	public class Director : IEntity
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public int DirectedMoviesId { get; set; }
        //public List<int> DirectedMoviesId { get; set; }
        //public List<Movie> DirectedMovies { get; set; }
    }
}

