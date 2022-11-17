using Confluent.Kafka;

namespace Kafka.Producer
{
    public class KafkaProducerConfig<TK, TV> : ProducerConfig
    {
        public string Topic { get; set; }
        public KafkaProducerConfig()
        {
        }
    }
}
