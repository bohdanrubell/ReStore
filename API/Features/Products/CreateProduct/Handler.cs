using API.Data;
using API.Entities;
using API.Shared;
using FluentValidation;
using MediatR;

namespace API.Features.Products.CreateProduct;

internal sealed class Handler : IRequestHandler<Command, Result<int>>
{
    private readonly StoreContext _context;
    private readonly IValidator<Command> _validator;

    public Handler(StoreContext context, IValidator<Command> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Result.Failure<int>(new Error("CreateProduct.Validation",
                validationResult.ToString()));
        }
            
        var product = new Product
        {
            Id = Random.Shared.Next(1, int.MaxValue),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Type = request.Type,
            Brand = request.Brand,
            QuantityInStock = request.QuantityInStock
        };

        _context.Products.Add(product);

        await _context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}