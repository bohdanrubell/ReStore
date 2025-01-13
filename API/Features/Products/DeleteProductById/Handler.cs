using API.Data;
using API.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Products.DeleteProductById;

public class Handler : IRequestHandler<Query, Result>
{
    private readonly StoreContext _context;

    public Handler(StoreContext context)
    {
        _context = context;
    }
    
    public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null)  return Result.Failure(new Error(
            "GetProduct", "Product not found."));
        
        _context.Products.Remove(product);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}