using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace DevTrackR.ShippingOrders.Infrastructure.Messaging;
public class RabbitMqService : IMessageBusService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private const string _exchange = "tracking-service";
    public RabbitMqService()
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        _connection = connectionFactory.CreateConnection("tracking-service-publisher");
        _channel = _connection.CreateModel();
    }
    public void Publish(object data, string routingKey)
    {
        var type = data.GetType();  
        var payload = JsonSerializer.Serialize(data);
        var byteArray = Encoding.UTF8.GetBytes(payload);

        Console.WriteLine($"{type.Name} Published");

        _channel.BasicPublish(_exchange, routingKey,null,byteArray);
    }
}
