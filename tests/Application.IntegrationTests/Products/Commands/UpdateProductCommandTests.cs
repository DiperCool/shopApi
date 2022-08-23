namespace CleanArchitecture.Application.IntegrationTests.Products.Commands;

using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Application.Products.Command.CreateProduct;
using CleanArchitecture.Application.Products.Command.UpdateProduct;
using FluentAssertions;
using NUnit.Framework;
using static Testing;
public class UpdateProductCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        await RunAsAdministratorAsync();
        var command = new UpdateProductCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
    [Test]
    public async Task ShouldUpdateProduct()
    {
        await RunAsAdministratorAsync();
        var commandCreate = new CreateProductCommand()
        {
            Title = "Phone",
            Price = 100,
            Description = "Phone"
        };
        Guid id = await SendAsync(commandCreate);

        var command = new UpdateProductCommand()
        {
            ProductId = id,
            Price = 120,
            Title = "Phone2",
            Description = "Phone2"
        };
        var result = await SendAsync(command);
        
        result.Id.Should().Be(id);
        result.Price.Should().NotBe(commandCreate.Price);
        result.Title.Should().NotBe(commandCreate.Title);
        result.Description.Should().NotBe(commandCreate.Description);
    }

    [Test]
    public async Task ShouldDenyNonAdministrator()
    {
        await RunAsDefaultUserAsync();

        var command = new UpdateProductCommand()
        {
            ProductId = Guid.NewGuid(),
            Price = 120,
            Title = "Phone2",
            Description = "Phone2"
        };
        var action = ()=> SendAsync(command);
        
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
    [Test]
    public async Task ShouldDenyAnonymouses()
    {
        await RunAsDefaultUserAsync();

        var command = new UpdateProductCommand()
        {
            ProductId = Guid.NewGuid(),
            Price = 120,
            Title = "Phone2",
            Description = "Phone2"
        };
        command.GetType().Should().BeDecoratedWith<AuthorizeAttribute>();
        var action = ()=> SendAsync(command);
        
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
