using CleanArchitecture.Application.SendEmail.Command.Send;
using Microsoft.AspNetCore.Mvc;
namespace CleanArchitecture.WebUI.Controllers;
public class EmailController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult> SendEmail(SendEmailCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}