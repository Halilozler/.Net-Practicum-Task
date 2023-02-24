using System;
using _1.Odev.Context;
using Microsoft.EntityFrameworkCore;

namespace _1.Odev.Extension
{
	public static class StartupDbContextExtension
	{
        public static void AddDbContextDI(this IServiceCollection services, IConfiguration configuration)
        {
            var dbtype = configuration.GetConnectionString("DbType");
            if (dbtype == "SQL")
            {
                var dbConfig = configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<AppDbContext>(options => options
                   .UseSqlServer(dbConfig)
                   );
            }
        }
    }
}

