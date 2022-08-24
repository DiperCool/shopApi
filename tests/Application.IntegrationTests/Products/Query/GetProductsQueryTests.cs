namespace CleanArchitecture.Application.IntegrationTests.Products.Query;

using CleanArchitecture.Application.IntegrationTests.Helpers;
using CleanArchitecture.Application.Products.Query.GetProducts;
using FluentAssertions;
using NUnit.Framework;
using static Testing;
public class GetProductsQueryTests: BaseTestFixture
{
    [Test]
    public async Task ShouldGetProducts()
    {
        var products = ProductHelper.GetProducts();

        products.ForEach(async x=> await AddAsync(x));

        var command = new GetProductsQuery()
        {
            PageNumber=1,
            PageSize = products.Count
        };

        var result = await SendAsync(command);

        result.Items.Count.Should().Be(products.Count);
    }
}
