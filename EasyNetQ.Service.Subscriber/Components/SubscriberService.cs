using System;
using System.IO;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQTest.Shared;

namespace EasyNetQTest.Service.Subscriber
{
    public class SubscriberService : ISubscriberService
    {
        private readonly IBus _bus;
        private readonly TaskFactory _taskFactory = new TaskFactory();

        public SubscriberService(IBus bus)
        {
            _bus = bus;
        }

        #region ISubscriberService Implementation

        public void Start()
        {
            _bus.SubscribeAsync<MyMessage>("test_handler", msg => _taskFactory.StartNew(() => ProcessMessage(msg)));
        }

        public void Stop()
        {
        }

        #endregion

        private void ProcessMessage(MyMessage msg)
        {
            var line = string.Format("Got message -- Text: {0}, Number: {1}, Date: {2}", msg.Text, msg.RandomNumber, msg.Date);

            File.AppendAllText(@"E:\Test.txt", line);
            Console.WriteLine(line);
        }
    }
}