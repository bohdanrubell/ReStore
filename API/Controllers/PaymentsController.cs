using API.Data;
using API.DTOs;
using API.Extensions;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class PaymentsController(PaymentService paymentService, StoreContext context): BaseApiController
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
}