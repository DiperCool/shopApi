namespace CleanArchitecture.Application.IntegrationTests.Orders.Query;

using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Application.IntegrationTests.Helpers;
using CleanArchitecture.Application.Orders.Query.GetOrdersQuery;
using FluentAssertions;
using NUnit.Framework;
using static Testing;
public class GetOrdersQueryTests: BaseTestFixture
{
    [Test]
    public async Task ShouldDenyAnonymouses()
    {
        var command = new GetOrdersQuery();
        command.GetType().Should().BeDecoratedWith<AuthorizeAttribute>();

        var action = ()=> SendAsync(command);

        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
    [Test]
    public async Task ShouldDenyNonAdministrator()
    {
        await RunAsDefaultUserAsync();
        var command = new GetOrdersQuery();

        var action = ()=> SendAsync(command);

        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
    [Test]
    public async Task ShouldGetOrders()
    {
        await RunAsAdministratorAsync();
        var orders= OrderHelper.GetOrders();
        orders.ForEach(async x=>await AddAsync(x));
        
        var command = new GetOrdersQuery(){
            PageNumber=1,
            PageSize=orders.Count
        };
        var result = await SendAsync(command);

        result.Items.Count.Should().Be(orders.Count);


    }
}
