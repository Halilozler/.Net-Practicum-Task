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
                /*
                services.AddDbContext<MovieDbContext>(opt =>
                {
                    opt.UseSqlServer(dbtype, configure =>
                    {
                        //Otmatik migrations belirtiğimiz konumdan direk olarak alsın
                        configure.MigrationsAssembly("Movie_Store.DbOperations");
                    });
                });
                */
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

