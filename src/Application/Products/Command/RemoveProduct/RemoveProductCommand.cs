using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Products.Command.RemoveProduct;
[Authorize(MustBeAdmin = true)]
public class RemoveProductCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, Unit>
{
    IApplicationDbContext _applicationDbContext;

    public RemoveProductCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Unit> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await _applicationDbContext.Products.FirstOrDefaultAsync(x => x.Id == request.Id) ?? throw new NotFoundException("Product with this id doesn't exist");
        _applicationDbContext.Products.Remove(product);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
