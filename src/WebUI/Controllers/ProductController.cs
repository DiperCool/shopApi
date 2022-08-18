using CleanArchitecture.Application.Products.Command.CreateProduct;
using CleanArchitecture.Application.Products.Command.RemoveProduct;
using CleanArchitecture.Application.Products.Query.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;

public class ProductController: ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    [HttpDelete]
    public async Task<IActionResult> RemoveProduct([FromBody] RemoveProductCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] GetProductsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
