namespace DevTrackR.ShippingOrders.Core.Events;
public class ShippingOrderUpdateEvent
{
    public ShippingOrderUpdateEvent(string trackingCode, string contactEmail, string description)
    {
        TrackingCode = trackingCode;
        ContactEmail = contactEmail;
        Description = description;
    }

    public string TrackingCode { get; private set; }
    public string ContactEmail { get; private set; }
    public string Description { get; private set; }
}
