using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.DTOs;

public class ProductDTO: IMapFrom<Product>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; }= String.Empty;
    public int Price { get; set; }
}
