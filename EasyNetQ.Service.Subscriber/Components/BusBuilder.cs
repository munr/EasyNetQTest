using System;
using System.Configuration;
using EasyNetQ;

namespace EasyNetQTest.Service.Subscriber
{
    public class BusBuilder
    {
        public static IBus CreateMessageBus()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["easynetq"];
            
            if (connectionString == null || connectionString.ConnectionString == string.Empty)
            {
                throw new SystemException("easynetq connection string is missing or empty");
            }

            return RabbitHutch.CreateBus(connectionString.ConnectionString);
        }
    }
}
