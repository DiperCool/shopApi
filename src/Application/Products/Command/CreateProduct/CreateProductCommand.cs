using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Products.Command.CreateProduct;
[Authorize(MustBeAdmin = true)]
public class CreateProductCommand : IRequest<Guid>
{
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public List<FileModel> Photos { get; set; } = new List<FileModel>();
    public int Price { get; set; }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    IApplicationDbContext _applicationDbContext;
    IFileService _fileService;

    public CreateProductCommandHandler(IApplicationDbContext applicationDbContext, IFileService fileService)
    {
        _applicationDbContext = applicationDbContext;
        _fileService = fileService;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = new Product() { Title = request.Title, Description = request.Description, Price = request.Price, Photos = request.Photos.Select(x=>_fileService.SaveFile(x)).ToList()};
        await _applicationDbContext.Products.AddAsync(product);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        return product.Id;
    }
}
