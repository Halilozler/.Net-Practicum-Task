using System;
using _1.Odev.Context;
using _1.Odev.Model;

namespace _1.Odev.Service
{
	public class StudentService : GenericService<Student>, IStudentService
	{
		public StudentService(AppDbContext context) : base(context)
		{
		}
	}
}

