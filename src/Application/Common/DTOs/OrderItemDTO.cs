using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.DTOs;

public class OrderItemDTO : IMapFrom<OrderItem>
{
    public ProductDTO Product { get; set; } = new ProductDTO();
    public int Quantity { get; set; }
    public int TotalPrice { get; set; }
}
