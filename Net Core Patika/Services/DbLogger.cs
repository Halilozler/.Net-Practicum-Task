using System;
namespace Net_Core_Patika.Services
{
	public class DbLogger : ILoggerService
	{
		public DbLogger()
		{
		}

        public void Write(string message)
        {
            Console.WriteLine("[DBLogger] - " + message);
        }
    }
}

