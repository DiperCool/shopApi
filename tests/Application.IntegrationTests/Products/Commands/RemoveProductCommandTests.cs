namespace CleanArchitecture.Application.IntegrationTests.Products.Commands;

using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Application.Products.Command.CreateProduct;
using CleanArchitecture.Application.Products.Command.RemoveProduct;
using FluentAssertions;
using NUnit.Framework;
using static Testing;
public class RemoveProductCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRemoveProduct()
    {
        await RunAsAdministratorAsync();
        var id = await SendAsync(new CreateProductCommand()
        {
            Title = "Phone",
            Price = 100,
            Description = "phone",
        });
        await SendAsync(new RemoveProductCommand()
        {
            Id = id
        });

        var product = await FindAsync<Product>(id);
        product.Should().BeNull();
    }
    [Test]
    public async Task ShouldDenyNonAdministrator()
    {
        await RunAsDefaultUserAsync();

        var action = () => SendAsync(new RemoveProductCommand()
        {
            Id = Guid.NewGuid()
        });

        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
    [Test]
    public async Task ShouldDenyAnonymouses()
    {
        var command = new RemoveProductCommand()
        {
            Id = Guid.NewGuid()
        };
        command.GetType().Should().BeDecoratedWith<AuthorizeAttribute>();

        var action = () => SendAsync(command);

        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
    [Test]
    public async Task ShouldNotFound()
    {
        await RunAsAdministratorAsync();

        var action = () => SendAsync(new RemoveProductCommand()
        {
            Id = Guid.NewGuid()
        });


        await action.Should().ThrowAsync<NotFoundException>();
    }
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        await RunAsAdministratorAsync();
        var command = new RemoveProductCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

}
