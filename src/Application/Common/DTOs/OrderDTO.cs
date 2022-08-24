using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Common.DTOs;

public class OrderDTO: IMapFrom<Order>
{
    public Guid Id { get; set; }
    public UserDTO User { get; set; }= new UserDTO();
    public List<OrderItemDTO> OrderItems { get; set; }= new List<OrderItemDTO>();
    public int TotalPrice { get; set; }
    public PaymentType PaymentType { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}
