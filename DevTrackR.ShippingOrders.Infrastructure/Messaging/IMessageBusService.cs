namespace DevTrackR.ShippingOrders.Infrastructure.Messaging;
public interface IMessageBusService
{
    void Publish(object data, string routingKey);
}
