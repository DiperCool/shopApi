namespace CleanArchitecture.WebUI.Models;

public class CreateProductModel
{
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public List<IFormFile> Photos { get; set; } = new List<IFormFile>();
    public int Price { get; set; }
}
