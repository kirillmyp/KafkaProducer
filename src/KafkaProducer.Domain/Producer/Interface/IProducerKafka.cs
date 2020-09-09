using System.Threading.Tasks;

namespace KafkaProducer.Domain.Producer.Interface
{
    public interface IProducerKafka
    {
        Task<bool> SendMessageAsync(string message);
    }
}
