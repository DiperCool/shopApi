using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Application.Products.Command.CreateProduct;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitecture.Application.IntegrationTests.Products.Commands;
using static Testing;
public class CreateProductCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateProduct()
    {
        await RunAsAdministratorAsync();

        var result = await SendAsync(new CreateProductCommand()
        {
            Title = "Phone",
            Price = 100,
            Description = "phone",
        });
        
        result.Should().NotBeEmpty();
    }
    [Test]
    public async Task ShouldDenyNonAdministrator()
    {
        await RunAsDefaultUserAsync();

        var action = () => SendAsync(new CreateProductCommand()
        {
            Title = "Phone",
            Price = 100,
            Description = "phone",
        });
        
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
    [Test]
    public async Task ShouldDenyAnonymouse()
    {
        var command = new CreateProductCommand()
        {
            Title = "Phone",
            Price = 100,
            Description = "phone",
        };
        command.GetType().Should().BeDecoratedWith<AuthorizeAttribute>();

        var action = () => SendAsync(command);

        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
    [Test]
    public async Task ShouldRequireMinimumFields()
    {   
        await RunAsAdministratorAsync();
        var command = new CreateProductCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
}
