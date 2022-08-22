using CleanArchitecture.Application.Products.Command.CreateProduct;
using CleanArchitecture.Application.Products.Command.RemoveProduct;
using CleanArchitecture.Application.Products.Query.GetProducts;
using CleanArchitecture.WebUI.ExtensionsMethods;
using CleanArchitecture.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;

public class ProductController: ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductModel model)
    {
        CreateProductCommand command = new CreateProductCommand()
        {
            Title = model.Title,
            Description=model.Description,
            Price = model.Price,
            Photos = model.Photos.Select(async x=>await x.ConvertToFileModelAsync()).Select(x=>x.Result).ToList()
        };
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
