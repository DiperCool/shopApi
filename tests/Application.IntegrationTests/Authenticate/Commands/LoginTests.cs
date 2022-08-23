using CleanArchitecture.Application.Authenticate.Command.Login;
using CleanArchitecture.Application.Authenticate.Register.Command;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitecture.Application.IntegrationTests.Authenticate.Commands;
using static Testing;
public class LoginTests: BaseTestFixture
{


    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new LoginUserCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<Common.Exceptions.ValidationException>();
    }
    [Test]
    public async Task ShouldNotFoundUser()
    {
        var command = new LoginUserCommand()
        {
            Login="sf@gmail.com",
            Password="12435"
        };
        var action = () => SendAsync(command);
        
        await action.Should().ThrowAsync<BadRequestException>();
    }
    [Test]
    public async Task ShouldLoginUser()
    {

        await SendAsync(new RegisterUserCommand()
        {
            Email="psdiperTest234@gmail.com",
            Password="12345",
            ConfirmPassword="12345"
        });

        var command = new LoginUserCommand()
        {
            Login="psdiperTest234@gmail.com",
            Password="12345"
        };
        var result = await SendAsync(command);
        result.Should().NotBeNull();
        result.AccessToken.Should().NotBeEmpty();
        
    }
}
