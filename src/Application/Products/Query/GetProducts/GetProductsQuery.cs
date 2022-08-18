using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.DTOs;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Products.Query.GetProducts;

public class GetProductsQuery : IRequest<PaginatedList<ProductDTO>>
{
    public int PageSize { get; set; } = 1;
    public int PageNumber { get; set; } = 1;
}

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PaginatedList<ProductDTO>>
{
    IApplicationDbContext _applicationDbContext;
    IMapper _mapper;

    public GetProductsQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProductDTO>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Products
                    .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
