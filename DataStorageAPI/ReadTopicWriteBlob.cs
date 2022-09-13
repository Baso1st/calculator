using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Models;
using System.Text.Json;
using Azure.Storage.Blobs;
using System.IO;

namespace DataStorageAPI
{
    public class ReadTopicWriteBlob
    {
        private readonly ILogger<ReadTopicWriteBlob> _logger;

        public ReadTopicWriteBlob(ILogger<ReadTopicWriteBlob> log)
        {
            _logger = log;
        }

        [FunctionName("ReadTopicWriteBlob")]
        public async Task Run([ServiceBusTrigger("multi-storage", "blob-storage", Connection = "service_bus_connection")]string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
            await AddMessageToBlobAsync(mySbMsg);
        }

        private async Task AddMessageToBlobAsync(string mySbMsg)
        {
            var equation = JsonSerializer.Deserialize<Equation>(mySbMsg);
            var connectionString = Environment.GetEnvironmentVariable("blob_connection");
            var client = new BlobClient(connectionString, "equation", $"{equation.id}.json");
            using var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);
            writer.Write(mySbMsg);
            writer.Flush();
            memoryStream.Position = 0;
            await client.UploadAsync(memoryStream);
        }
    }
}
