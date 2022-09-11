using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace calculatorApi
{
    public static class SubtractTwoNumbers
    {
        [FunctionName("SubtractTwoNumbers")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var x = Double.Parse(req.Query["x"]);
            var y = Double.Parse(req.Query["y"]);
            var result = x - y;

            return new OkObjectResult(result);
        }
    }
}
