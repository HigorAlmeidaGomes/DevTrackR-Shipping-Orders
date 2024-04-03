using DevTrackR.ShippingOrders.Application.InputModel;
using DevTrackR.ShippingOrders.Application.ViewModels;
using DevTrackR.ShippingOrders.Core.Repositories;

namespace DevTrackR.ShippingOrders.Application.Services;
public class ShippingOrderService : IShippingOrderService
{
    private readonly IShippingOrderRepository _shippingOrderServiceRepository;

    public ShippingOrderService(IShippingOrderRepository shippingOrderServiceRepository)
    {
        _shippingOrderServiceRepository = shippingOrderServiceRepository;
    }
    public async Task<string> Add(AddShippingOrderInputModel model)
    {
        var shippingOrder = model.ToEntity();
        var shippingServices = model.Services.Select(x => x.ToEntity()).ToList();
        shippingOrder.SetupServices(shippingServices);
        await _shippingOrderServiceRepository.AddAsync(shippingOrder);

        return shippingOrder.TrackingCode;
    }

    public async Task<ShippingOrderViewModel> GetByCode(string trackingCode)
    {
        var shippingOrder = await _shippingOrderServiceRepository.GetByCodeAsync(trackingCode);
        return ShippingOrderViewModel.FromEntity(shippingOrder);
    }
}
