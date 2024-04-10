using DevTrackR.ShippingOrders.Application.InputModel;
using DevTrackR.ShippingOrders.Application.ViewModels;
using DevTrackR.ShippingOrders.Core.Events;
using DevTrackR.ShippingOrders.Core.Repositories;
using DevTrackR.ShippingOrders.Infrastructure.Messaging;

namespace DevTrackR.ShippingOrders.Application.Services;
public class ShippingOrderService : IShippingOrderService
{
    private readonly IShippingOrderRepository _shippingOrderServiceRepository;
    private readonly IMessageBusService _messageBus;

    public ShippingOrderService(IShippingOrderRepository shippingOrderServiceRepository, IMessageBusService messageBus)
    {
        _shippingOrderServiceRepository = shippingOrderServiceRepository;
        _messageBus = messageBus;
    }
    public async Task<string> Add(AddShippingOrderInputModel model)
    {
        var shippingOrder = model.ToEntity();
        var shippingServices = model.Services.Select(x => x.ToEntity()).ToList();
        shippingOrder.SetupServices(shippingServices);
        await _shippingOrderServiceRepository.AddAsync(shippingOrder);

        return shippingOrder.TrackingCode;
    }

    public async Task AddUpdate(ShippingOrderUpdateInputModel model)
    {
        var shippingOrderUpdate = model.ToEntity();
        await _shippingOrderServiceRepository.UpdateAsync(shippingOrderUpdate);

        var shippingOrderUpdateEvent = new ShippingOrderUpdateEvent(model.TrackingCode, model.ContactEmail, model.Description);

        _messageBus.Publish(shippingOrderUpdateEvent, "shipping-order-update");
        if (model.Completed)
        {
            var  orderCompletdEvent = new ShippingOrderCompledEvent(model.TrackingCode);
            _messageBus.Publish(orderCompletdEvent, "shipping-order-completed");
        }
    }

    public async Task<ShippingOrderViewModel> GetByCode(string trackingCode)
    {
        var shippingOrder = await _shippingOrderServiceRepository.GetByCodeAsync(trackingCode);
        return ShippingOrderViewModel.FromEntity(shippingOrder);
    }
}
