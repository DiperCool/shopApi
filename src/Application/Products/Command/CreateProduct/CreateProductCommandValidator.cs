using FluentValidation;

namespace CleanArchitecture.Application.Products.Command.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly List<string> allowedExtensions = new()
    {
        "jpg", "JPG", "png", "PNG",
    };

    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty();
        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThan(0);
        RuleFor(x => x.Title)
            .MaximumLength(40)
            .NotEmpty();
        RuleForEach(x => x.Photos)
            .ChildRules(child =>
            {
                child.RuleFor(x => x.Length)
                    .GreaterThan(0)
                    .LessThanOrEqualTo(2 * 1024 * 1024)
                    .WithMessage("Image file should not exceed 2 MB.");
                child.RuleFor(x => x.NameFile)
                    .NotEmpty()
                    .Must(HaveAllowedExtension)
                    .WithMessage(
                        $"File extension is not allowed. Allowed extensions: {string.Join(", ", allowedExtensions)}.")
                    .Length(1, 50);
            });
    }
    private bool HaveAllowedExtension(string str)
    {
        var extension = str.Split('.').Last();
        return allowedExtensions.Contains(extension);
    }
}
