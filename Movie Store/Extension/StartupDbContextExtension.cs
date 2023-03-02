using System;
using Microsoft.EntityFrameworkCore;
using Movie_Store.DbOperations;

namespace Movie_Store.Extension
{
	public static class StartupDbContextExtension
	{
		public static void AddDbContextDI(this IServiceCollection services, IConfiguration configuration)
		{
            var dbtype = configuration.GetConnectionString("DbType");
            if (dbtype == "SQL")
            {
                var dbConfig = configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<MovieDbContext>(options => options
                   .UseSqlServer(dbConfig)
                   );
            }
            else if (dbtype == "PostgreSQL")
            {
                var dbConfig = configuration.GetConnectionString("PostgreSqlConnection");
                services.AddDbContext<MovieDbContext>(options => options
                   .UseNpgsql(dbConfig)
                   );
            }
        }
	}
}

