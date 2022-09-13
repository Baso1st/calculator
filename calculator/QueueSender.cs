using System;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace calculatorApi
{
    internal class QueueSender
    {
        readonly string _connectionString = Environment.GetEnvironmentVariable("service_bus_connection");
        readonly string _queueName = "operation";
        
        public async Task Enqueue(object message)
        {
            await using var client = new ServiceBusClient(_connectionString);
            await using var sender = client.CreateSender(_queueName);
            var jObject = JsonSerializer.Serialize(message);
            var serviceBusMessage = new ServiceBusMessage(jObject);
            await sender.SendMessageAsync(serviceBusMessage);
        }

    }
}
