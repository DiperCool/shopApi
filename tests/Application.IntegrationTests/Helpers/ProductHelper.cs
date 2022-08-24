namespace CleanArchitecture.Application.IntegrationTests.Helpers;

public static class ProductHelper
{
    public static Product GetPhone()
    {
        return new Product()
        {
            Title = "Phone",
            Price = 100,
            Description = "Phone"
        };
    }
    public static Product GetSofa()
    {
        return new Product()
        {
            Title = "Sofa",
            Price = 150,
            Description = "Sofa"
        };
    }
    public static Product GetCar()
    {
        return new Product()
        {
            Title = "Car",
            Price = 300,
            Description = "Car"
        };
    }

    public static List<Product> GetProducts()
    {
        return new List<Product>(){ GetPhone(), GetCar(),GetSofa() };
    }

}
