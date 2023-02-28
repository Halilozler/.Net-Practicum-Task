using System;
using System.ComponentModel.DataAnnotations.Schema;
using Net_Core_Patika.Entity;

namespace Net_Core_Patika
{
	public class Book
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Title { get; set; }

		public int GenreId { get; set; }

        //veritabanı ile ilişki kurdurmak istersek GenreId otomatik bağlanır
        public Genre Genre { get; set; }

		public int PageCount { get; set; }

		public DateTime PublishDate { get; set; }
	}
}

