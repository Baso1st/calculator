using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Models;

namespace calculatorApi
{
    public static class SumTwoNumbers
    {
        [FunctionName("SumTwoNumbers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var x = Double.Parse(req.Query["x"]);
            var y = Double.Parse(req.Query["y"]);
            var result = x + y;

            var operation = new Equation { X = x, Y = y, Operation = Operation.Summition, Result = result };
            var qSender = new QueueSender();
            await qSender.Enqueue(operation);

            return new OkObjectResult(result);
        }
    }
}
