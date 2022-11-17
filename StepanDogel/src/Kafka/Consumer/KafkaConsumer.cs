using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using static Confluent.Kafka.ConfigPropertyNames;

namespace Kafka.Consumer
{
    public class KafkaConsumer<TK, TV> : BackgroundService
    {
        private readonly KafkaConsumerConfig<TK, TV> _config;
        private IKafkaConsumerHandler<TK, TV> _consumerHandler;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public KafkaConsumer(IOptions<KafkaConsumerConfig<TK, TV>> config, IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _config = config.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            _consumerHandler = scope.ServiceProvider.GetRequiredService<IKafkaConsumerHandler<TK, TV>>();

            var builder = new ConsumerBuilder<TK, TV>(_config).SetValueDeserializer(new KafkaDeserializer<TV>());
            // these are the lines of code for creating a topic, because autocreatetopics doesn't work
            var configp = new ProducerConfig()
            {
                BootstrapServers = "broker:29092"
            };
            var producer = new ProducerBuilder<TK, TV>(configp).Build();
            producer.Produce(_config.Topic, new Message<TK, TV> { Value = default });
            //
            using var consumer = builder.Build();
            consumer.Subscribe(_config.Topic);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5);
                var result = consumer.Consume(TimeSpan.FromMilliseconds(10));

                if (result != null)
                {
                    await _consumerHandler.HandleAsync(result.Message.Key, result.Message.Value);

                    consumer.Commit(result);
                    consumer.StoreOffset(result);
                }
            }
        }
    }
}