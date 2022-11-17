using Kafka.Producer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IKafkaProducer<string, string> _kafkaProducer;

        public TestController(ILogger<TestController> logger, IKafkaProducer<string, string> kafkaProducer)
        {
            _logger = logger;
            _kafkaProducer = kafkaProducer;
        }

        [HttpGet(Name = "TestLogs")]
        public void Log()
        {
            _logger.LogInformation("Custom information");
            _logger.LogWarning("Custom warning");
        }
        [HttpPost(Name = "TestKafka")]
        public async void KafkaSend(string message)
        {
           await _kafkaProducer.ProduceAsync("test", message);
        }
    }
}
