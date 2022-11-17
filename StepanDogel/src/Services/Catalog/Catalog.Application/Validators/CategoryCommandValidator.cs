using Catalog.Application.Category.Commands.Create;
using Catalog.Application.Category.Commands.Update;
using FluentValidation;

namespace Catalog.Application.Validators
{
    public sealed class CategoryPostValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CategoryPostValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter name");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter description");
        }
    }
    public sealed class CategoryPutValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public CategoryPutValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter name");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter description");
        }
    }
}
