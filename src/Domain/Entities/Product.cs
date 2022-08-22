namespace CleanArchitecture.Domain.Entities;
public class Product:AuditableEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public int Price { get; set; }
    public List<Photo> Photos { get; set; } = new List<Photo>();

}
