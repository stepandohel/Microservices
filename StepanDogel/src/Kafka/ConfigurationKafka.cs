using Confluent.Kafka;
using Kafka.Consumer;
using Kafka.Producer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka
{
    public static class ConfigurationKafka
    {
        public static IServiceCollection AddKafkaConsumer<TK, TV, THandler>(this IServiceCollection services, Action<KafkaConsumerConfig<TK, TV>> configAction) where THandler : class, IKafkaConsumerHandler<TK, TV>
        {
            services.AddScoped<IKafkaConsumerHandler<TK, TV>, THandler>();

            services.AddHostedService<KafkaConsumer<TK, TV>>();

            services.Configure(configAction);

            return services;
        }

        public static IServiceCollection AddKafkaProducer<TKey, TValue>(this IServiceCollection services, Action<KafkaProducerConfig<TKey, TValue>> configAction)
        {
            services.AddConfluentKafkaProducer<TKey, TValue>();

            services.AddSingleton<IKafkaProducer<TKey, TValue>, KafkaProducer<TKey, TValue>>();

            services.Configure(configAction);

            return services;
        }

        private static IServiceCollection AddConfluentKafkaProducer<TKey, TValue>(this IServiceCollection services)
        {
            services.AddSingleton(
                sp =>
                {
                    var config = sp.GetRequiredService<IOptions<KafkaProducerConfig<TKey, TValue>>>();
                    var builder = new ProducerBuilder<TKey, TValue>(config.Value).SetValueSerializer(new KafkaSerializer<TValue>());

                    return builder.Build();
                });

            return services;
        }
    }
}
