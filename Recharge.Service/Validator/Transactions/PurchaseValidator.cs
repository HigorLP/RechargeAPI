using FluentValidation;
using Recharge.Domain.Models.Transactions;

namespace Recharge.Application.Validator.Transactions;
public class PurchaseValidator : AbstractValidator<Purchase> {
    public PurchaseValidator() {
        RuleFor(purchase => purchase.Payment)
        .NotEmpty().WithMessage("A forma de pagamento não pode estar vazia.")
        .MaximumLength(60).WithMessage("A forma de pagamento não pode exceder 60 caracteres.");

        RuleFor(purchase => purchase.Status)
        .NotEmpty().WithMessage("O status da compra não pode estar vazio.")
        .MaximumLength(60).WithMessage("O status da compra não pode exceder 60 caracteres.");
    }
}