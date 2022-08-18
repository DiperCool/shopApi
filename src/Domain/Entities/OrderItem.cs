namespace CleanArchitecture.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = new Product();
    public int Quantity { get; set; }
    public int TotalPrice { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = new Order();
}
