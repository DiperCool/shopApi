using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace CleanArchitecture.Infrastructure.Services
{
    public class StripeService : IBillingService
    {

        public string CreateBillingUser(string email)
        {

            var options = new CustomerCreateOptions
            {
                Email = email,
                Name = email,
            };
            var service = new CustomerService();
            return service.Create(options).Id;
        }

        public string CreateCheckout(User user, Order order)
        {
            var domain = "http://localhost:5001";
            var options = new SessionCreateOptions
            {
                LineItems = order.OrderItems.Select(x => new SessionLineItemOptions()
                {
                    Quantity = x.Quantity,
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        Currency="usd",
                        ProductData= new SessionLineItemPriceDataProductDataOptions()
                        {
                            Name = x.Product.Title,
                            Description = x.Product.Description
                        },
                        UnitAmount = x.Product.Price*100
                    }
                }).ToList(),
                Mode = "payment",
                Customer = user.BillingId,
                SuccessUrl = domain + "?success=true&session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "?canceled=true",
                Metadata = new Dictionary<string, string>() { { "OrderId", order.Id.ToString() } }
            };
            var service = new SessionService();
            Session session = service.Create(options);
            return session.Url;

        }

        public string CreatePortal(User user)
        {
            var returnUrl = "http://localhost:4242";

            var options = new Stripe.BillingPortal.SessionCreateOptions
            {
                Customer = user.BillingId,
                ReturnUrl = returnUrl,
            };
            var service = new Stripe.BillingPortal.SessionService();
            var session = service.Create(options);
            return session.Url;
        }
    }
}