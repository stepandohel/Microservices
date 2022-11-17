using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
