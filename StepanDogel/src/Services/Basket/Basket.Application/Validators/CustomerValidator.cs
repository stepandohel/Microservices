using Basket.Application.Models.Customer;
using FluentValidation;

namespace Basket.Application.Validators
{
    public sealed class CustomerPostValidator : AbstractValidator<CustomerCreateModel>
    {
        public CustomerPostValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter name");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Please enter surname");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Please enter address");
        }
    }

    public sealed class CustomerPutValidator : AbstractValidator<CustomerUpdateModel>
    {
        public CustomerPutValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter name");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Please enter surname");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Please enter address");
            RuleFor(x => x.Id).NotEmpty().WithMessage("Customer must be selected");
        }
    }
}
