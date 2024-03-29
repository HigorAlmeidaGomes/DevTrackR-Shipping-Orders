using DevTrackR.ShippingOrders.Application.InputModel;
using DevTrackR.ShippingOrders.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevTrackR.ShippingOrders.API.Controllers;
[Route("api/shipping-orders")]
[ApiController]
public class ShippingOrdersController : ControllerBase
{
    private readonly IShippingOrderService _shippingOrderService;
    public ShippingOrdersController(IShippingOrderService shippingOrderService)
    {
            _shippingOrderService = shippingOrderService;   
    }
    [HttpGet("{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        var pesquisa = await _shippingOrderService.GetByCode(code);
        
        return pesquisa == null ? NotFound() : Ok(pesquisa);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(AddShippingOrderInputModel model)
    {
        var code = await _shippingOrderService.Add(model);

            return CreatedAtAction(
                nameof(GetByCode), 
                new {code},
                model);
    }
}
