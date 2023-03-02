using System;
using AutoMapper;
using Movie_Store.Entity;
using Movie_Store.Mapper;
using Movie_Store.Repository.Abstract;
using Movie_Store.Repository.Concrete;
using Store.Data.UnitOfWork;
using Store.Service.Abstract;
using Store.Service.Concrete;

namespace Movie_Store.Extension
{
	public static class StartUpDIExtension
	{
		public static void AddServiceDI(this IServiceCollection services)
		{
            //Bu kısımda hangi classlarımızı başlangıçta çalıştırıcaz.
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IGenericRepository<Genre>, GenericRepository<Genre>>();

			services.AddScoped<IGenreService, GenreService>();

            // mapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingConfig());
            });
            services.AddSingleton(mapperConfig.CreateMapper());
        }
	}
}

