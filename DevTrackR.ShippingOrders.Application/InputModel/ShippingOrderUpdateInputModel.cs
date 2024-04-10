using DevTrackR.ShippingOrders.Core.Entities;

namespace DevTrackR.ShippingOrders.Application.InputModel;
public class ShippingOrderUpdateInputModel
{
    public string TrackingCode { get; set; }
    public string ContactEmail { get; set; }
    public string Description { get; set; }
    public decimal WeightInKg { get; set; }
    public bool Completed { get; set; }
    public DeliveryAddressInputModel DeliveryAddress { get; set; }
    public List<ShippingServiceInputModel> Services { get; set; }

    public ShippingOrder ToEntity()
        => new(
            Description,
            WeightInKg,
            DeliveryAddress.ToValueObject()
        );

    
}
