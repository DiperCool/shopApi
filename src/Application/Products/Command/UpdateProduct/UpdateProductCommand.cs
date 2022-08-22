using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Products.Command.UpdateProduct;
[Authorize(MustBeAdmin = true)]
public class UpdateProductCommand : IRequest<Product>
{
    public Guid ProductId { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public int Price { get; set; }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
{
    IApplicationDbContext _applicationDbContext;

    public UpdateProductCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await _applicationDbContext.Products.FirstOrDefaultAsync(x=>x.Id==request.ProductId) ?? throw new NotFoundException("Product with this Id not found");
        product.Title = request.Title;
        product.Description = request.Description;
        product.Price = request.Price;
        _applicationDbContext.Products.Update(product);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        return product;
    }
}
