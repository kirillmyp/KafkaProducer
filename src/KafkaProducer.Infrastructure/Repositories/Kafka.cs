using Confluent.Kafka;
using KafkaProducer.Infrastructure.Config;
using KafkaProducer.Infrastructure.Repositories.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace KafkaProducer.Infrastructure.Repositories
{
    public class Kafka : IKafka
    {
        private KafkaOptions _kafkaOptions { get; }

        public Kafka(IOptions<AppSettingsOptions> appSettingsOptions)
        {
            _kafkaOptions = appSettingsOptions?.Value?.KafkaOptions ?? throw new ArgumentNullException(nameof(appSettingsOptions));
        }

        public async Task<bool> SendMessageAsync(string message, string broker = null, string topic = null)
        {
            broker = string.IsNullOrEmpty(broker) ? _kafkaOptions.Broker : broker;
            topic = string.IsNullOrEmpty(topic) ? _kafkaOptions.Topic : topic;

            var config = new ProducerConfig
            {
                BootstrapServers = broker,
                ClientId = topic,
                SaslMechanism = SaslMechanism.Plain,
                SecurityProtocol = SecurityProtocol.SaslPlaintext,
            };
            try
            {

                using (var producer = new ProducerBuilder<string, string>(config).Build())
                {

                    var key = string.Empty;
                    var val = message;
                    var index = val.IndexOf(":");
                    if (index != -1)
                    {
                        key = val.Substring(0, index);
                        val = val.Substring(index + 1);
                    }

                    try
                    {
                        var deliveryReport = await producer.ProduceAsync(
                            topic, new Message<string, string> { Key = key, Value = val });

                        Console.WriteLine($"delivered to: {deliveryReport.TopicPartitionOffset}");
                    }
                    catch (ProduceException<string, string> e)
                    {
                        Console.WriteLine($"failed to deliver message: {e.Message} [{e.Error.Code}]");
                        throw e;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
