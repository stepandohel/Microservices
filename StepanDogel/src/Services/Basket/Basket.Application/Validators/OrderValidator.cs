using Basket.Application.Models.Order;
using FluentValidation;

namespace Basket.Application.Validators
{
    public sealed class OrderPostValidator : AbstractValidator<OrderPostModel>
    {
        public OrderPostValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer must be selected");
            RuleFor(x => x.Products).NotEmpty().WithMessage("Products must be selected");
        }
    }

    public sealed class OrderPutValidator : AbstractValidator<OrderPutModel>
    {
        public OrderPutValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer must be selected");
            RuleFor(x => x.Products).NotEmpty().WithMessage("Products must be selected");
            RuleFor(x => x.Id).NotEmpty().WithMessage("Order must be selected");
        }
    }
}
