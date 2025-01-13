// * Krapkasoft Proprietary License Notice
// *
// * This code snippet (Endpoint.cs) is owned by Krapkasoft LLC.
// * Unauthorized sharing, distribution, reproduction, or use of this code with third parties
// * in any form is strictly prohibited.
// *
// * © 2025 Krapkasoft LLC. All rights reserved.

using Carter;
using MediatR;

namespace API.Features.Products.DeleteProductById;

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/products/delete/{id}", async (int id, ISender sender) =>
        {
            var query = new Query
            {
                Id = id
            };

            var result = await sender.Send(query);

            return result.IsFailure ? Results.BadRequest(result.Error) : Results.Ok(result);
        });
    }
}