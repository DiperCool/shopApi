using FluentValidation;

namespace CleanArchitecture.Application.Products.Query.GetProducts;

public class GetProductsQueryValidation: AbstractValidator<GetProductsQuery>
{
    public GetProductsQueryValidation()
    {
        RuleFor(x=>x.PageSize)
            .GreaterThanOrEqualTo(1);
        RuleFor(x=>x.PageNumber)
            .GreaterThanOrEqualTo(1);
    }
}
