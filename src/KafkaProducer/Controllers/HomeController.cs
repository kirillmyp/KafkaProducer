using KafkaProducer.Domain.Producer.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KafkaProducer.Controllers
{
    [Produces("application/json")]
    [Route("home")]
    [ApiController]
    public class HomeController : Controller
    {
        private IProducerKafka _producerKafka;
        public HomeController(IProducerKafka producerKafka) => (_producerKafka) = (producerKafka);

        [HttpPost("Send")]
        public async Task<ActionResult> SendAsync([FromBody] string message) => Ok(await _producerKafka.SendMessageAsync(message));
    }
}
