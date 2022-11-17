using Confluent.Kafka;
using Kafka.Consumer;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Confluent.Kafka.ConfigPropertyNames;

namespace Kafka.Producer
{
    public class KafkaProducer<TK, TV> : IKafkaProducer<TK, TV>
    {
        private readonly IProducer<TK, TV> _producer;
        private readonly string _topic;

        public KafkaProducer(IProducer<TK, TV> producer, IOptions<KafkaProducerConfig<TK, TV>> topicOptions)
        {
            _producer = producer;
            _topic = topicOptions.Value.Topic;
        }

        public async Task ProduceAsync(TK key, TV value)
        {
            await _producer.ProduceAsync(_topic, new Message<TK, TV> { Key = key, Value = value });
        }

        public void Dispose() => _producer.Dispose();
    }
}