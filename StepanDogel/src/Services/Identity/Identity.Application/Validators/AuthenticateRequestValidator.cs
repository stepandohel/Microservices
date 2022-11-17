using FluentValidation;
using Identity.Application.Models;

namespace Identity.Application.Validators
{
    public sealed class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequest>
    {
        public AuthenticateRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Please enter username");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Please enter password");
        }
    }
}
