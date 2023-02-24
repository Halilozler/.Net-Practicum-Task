using System;
using _1.Odev.Context;
using _1.Odev.Model;

namespace _1.Odev.DataAccess
{
	public class StudentAccess : GenericAccess<Student>, IStudentAccess
    {
		public StudentAccess(AppDbContext context) : base(context)
		{
		}
	}
}

