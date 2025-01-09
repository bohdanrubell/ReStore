using Carter;
using Mapster;
using MediatR;

namespace API.Features.Products.CreateProduct;

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/products", async (Request request, ISender sender) =>
        {
            var command = request.Adapt<Command>();

            var result = await sender.Send(command);

            return result.IsFailure ? Results.BadRequest(result.Error) : Results.Ok(result.Value);
        });
    }
}