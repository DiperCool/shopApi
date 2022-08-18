using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Orders.Command.CreateOrderCommand;
[Authorize]
public class CreateOrderCommand : IRequest<string>
{
    public List<OrderItemModel> Items { get; set; } = new List<OrderItemModel>();
    public PaymentType PaymentType { get; set; }
}


public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, string>
{
    IApplicationDbContext _applicationDbContext;
    ICurrentUserService _currentUserService;
    IBillingService _billingService;

    public CreateOrderCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService, IBillingService billingService)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
        _billingService = billingService;
    }

    public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Order order = new Order()
        {
            UserId = _currentUserService.UserIdGuid,
            PaymentType = request.PaymentType,
            PaymentStatus = request.PaymentType == PaymentType.Stripe ? PaymentStatus.Pending : PaymentStatus.Success
        };
        foreach (OrderItemModel item in request.Items)
        {
            Product product = await _applicationDbContext.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId) ?? throw new NotFoundException("Product not found");
            OrderItem orderItem = new OrderItem
            {
                Product = product,
                Quantity = item.Quantity,
                TotalPrice = product.Price * item.Quantity
            };
            order.OrderItems.Add(orderItem);
        }
        order.TotalPrice = order.OrderItems.Sum(x => x.TotalPrice);
        await _applicationDbContext.Orders.AddAsync(order);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        return request.PaymentType == PaymentType.Stripe ? _billingService.CreateCheckout(await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == _currentUserService.UserIdGuid) ?? throw new BadRequestException("Something went wrong"), order) : "";

    }
}
