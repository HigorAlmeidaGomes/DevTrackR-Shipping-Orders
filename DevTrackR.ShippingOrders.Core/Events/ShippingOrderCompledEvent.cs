namespace DevTrackR.ShippingOrders.Core.Events;
public class ShippingOrderCompledEvent
{
    public ShippingOrderCompledEvent(string trackingCode)
    {
        TrackingCode = trackingCode;
    }

    public string TrackingCode { get; private set; }
}
