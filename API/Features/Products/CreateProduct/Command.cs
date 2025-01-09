using API.Shared;
using MediatR;

namespace API.Features.Products.CreateProduct;

public class Command : IRequest<Result<int>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public long Price { get; set; }
    public string Type { get; set; }
    public string Brand { get; set; }
    public int QuantityInStock { get; set; }
}