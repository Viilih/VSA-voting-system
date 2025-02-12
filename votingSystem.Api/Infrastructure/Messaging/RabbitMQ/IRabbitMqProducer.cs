namespace votingSystem.Api.Infrastructure.Messaging.RabbitMQ;

public interface IRabbitMqProducer
{
    void SendMessage<T>(T message);
}