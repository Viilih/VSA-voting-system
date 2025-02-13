using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using votingSystem.Api.Features.Votes;
using votingSystem.Api.Features.Votes.ProcessVote;

namespace votingSystem.Api.Infrastructure.Messaging.RabbitMQ;

public class RabbitMqConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RabbitMqConsumer> _logger;
    private IConnection _connection;
    private IModel _channel;

    public RabbitMqConsumer(ILogger<RabbitMqConsumer> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker starting at: {time}", DateTimeOffset.Now);
        
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest",
        };
        
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        
        _channel.QueueDeclare("vote", 
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation("Received: {Message}", message);
            if (int.TryParse(message, out int candidateId))
            {
                await ProcessVote(candidateId);
            }
            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume(queue: "vote", 
            autoAck: false, 
            consumer: consumer);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task ProcessVote(int candidateId)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var processVoteHandler = scope.ServiceProvider.GetRequiredService<ProcessVoteHandler>();
            var result = await processVoteHandler.Handle(candidateId);

            if (result.IsSuccess)
            {
                // Successfully processed the vote
                _logger.LogInformation($"Vote for candidate {candidateId} processed successfully.");
            }
            else
            {
                // Log failure message
                _logger.LogError($"Failed to process vote for candidate {candidateId}: {result.Errors[0].Message}");
            }
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _channel?.Close();
        _connection?.Close();
        await base.StopAsync(cancellationToken);
    }
}
