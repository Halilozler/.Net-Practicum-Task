using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net_Core_Patika.Entity
{
	public class Genre
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Name { get; set; }

		public bool IsActive { get; set; } = true;
	}
}

