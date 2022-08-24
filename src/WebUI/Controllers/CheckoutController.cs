using CleanArchitecture.Application.Orders.Command.CreateOrderCommand;
using CleanArchitecture.Application.Orders.Query.GetOrdersQuery;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;

public class CheckoutController: ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Checkout([FromBody] CreateOrderCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders([FromQuery] GetOrdersQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
