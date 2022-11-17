using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
