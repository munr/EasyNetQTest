using System;
using System.Threading;
using EasyNetQ.Shared;

namespace EasyNetQ.Publisher
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var bus = RabbitHutch.CreateBus("host=localhost"))
			{
				for (var i = 0; i < 1500; i++)
				{
					var message = new Message { Text = "Hello World", RandomNumber = new Random().Next(1, 100), Date = DateTime.Now };
					
					using (var channel = bus.OpenPublishChannel())
					{
						channel.Publish(message);
						Console.WriteLine(string.Format("Published message - Text: {0}, RandomNumber: {1}", message.Text, message.RandomNumber));
					}

					Thread.Sleep(100);
				}
			}

			Console.WriteLine("Press <ENTER> to exit...");
			Console.ReadLine();
		}
	}
}
