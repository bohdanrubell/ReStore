using API.Data;
using API.DTOs;
using API.Entities.OrderAggregate;
using API.Extensions;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace API.Controllers;

public class PaymentsController(PaymentService paymentService, StoreContext context, IConfiguration config): BaseApiController
{
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent()
    {
        var basket = await context.Baskets
            .RetrieveBasketWithItems(User.Identity.Name)
            .FirstOrDefaultAsync();

        if (basket is null) return NotFound();

        var intent = await paymentService.CreateOrUpdatePaymentIntent(basket);

        if (intent is null) return BadRequest(new ProblemDetails { Title = "Problem creating payment intent" });

        basket.PaymentIntentId ??= intent.Id;
        basket.ClientSecret ??= intent.ClientSecret;

        context.Update(basket);

        var result = await context.SaveChangesAsync() > 0;

        if (!result) return BadRequest(new ProblemDetails { Title = "Problem updating basket with intent" });

        return basket.MapBasketToDto();
    }

    [HttpPost("webhook")]
    public async Task<ActionResult> StripeWebook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"],
            config["StripeSettings:WhSecret"]);

        var charge = (Charge)stripeEvent.Data.Object;

        var order = await context.Orders.FirstOrDefaultAsync(x =>
            x.PaymentIntentId == charge.PaymentIntentId);

        if (charge.Status == "succeeded") order.OrderStatus = OrderStatus.PaymentReceived;

        await context.SaveChangesAsync();

        return new EmptyResult();
    }
}