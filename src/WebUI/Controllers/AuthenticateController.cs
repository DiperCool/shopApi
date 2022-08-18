using CleanArchitecture.Application.Authenticate.Command.Login;
using CleanArchitecture.Application.Authenticate.Register.Command;
using CleanArchitecture.Application.Users.Query.GetMe;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;
[Route("api/[controller]/[action]")]
public class AuthenticateController: ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpGet]
    public async Task<IActionResult> GetMe()
    {
        return Ok(await Mediator.Send(new AuthUserQuery ()));
    }
}