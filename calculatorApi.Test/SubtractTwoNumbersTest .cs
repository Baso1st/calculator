using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;

namespace calculatorApi.Test
{
    public class SubtractTwoNumbersTest
    {

        readonly ILogger _logger = NullLoggerFactory.Instance.CreateLogger("Test");


        [Fact]
        public void TestOkObjectResult()
        {
            var request = GenerateHttpRequest(1, 1);
            var result = SubtractTwoNumbers.Run(request, _logger).Result;
            Assert.IsType<OkObjectResult>(result);
        }


        [Theory]
        [InlineData(8, 4)]
        [InlineData(7, 4)]
        [InlineData(5, 4)]
        public void TestSumXAndY(double x, double y)
        {
            var request = GenerateHttpRequest(x, y);
            var result = SubtractTwoNumbers.Run(request, _logger).Result as OkObjectResult;
            Assert.Equal(x - y, Double.Parse(result?.Value.ToString() ?? ""));
        }

        private DefaultHttpRequest GenerateHttpRequest(double x, double y)
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext());
            var queryParams = new Dictionary<string, StringValues>()
            {
                {"x", x.ToString() },
                {"y", y.ToString() }
            };
            request.Query = new QueryCollection(queryParams);
            return request;
        }
    }
} 