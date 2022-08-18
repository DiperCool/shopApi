using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

namespace CleanArchitecture.WebUI.Controllers;

public class StripeWebhookController:ApiControllerBase
{
    IApplicationDbContext _applicationDbContext;

    IConfiguration _configuration;

    public StripeWebhookController(IApplicationDbContext applicationDbContext, IConfiguration configuration)
    {
        _applicationDbContext = applicationDbContext;
        _configuration = configuration;
    }

    [HttpPost]
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            // Replace this endpoint secret with your endpoint's unique secret
            // If you are testing with the CLI, find the secret by running 'stripe listen'
            // If you are using an endpoint defined with the API or dashboard, look in your webhook settings
            // at https://dashboard.stripe.com/webhooks
            string endpointSecret = _configuration["Stripe:WebhookSecret"];
            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var signatureHeader = Request.Headers["Stripe-Signature"];
                stripeEvent = EventUtility.ConstructEvent(json,
                signatureHeader, endpointSecret, throwOnApiVersionMismatch: false);//checkout.session.completed
                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    Session? session = stripeEvent.Data.Object as Session;
                    if(session==null) return Ok();
                    Order? order = await _applicationDbContext.Orders.FirstOrDefaultAsync(x=>x.Id==new Guid(session.Metadata["OrderId"]));
                    if(order==null) return Ok();
                    order.PaymentStatus = PaymentStatus.Success;
                    _applicationDbContext.Orders.Update(order);
                    await _applicationDbContext.SaveChangesAsync(ct);                    
                }
                return Ok();
            }
            catch (StripeException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return BadRequest();
            }
        }
}
