using API.Contracts;
using API.Shared;
using MediatR;

namespace API.Features.Products.GetProductById;

public class Query : IRequest<Result<ProductResponse>>
{
    public int Id { get; set; }
}