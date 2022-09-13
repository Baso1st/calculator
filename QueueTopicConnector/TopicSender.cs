using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace calculatorApi
{
    internal class TopicSender
    {
        readonly string _connectionString = Environment.GetEnvironmentVariable("service_bus_connection");
        readonly string _topicName = "multi-storage";
        
        public async Task Enqueue(string message)
        {
            await using var client = new ServiceBusClient(_connectionString);
            await using var sender = client.CreateSender(_topicName);
            var serviceBusMessage = new ServiceBusMessage(message);
            await sender.SendMessageAsync(serviceBusMessage);
        }

    }
}
