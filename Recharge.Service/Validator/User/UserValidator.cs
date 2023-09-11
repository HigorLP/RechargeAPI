using FluentValidation;

namespace Recharge.Application.Validator.User;
public class UserValidator : AbstractValidator<Domain.Models.Users.User> {
    public UserValidator() {
        RuleFor(user => user.Name)
            .NotEmpty().WithMessage("O nome de usuário não pode estar vazio.")
            .MaximumLength(60).WithMessage("O nome de usuário não pode exceder 60 caracteres.");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("O e-mail não pode estar vazio.")
            .MaximumLength(60).WithMessage("O e-mail não pode exceder 60 caracteres.");
    }
}