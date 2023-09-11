using FluentValidation;
using Recharge.Domain.Models.Products;

namespace Recharge.Application.Validator.Products;
public class DatasheetValidator : AbstractValidator<Datasheet> {
    public DatasheetValidator() {
        RuleFor(datasheet => datasheet.Model)
            .NotEmpty().WithMessage("O Modelo do produto não pode estar vazio.")
            .MaximumLength(60).WithMessage("O Modelo do produto não pode exceder 60 caracteres.");

        RuleFor(datasheet => datasheet.Warranty)
            .NotEmpty().WithMessage("A Garantia do produto não pode estar vazia.")
            .MaximumLength(60).WithMessage("A Garantia do produto não pode exceder 60 caracteres.");
    }
}