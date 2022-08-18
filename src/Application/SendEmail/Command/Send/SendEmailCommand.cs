using MediatR;

namespace CleanArchitecture.Application.SendEmail.Command.Send;
public class SendEmailCommand : IRequest
{
    public string? Name { get; set; }
    public string? Phone { get; set; }
}

public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand>
{
    IEmailSender _emailSender;

    IEmailTemplate _emailTemplate;
    IDateTime _dateTime;

    public SendEmailCommandHandler(IEmailSender emailSender, IEmailTemplate emailTemplate, IDateTime dateTime)
    {
        _emailSender = emailSender;
        _emailTemplate = emailTemplate;
        _dateTime = dateTime;
    }

    public Task<Unit> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        string content = _emailTemplate.GetTemplate("application")
                        .Replace("{Name}", request.Name)
                        .Replace("{Phone}", request.Phone)
                        .Replace("{Time}", _dateTime.Now.ToString("MM/dd/yyyy hh:mm tt"));
        _emailSender.Send(content, "Заявка с сайта", _emailSender.GetMail());

        return Unit.Task;
    }
}