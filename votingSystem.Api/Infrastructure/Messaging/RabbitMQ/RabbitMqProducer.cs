using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace votingSystem.Api.Infrastructure.Messaging.RabbitMQ;

public class RabbitMqProducer : IRabbitMqProducer
{
    public void SendMessage<T>(T message)
    {
        var factory = new ConnectionFactory { HostName = "localhost", UserName = "guest", Password = "guest", Port = 5672, VirtualHost = "/" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare("vote", durable: true,exclusive: false, autoDelete: false, arguments: null);

        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish("", "vote", null, body);
    }
}