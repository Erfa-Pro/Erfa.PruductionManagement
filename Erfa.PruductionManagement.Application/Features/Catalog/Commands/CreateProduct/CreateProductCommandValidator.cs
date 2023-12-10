using FluentValidation;

namespace Erfa.PruductionManagement.Application.Features.Catalog.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.UserName).NotNull().NotEmpty().WithMessage("Headers are missing {PropertyName}.");
            RuleFor(p => p.ProductNumber).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.MaterialProductName).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.ProductionTimeSec).GreaterThanOrEqualTo(0).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(p => p.Description).NotNull().NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
