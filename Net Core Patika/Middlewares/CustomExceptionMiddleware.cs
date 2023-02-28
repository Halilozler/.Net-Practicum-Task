using System;
using System.Diagnostics;
using System.Net;
using Net_Core_Patika.Services;
using Newtonsoft.Json;

namespace Net_Core_Patika.Middlewares
{
	//burada girdi ve çıkarken loglanmasını istiyoruz.
	public class CustomExceptionMiddleware
	{
		private readonly RequestDelegate next;

		//DI alalım
		private readonly ILoggerService _loggerService;

		public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
		{
			this.next = next;
			_loggerService = loggerService;
		}

		public async Task Invoke(HttpContext context)   //HttpContext => Microsoft.AspNetCore.Http den geliyor
        {
			//ne kadar sürede çalıştığını göremek için timer yaptık.
			var watch = Stopwatch.StartNew();

			//bizim burda controller kısmında yazdığımız kısımlar aslında "await next(content);" içinde çalışıyr yani buradan kontrol edebiliriz. Burada bütün hepsini bir try-catch içine alırsak aslında genel bir try-catch yapısı oluşturmuşl oluruz. Yani bir throw fırlatığımızda direk olarak burası tetiklenir.
			try
			{
				//Request
				string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path;
				//Console.WriteLine(message);
				//Artık consola aldığımız kısımlarda direk ConsoleService kullanabiliriz.
				_loggerService.Write(message);

				//request bitti ve buraya düştü. yani işlem bittikten sonra response kısmı bundan sonra çalışır.
				await next(context);

				//timerı durdurduk.
				watch.Stop();

				//Response
				//biz burada ayrıca birde ne kadar sürede requesten response geldi bunda görmek istiyoruz.
				message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + "ms";
                //Console.WriteLine(message);
                _loggerService.Write(message);
            }
			catch (Exception ex)
			{
                //timerı durdurduk.
                watch.Stop();

				//Hatalarımızı tek bir yerden kontrol etmek istiyoruz.
				await HandleException(context, ex, watch);
            }
        }

		private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
		{
			//burada biz istediğimiz gibi hata türüne bakarak yönetibiliriz.
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;	//biz sadece 500 döndük.

			string message = "[Error] HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message " + ex.Message + " in " + watch.Elapsed.TotalMilliseconds;
            //Console.WriteLine(message);
            _loggerService.Write(message);

            //Burada json türünde biz geri dönüş yapacağımız için =>"Newtonsoft.Json" kütüphanesini indirdik.
            //yani jsona çevirdik.
            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);

			//contexte yazıp geri döndürdük.
			return context.Response.WriteAsync(result);

			//şimdi direk biz diğer taraftan throw(hata) gönderirsek direk üst taraf yakalacak ve buraya yönlendirip bu işlemleri yapacak.
		}

    }

	public static class CustomExceptionMiddlewareExtension
	{
		public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<CustomExceptionMiddleware>();
		}
	}
}

