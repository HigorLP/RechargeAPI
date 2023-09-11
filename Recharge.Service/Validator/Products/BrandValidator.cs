using FluentValidation;
using Recharge.Domain.Models.Products;

namespace Recharge.Application.Validator.Products {
    public class BrandValidator : AbstractValidator<Brand> {
        public BrandValidator() {
            RuleFor(brand => brand.Name).NotEmpty().WithMessage("O nome da marca não pode estar vazio.")
            .MaximumLength(255).WithMessage("O nome da marca não pode exceder 255 caracteres.");
        }
    }
}