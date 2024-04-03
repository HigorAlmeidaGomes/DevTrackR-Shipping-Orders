using DevTrackR.ShippingOrders.Core.Entities;
using MongoDB.Driver;

namespace DevTrackR.ShippingOrders.Infrastructure.Persistence;
public class DbSeed
{
    private List<ShippingService> _shippingServices = [
            new ShippingService("Envio estadual", 3.75m, 12),
            new ShippingService("Envio internacional", 5.25m, 15),
            new ShippingService("Caixa tamanho P", 0, 5),
        ];

    private readonly IMongoCollection<ShippingService> _shippingServicesCollection;

    public DbSeed(IMongoDatabase mongoDatabase)
    {
        _shippingServicesCollection = mongoDatabase.GetCollection<ShippingService>("shipping-service");
    }

    public void Populate()
    {
        if (_shippingServicesCollection.CountDocuments(x => true) == 0)
        {
            _shippingServicesCollection.InsertMany(_shippingServices);
        }
    }
}
