using System;
using Movie_Store.Entity;
using Movie_Store.Repository.Abstract;
using Movie_Store.Repository.Concrete;
using Store.Data.UnitOfWork;

namespace Movie_Store.Extension
{
	public static class StartUpDIExtension
	{
		public static void AddServiceDI(this IServiceCollection services)
		{
            //Bu kısımda hangi classlarımızı başlangıçta çalıştırıcaz.
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IGenericRepository<Genre>, GenericRepository<Genre>>();
		}
	}
}

