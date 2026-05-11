using DevKickstart.Api.Contracts.Requests;
using FluentValidation;

namespace DevKickstart.Api.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(r => r.Username)
            .NotEmpty()
            .WithMessage("El usuario es obligatorio.")
            .MinimumLength(3)
            .WithMessage("El usuario debe tener al menos 3 caracteres.");

        RuleFor(r => r.Password)
            .NotEmpty()
            .WithMessage("La contraseña es obligatoria.");
    }
}
