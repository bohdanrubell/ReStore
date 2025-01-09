using FluentValidation;

namespace API.Features.Products.CreateProduct;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Price).NotEmpty();
        RuleFor(x => x.Type).NotEmpty();
        RuleFor(x => x.Brand).NotEmpty();
        RuleFor(x => x.QuantityInStock).GreaterThan(0);
    }
}