using System;
using System.Net;
using System.Threading.Tasks;
using calculatorApi;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace QueueTopicConnector
{
    public class ReadFromQWriteToTopic
    {
        [FunctionName("ReadFromQWriteToTopic")]
        public static async Task Run([QueueTrigger("operation", Connection = "Endpoint=sb:::calculator-service-bus.servicebus.windows.net:;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=WvbPtxguSIiv2VRb5G9009dfkrlQHD8JXGwjuGZN59g=")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            var topicSender = new TopicSender();
            await topicSender.Enqueue(myQueueItem);
        }
    }
}