using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Consumer
{
    public interface IKafkaConsumerHandler<TK, TV>
    {
        Task HandleAsync(TK key, TV value);
    }
}
