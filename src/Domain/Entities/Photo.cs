namespace CleanArchitecture.Domain.Entities;

public class Photo
{
    public int Id { get; set; }
    public string FullPath { get; set; } = String.Empty;
    public string ShortPath { get; set; } = String.Empty;
    public string FileExtension { get; set; } = String.Empty;
    public long Length { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = new Product();
}
