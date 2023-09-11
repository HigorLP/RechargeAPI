using FluentValidation;
using Recharge.Domain.Models.Products;

namespace Recharge.Application.Validator.Products;
public class ProductValidator : AbstractValidator<Product> {
    public ProductValidator() {
        RuleFor(product => product.Name)
        .NotEmpty().WithMessage("O nome do produto não pode estar vazio.")
        .MaximumLength(255).WithMessage("O nome do produto não pode exceder 255 caracteres.");

        RuleFor(product => product.Sku)
        .NotEmpty().WithMessage("O SKU do produto não pode estar vazio.")
        .MaximumLength(20).WithMessage("O SKU do produto não pode exceder 20 caracteres.");

        RuleFor(product => product.BarCode)
        .NotEmpty().WithMessage("O Código de Barras do produto não pode estar vazio.")
        .MaximumLength(30).WithMessage("O Código de Barras do produto não pode exceder 30 caracteres.");
    }
}