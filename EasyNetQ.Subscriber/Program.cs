using System;
using EasyNetQ;
using EasyNetQTest.Shared;

namespace EasyNetQTest.Subscriber
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var bus = RabbitHutch.CreateBus("host=localhost"))
			{
				bus.Subscribe<MyMessage>("test", m => Console.WriteLine(string.Format("Text: {0}, RandomNumber: {1}, Date: {2}", m.Text, m.RandomNumber, m.Date)));
				
				Console.WriteLine("Press <ENTER> to exit...");
				Console.ReadLine();
			}
		}
	}
}
