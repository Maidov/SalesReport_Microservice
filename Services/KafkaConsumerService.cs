using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

public class KafkaConsumerService : BackgroundService
{
    private readonly IConsumer<Null, string> _consumer;
    private readonly string? _topic;

    public KafkaConsumerService(IConfiguration configuration)
    {
        var config = new ConsumerConfig
        {
            GroupId = configuration["Kafka:GroupId"],
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<Null, string>(config).Build();
        _topic = configuration["Kafka:Topic"];
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe(_topic);

        return Task.Run(() =>
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(stoppingToken);
                    if (consumeResult != null)
                    {
                        // Console.WriteLine("Test string for debugging KafkaConsumer");
                        ProcessMessage(consumeResult.Message.Value);
                    }
                }
                catch (OperationCanceledException)
                {
                    _consumer.Close();
                }
            }
        }, stoppingToken);
    }

    private void ProcessMessage(string message)
    {
        // Логика обработки сообщения
    }

    public override Task StopAsync(CancellationToken stoppingToken)
    {
        _consumer.Close();
        return base.StopAsync(stoppingToken);
    }
}
