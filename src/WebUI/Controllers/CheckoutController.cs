using CleanArchitecture.Application.Orders.Command.CreateOrderCommand;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;

public class CheckoutController: ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Checkout([FromBody] CreateOrderCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
