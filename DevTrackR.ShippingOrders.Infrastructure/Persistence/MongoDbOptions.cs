namespace DevTrackR.ShippingOrders.Infrastructure.Persistence;
public class MongoDbOptions
{
    public string ConnectionString { get; set; }

    public string Database { get; set; }

    public string MongoInitDbRootUserName { get; set; }

    public string MongoInitDbRootPassword { get; set; }
}
