using Carter;
using MediatR;

namespace API.Features.Products.GetProductById;

public class Endpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{id}", async (int id, ISender sender) =>
        {
            var query = new Query { Id = id };

            var result = await sender.Send(query);

            return result.IsFailure ? Results.NotFound(result.Error) : Results.Ok(result.Value);
        });
    }
}
