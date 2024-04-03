using DevTrackR.ShippingOrders.Core.Entities;

namespace DevTrackR.ShippingOrders.Application.InputModel;
public class ShippingServiceInputModel
{
    public string Title { get; set; }
    public decimal PricePerKg { get; set; }
    public decimal FixedPrice { get; set; }
    public ShippingService ToEntity()
        => new(Title, PricePerKg, FixedPrice);
}
