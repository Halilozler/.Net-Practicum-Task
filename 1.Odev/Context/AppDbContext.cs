using System;
using _1.Odev.Model;
using Microsoft.EntityFrameworkCore;

namespace _1.Odev.Context
{
	public class AppDbContext : DbContext
	{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // dbsets Entityleri yani tablolarımız tanımladık
        public DbSet<Student> Student { get; set; }
    }
}

