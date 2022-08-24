using FluentValidation;

namespace CleanArchitecture.Application.Orders.Query.GetOrdersQuery;

public class GetOrdersQueryValidator: AbstractValidator<GetOrdersQuery>
{
    public GetOrdersQueryValidator()
    {
        RuleFor(x=>x.PageSize)
            .GreaterThanOrEqualTo(1);
        RuleFor(x=>x.PageNumber)
            .GreaterThanOrEqualTo(1);
    }
}
