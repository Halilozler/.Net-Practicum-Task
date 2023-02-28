using System;
namespace Net_Core_Patika.Services
{
	public class ConsoleLogger : ILoggerService
	{
        public void Write(string message)
        {
            Console.WriteLine("[ConsoleLogger] - " + message);
        }
    }
}

