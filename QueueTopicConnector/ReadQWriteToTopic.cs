using System;
using System.Threading.Tasks;
using calculatorApi;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace QueueTopicConnector
{
    public class ReadQWriteToTopic
    {
        [FunctionName("ReadQWriteToTopic")]
        public async Task RunAsync([ServiceBusTrigger("operation", Connection = "service_bus_connection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            var topicSender = new TopicSender();
            await topicSender.Enqueue(myQueueItem);
        }
    }
}
