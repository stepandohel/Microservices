using Confluent.Kafka;

namespace Kafka.Consumer
{
    public class KafkaConsumerConfig<TKey, TValue> : ConsumerConfig
    {
        public string Topic { get; set; } = null!;

        public KafkaConsumerConfig()
        {
            AutoOffsetReset = Confluent.Kafka.AutoOffsetReset.Earliest;
            EnableAutoOffsetStore = false;
            AllowAutoCreateTopics = true;
        }
    }
}
