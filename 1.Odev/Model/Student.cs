using System;
using System.ComponentModel.DataAnnotations;
using _1.Odev.CustomValidations;

namespace _1.Odev.Model
{
	public class Student
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(maximumLength: 15, MinimumLength = 2)]
		public string Name { get; set; }

		[Required]
        [StringLength(maximumLength: 15, MinimumLength = 2)]
        public string Surname { get; set; }

        [Required]
		[MinAgeAttribute]
        public DateTime BirthYear { get; set; }

		public DateTime CreatedTime => DateTime.Now;

		public Student(){}

        public Student(int id, string name, string surname, DateTime birthYear)
		{
			this.Id = id;
			this.Name = name;
			this.Surname = surname;
			this.BirthYear = birthYear;
		}
	}
}

