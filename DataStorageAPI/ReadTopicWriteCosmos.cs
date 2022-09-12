using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DataStorageAPI
{
    public class ReadTopicWriteCosmos
    {
        private readonly ILogger<ReadTopicWriteCosmos> _logger;

        public ReadTopicWriteCosmos(ILogger<ReadTopicWriteCosmos> log)
        {
            _logger = log;
        }

        [FunctionName("ReadTopicWriteCosmos")]
        public void Run([ServiceBusTrigger("multi-storage", "cosmos", Connection = "connection-string")]string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
            Console.WriteLine($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}
