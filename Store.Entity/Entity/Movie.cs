using System;
using System.ComponentModel.DataAnnotations.Schema;
using Movie_Store.Base;

namespace Movie_Store.Entity
{
	public class Movie : IEntity
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		public string Name { get; set; }

		public int GenreId { get; set; }
		//public Genre Genre { get; set; }

		public int DirectorId { get; set; }
		//public Director Director { get; set; }

		public int PlayersId { get; set; }
		//public List<int> PlayersId { get; set; }
		//public List<Player> Players { get; set; }

		public decimal Price { get; set; }
	}
}

