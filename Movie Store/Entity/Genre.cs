using System;
using System.ComponentModel.DataAnnotations.Schema;
using Movie_Store.Base;

namespace Movie_Store.Entity
{
	public class Genre : IEntity
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		public string Name { get; set; }
	}
}

