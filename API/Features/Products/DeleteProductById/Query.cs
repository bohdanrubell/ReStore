using API.Shared;
using MediatR;

namespace API.Features.Products.DeleteProductById;

public class Query : IRequest<Result>
{
    public int Id { get; set; }
}