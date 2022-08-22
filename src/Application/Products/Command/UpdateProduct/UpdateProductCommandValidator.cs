using FluentValidation;

namespace CleanArchitecture.Application.Products.Command.UpdateProduct;

public class UpdateProductCommandValidator: AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x=>x.ProductId)
            .NotEmpty();
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
