namespace CleanArchitecture.Application.IntegrationTests.Orders.Command;

using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Application.IntegrationTests.Helpers;
using CleanArchitecture.Application.Orders.Command.CreateOrderCommand;
using CleanArchitecture.Domain.Enums;
using FluentAssertions;
using NUnit.Framework;
using static Testing;
public class CreateOrderCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateCheckoutStripe()
    {
        await RunAsDefaultUserAsync();
        Product product = ProductHelper.GetSofa();
        await AddAsync(product);

        var command = new CreateOrderCommand()
        {
            PaymentType = PaymentType.Stripe,
            Items = new List<OrderItemModel>()
        };
        command.Items.Add(new OrderItemModel()
        {
            ProductId = product.Id,
            Quantity = 2
        });

        var result = await SendAsync(command);

        result.Should().NotBeNull();
        result.PaymentType.Should().Be(PaymentType.Stripe);
        result.CallbackUrl.Should().NotBeNullOrEmpty();
    }
    [Test]
    public async Task ShouldCreateCheckoutCOD()
    {
        await RunAsDefaultUserAsync();
        Product product = ProductHelper.GetCar();
        await AddAsync(product);

        var command = new CreateOrderCommand()
        {
            PaymentType = PaymentType.COD,
            Items = new List<OrderItemModel>()
        };
        command.Items.Add(new OrderItemModel()
        {
            ProductId = product.Id,
            Quantity = 2
        });

        var result = await SendAsync(command);

        result.Should().NotBeNull();
        result.PaymentType.Should().Be(PaymentType.COD);
        result.CallbackUrl.Should().BeEmpty();
    }
    [Test]
    public async Task ShouldDenyAnonymouses()
    {
        var command = new CreateOrderCommand()
        {
            PaymentType = PaymentType.Stripe,
            Items = new List<OrderItemModel>()
        };
        command.Items.Add(new OrderItemModel()
        {
            ProductId = Guid.NewGuid(),
            Quantity = 2
        });

        command.GetType().Should().BeDecoratedWith<AuthorizeAttribute>();

        var action = () => SendAsync(command);

        await action.Should().ThrowAsync<UnauthorizedAccessException>();

    }
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        await RunAsDefaultUserAsync();
        var command = new CreateOrderCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }
}
