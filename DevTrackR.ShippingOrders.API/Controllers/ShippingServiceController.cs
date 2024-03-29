using DevTrackR.ShippingOrders.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevTrackR.ShippingOrders.API.Controllers;
[Route("api/shipping-services")]
[ApiController]
public class ShippingServiceController : ControllerBase
{
    private readonly IShippingServiceService _shippingServiceService;

    public ShippingServiceController(IShippingServiceService shippingServiceService)
    {
        _shippingServiceService = shippingServiceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _shippingServiceService.GetAll());
    }
}
