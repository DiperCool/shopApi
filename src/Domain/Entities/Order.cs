using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }= new User();
    public List<OrderItem> OrderItems { get; set; }= new List<OrderItem>();
    public int TotalPrice { get; set; }
    public PaymentType PaymentType { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}
