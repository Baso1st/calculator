using System;
using Azure.Storage.Blobs;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Models;
using Azure.Data.Tables;
using System.Text.Json;

namespace DataStorageAPI
{
    public class ReadTopicWriteTable
    {
        private readonly ILogger<ReadTopicWriteTable> _logger;

        public ReadTopicWriteTable(ILogger<ReadTopicWriteTable> log)
        {
            _logger = log;
        }

        [FunctionName("ReadTopicWriteTable")]
        public async Task Run([ServiceBusTrigger("multi-storage", "table-storage", Connection = "service_bus_connection")]string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
            await AddMessageToBlobAsync(mySbMsg);
        }
        private async Task AddMessageToBlobAsync(string mySbMsg)
        {
            var connectionString = Environment.GetEnvironmentVariable("storage_connection");
            var client = new TableServiceClient(connectionString);
            var tableClient = client.GetTableClient("equation");
            var equationEntity = JsonSerializer.Deserialize<EquationEntity>(mySbMsg);
            equationEntity.PartitionKey = equationEntity.Operation;
            equationEntity.RowKey = equationEntity.id;
            await tableClient.AddEntityAsync<EquationEntity>(equationEntity);
        }
    }
}
