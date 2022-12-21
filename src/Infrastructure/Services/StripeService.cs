
using Jorda.Server.Application.Common.Interfaces;
using MediatR;
using SendGrid;
using Stripe.Checkout;
using Stripe;
using Jorda.Server.Application.Common.Models.Payment;

namespace Jorda.Server.Infrastructure.Services;

public class StripeService: IPaymentService
{
    public StripeService()
    {
       

    }

    public void CreateCheckoutSession(CreateCheckoutSessionRequest request)
    {
        var domain = "http://localhost:4242";

        var priceOptions = new PriceListOptions
        {
            LookupKeys = new List<string> { request.LookupKey }
        };
        var priceService = new PriceService();
        StripeList<Price> prices = priceService.List(priceOptions);

        var options = new SessionCreateOptions
        {
            LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    Price = prices.Data[0].Id,
                    Quantity = 1,
                  },
                },
            Mode = "subscription",
            SuccessUrl = domain + "/success.html?session_id={CHECKOUT_SESSION_ID}",
            CancelUrl = domain + "/cancel.html",
        };
        var service = new SessionService();
        Session session = service.Create(options);
    }
}
