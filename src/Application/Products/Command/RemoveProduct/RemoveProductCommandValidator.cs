using FluentValidation;

namespace CleanArchitecture.Application.Products.Command.RemoveProduct;

public class RemoveProductCommandValidator: AbstractValidator<RemoveProductCommand>
{
    public RemoveProductCommandValidator()
    {
        RuleFor(x=>x.Id)
            .NotEmpty();
    }
}
