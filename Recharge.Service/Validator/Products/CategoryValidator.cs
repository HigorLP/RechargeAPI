using FluentValidation;
using Recharge.Domain.Models.Products;

namespace Recharge.Application.Validators.Products;
public class CategoryValidator : AbstractValidator<Category> {
    public CategoryValidator() {
        RuleFor(category => category.Name)
            .NotEmpty().WithMessage("O nome da categoria não pode estar vazio.")
            .MaximumLength(255).WithMessage("O nome da categoria não pode exceder 255 caracteres.");
    }
}