﻿using DevTrackR.ShippingOrders.Application.ViewModels;
using DevTrackR.ShippingOrders.Core.Repositories;

namespace DevTrackR.ShippingOrders.Application.Services;
public class ShippingServiceService : IShippingServiceService
{
    private readonly IShippingServiceRepository _shippingServiceRepository;

    public ShippingServiceService(IShippingServiceRepository shippingServiceRepository)
    {
        _shippingServiceRepository = shippingServiceRepository;
    }
    public async Task<List<ShippingServiceViewModel>> GetAll()
    {
        var shippingServices = await _shippingServiceRepository.GetAllAsync();

        return shippingServices.Select(x => new ShippingServiceViewModel(x.Id, x.Title, x.PricePerKg, x.FixedPrice)).ToList();
    }
}
