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
        readonly string _connectionString = "Endpoint=sb://calculator-service-bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=WvbPtxguSIiv2VRb5G9009dfkrlQHD8JXGwjuGZN59g=";
        readonly string _topicName = "multi-storage";
        
        public async Task Enqueue(object message)
        {
            await using var client = new ServiceBusClient(_connectionString);
            await using var sender = client.CreateSender(_topicName);
            var jObject = JsonSerializer.Serialize(message);
            var serviceBusMessage = new ServiceBusMessage(jObject);
            await sender.SendMessageAsync(serviceBusMessage);
        }

    }
}
