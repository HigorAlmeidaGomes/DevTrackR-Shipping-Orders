using DevTrackR.ShippingOrders.Application.InputModel;
using DevTrackR.ShippingOrders.Application.ViewModels;

namespace DevTrackR.ShippingOrders.Application.Services;
public class ShippingOrderService : IShippingOrderService
{
    public Task<string> Add(AddShippingOrderInputModel model)
    {
        throw new NotImplementedException();
    }

    public Task<ShippingOrderViewModel> GetByCode(string trackingCode)
    {
        throw new NotImplementedException();
    }
}
