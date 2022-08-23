using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Common.Models;

public class CheckoutResponse
{
    public PaymentType PaymentType { get; set; }
    public string CallbackUrl { get; set; } = string.Empty;
}
