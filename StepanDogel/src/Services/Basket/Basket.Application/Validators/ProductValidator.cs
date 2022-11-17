using Basket.Application.Models.Product;
using FluentValidation;

namespace Basket.Application.Validators
{
    public sealed class ProductPostValidator : AbstractValidator<ProductPostModel>
    {
        public ProductPostValidator()
        {
            RuleFor(x => x.Price).NotEmpty().WithMessage("Please enter price");
        }
    }

    public sealed class ProductPutValidator : AbstractValidator<ProductPutModel>
    {
        public ProductPutValidator()
        {
            RuleFor(x => x.Price).NotEmpty().WithMessage("Please enter price");
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product must be selected");
        }
    }
}
