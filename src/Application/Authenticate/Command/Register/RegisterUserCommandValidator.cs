using System.Text.RegularExpressions;
using FluentValidation;

namespace CleanArchitecture.Application.Authenticate.Register.Command
{
    public class RegisterUserCommandValidator: AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x=>x.Email)
                .EmailAddress()
                .NotEmpty();
            RuleFor(x=>x.Password)
                .Equal(x=>x.ConfirmPassword).WithMessage("The confirmed password does not match the password")
                .NotEmpty();
        }
    }
}