using System;
using _1.Odev.Model;
using _1.Odev.Service;

namespace _1.Odev.Extension
{
    public static class StartUpDIExtension
    {
        public static void AddServicesDI(this IServiceCollection services)
        {
            services.AddScoped<IGenericService<Student>, GenericService<Student>>();
        }
    }
}

