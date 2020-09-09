using KafkaProducer.Domain.Producer.Interface;
using KafkaProducer.Infrastructure.Repositories.Interface;
using System.Threading.Tasks;

namespace KafkaProducer.Domain.Producer
{
    public class ProducerKafka: IProducerKafka
    {
        private IKafka _kafka;
        public ProducerKafka(IKafka kafka) => _kafka = kafka;

        public async Task<bool> SendMessageAsync(string message) => await _kafka.SendMessageAsync(message);
    }
}
