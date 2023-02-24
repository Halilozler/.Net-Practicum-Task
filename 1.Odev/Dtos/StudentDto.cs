using System;
using _1.Odev.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace _1.Odev.Dtos
{
	public class StudentDto
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
        [MinAgeAttribute()]
        public DateTime BirthYear { get; set; }

        public string FullName => Name + " " + Surname;
	}
}

