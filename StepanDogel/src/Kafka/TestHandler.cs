using Kafka.Consumer;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka
{
    public class TestHandler : IKafkaConsumerHandler<string, string>
    {
        private readonly ILogger<TestHandler> _logger;

        public TestHandler(ILogger<TestHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(string key, string value)
        {
            if (value is not null)
                _logger.LogInformation(value.ToString());
            return Task.CompletedTask;
        }
    }
}
