using CleanArchitecture.Application.Authenticate.Register.Command;
using CleanArchitecture.Application.Common.Exceptions;
using FluentAssertions;
using FluentValidation;
using NUnit.Framework;
using static CleanArchitecture.Application.IntegrationTests.Testing;

namespace CleanArchitecture.Application.IntegrationTests.Authenticate.Commands;

public class RegisterTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new RegisterUserCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<Common.Exceptions.ValidationException>();
    }
    [Test]
    public async Task ShouldBeSamePasswordAndConfirmPassword()
    {

        var command = new RegisterUserCommand()
        {
            Email="psdiperTest234@gmail.com",
            Password="1234",
            ConfirmPassword="1345"
            
        };
        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<Common.Exceptions.ValidationException>();
    }
    [Test]
    public async Task ShouldBeUniqueEmail()
    {
        await SendAsync(new RegisterUserCommand()
        {
            Email="psdiperTest234@gmail.com",
            Password="12345",
            ConfirmPassword="12345"
            
        });

        var command = new RegisterUserCommand()
        {
            Email="psdiperTest234@gmail.com",
            Password="12345",
            ConfirmPassword="12345"
        };

        var action = () => SendAsync(command);
        await action.Should().ThrowAsync<BadRequestException>();

    }
    [Test]
    public async Task ShouldCreateUser()
    {

        var command = new RegisterUserCommand()
        {
            Email="psdiperTest234@gmail.com",
            Password="12345",
            ConfirmPassword="12345"
        };

        var result= await SendAsync(command);
        result.Should().NotBeNull();
        result.AccessToken.Should().NotBeEmpty();
    }
}
