using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Models;

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
        public async Task Run([ServiceBusTrigger("multi-storage", "cosmos", Connection = "connection-string")]string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
            //Console.WriteLine($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
            await AddMessageToCosmosAsync(mySbMsg);
        }

        private async Task AddMessageToCosmosAsync(string mySbMsg)
        {
            var cosmosUrl = "https://calculator-cosmos.documents.azure.com:443/";
            var cosmosKey = "vUN3Gaxlal69T8MHkvyf3eUxeMqE8nJIVERLU3c7USWl7gC1AJAgHwVDRMwDB42Uw0kJxPwb77Iznp3pqsJmkQ==";
            var dbName = "Calculator";
            var client = new CosmosClient(cosmosUrl, cosmosKey);
            var database = client.GetDatabase(dbName);
            var container = database.GetContainer("Equation");
            var equation = JsonSerializer.Deserialize<Equation>(mySbMsg);
            await container.CreateItemAsync<Equation>(equation);
        }
    }
}
