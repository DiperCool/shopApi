using FluentValidation;

namespace CleanArchitecture.Application.SendEmail.Command.Send;
public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    public SendEmailCommandValidator()
    {
        RuleFor(x=>x.Name)
            .NotEmpty();
        RuleFor(x=>x.Phone)
            .NotEmpty();
    }
}