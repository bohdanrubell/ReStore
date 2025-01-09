using API.Contracts;
using API.Data;
using API.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Products.GetProductById;

public sealed class Handler : IRequestHandler<Query, Result<ProductResponse>>
{
    private readonly StoreContext _context;

    public Handler(StoreContext context)
    {
        _context = context;
    }

    public async Task<Result<ProductResponse>> Handle(Query request, CancellationToken cancellationToken)
    {
        var productResponse = await _context.Products
            .Where(p => p.Id == request.Id)
            .Select(product => new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Brand = product.Brand,
                QuantityInStock = product.QuantityInStock,
                Type = product.Type
            }).FirstOrDefaultAsync(cancellationToken);

        if (productResponse is null)
            return Result.Failure<ProductResponse>(new Error(
                "GetProduct", "Product not found."));

        return productResponse;
    }
}