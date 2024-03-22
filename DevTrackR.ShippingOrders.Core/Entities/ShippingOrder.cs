﻿using DevTrackR.ShippingOrders.Core.Enum;
using DevTrackR.ShippingOrders.Core.ValueObjects;

namespace DevTrackR.ShippingOrders.Core.Entities;
public class ShippingOrder : EntityBase
{
    public ShippingOrder(string description, DateTime postedAt, decimal weightInKg, DeliveryAddress deliveryAddress)
    {
        TrackingCode = GenerateTrackingCode();
        Description = description;
        PostedAt = DateTime.Now;
        WeightInKg = weightInKg;
        DeliveryAddress = deliveryAddress;
        Services = [];
        Status = ShippingOrderStatus.Startead;
    }

    public string TrackingCode { get; private set; }
    public string Description { get; private set; }

    public DateTime PostedAt { get; private set; }

    public decimal WeightInKg { get; private set; }

    public DeliveryAddress DeliveryAddress { get; private set; }

    public ShippingOrderStatus Status { get; private set; }

    public List<ShippingOrderService> Services { get; private set; }
    public Decimal TotalPrice { get; private set; }

    public void SetupServices(List<ShippingService> services)
    {
        foreach (var service in services)
        {
            var servicePrice = service.FixedPrice + service.PricePerKg * WeightInKg;

            TotalPrice += servicePrice;
            Services.Add(new ShippingOrderService(service.Title, servicePrice));
        }
    }

    private string GenerateTrackingCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string numbers = "0123456789";

        var code = new char[10];
        var random = new Random();

        for (var i = 0; i < 5; i++)
        {
            code[i] = chars[random.Next(chars.Length)];
        }

        for (var i = 5; i < 10; i++)
        {
            code[i] = numbers[random.Next(numbers.Length)];
        }

        return new String(code);
    }
}
