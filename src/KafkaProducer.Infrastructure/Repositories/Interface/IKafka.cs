using System.Threading.Tasks;

namespace KafkaProducer.Infrastructure.Repositories.Interface
{
    public interface IKafka
    {
        Task<bool> SendMessageAsync(string message, string broker = null, string topic = null);
    }
}
