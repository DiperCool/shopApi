using FluentValidation;

namespace CleanArchitecture.Application.Orders.Command.CreateOrderCommand;

public class CreateOrderCommandValidator: AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x=>x.Items)
            .NotEmpty();
    }
}
