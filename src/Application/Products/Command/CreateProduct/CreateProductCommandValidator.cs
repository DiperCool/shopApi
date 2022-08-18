using FluentValidation;

namespace CleanArchitecture.Application.Products.Command.CreateProduct;

public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x=>x.Description)
            .NotEmpty();
        RuleFor(x=>x.Price)
            .NotEmpty()
            .GreaterThan(0);
        RuleFor(x=>x.Title)
            .MaximumLength(40)
            .NotEmpty();
    }
}
