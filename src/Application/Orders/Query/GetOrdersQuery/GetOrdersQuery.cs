using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.DTOs;
using MediatR;

namespace CleanArchitecture.Application.Orders.Query.GetOrdersQuery;
[Authorize(MustBeAdmin = true)]
public class GetOrdersQuery: IRequest<PaginatedList<OrderDTO>>
{
    public int PageSize { get; set; } = 1;
    public int PageNumber { get; set; } = 1;
}
public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, PaginatedList<OrderDTO>>
{
    IApplicationDbContext _applicationDbContext;
    IMapper _mapper;

    public GetOrdersQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<OrderDTO>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        return  await _applicationDbContext.Orders
                    .OrderBy(x=>x.Created)
                    .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
