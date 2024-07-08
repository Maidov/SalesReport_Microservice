using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class KafkaTestController : ControllerBase
{
    private readonly KafkaProducerService _producerService;

    public KafkaTestController(KafkaProducerService producerService)
    {
        _producerService = producerService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] string message)
    {
        await _producerService.SendMessageAsync(message);
        return Ok("Message sent to Kafka");
    }
}
