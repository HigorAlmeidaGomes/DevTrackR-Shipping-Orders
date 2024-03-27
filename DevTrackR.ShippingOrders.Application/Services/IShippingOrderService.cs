using DevTrackR.ShippingOrders.Application.InputModel;
using DevTrackR.ShippingOrders.Application.ViewModels;

namespace DevTrackR.ShippingOrders.Application.Services;
public interface IShippingOrderService
{
    Task<string> Add(AddShippingOrderInputModel model);
    Task<ShippingOrderViewModel> GetByCode(string trackingCode);
}
