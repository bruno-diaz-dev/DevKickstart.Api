using DevKickstart.Api.Contracts.Requests;
using FluentValidation;

namespace DevKickstart.Api.Validators;
public class CrearUsuarioRequestValidator : AbstractValidator<CrearUsuarioRequest>
{
    public CrearUsuarioRequestValidator()
    {
        RuleFor(c => c.Nombre)
            .NotEmpty()
            .WithMessage("El nombre es obligatorio.")
            .MinimumLength(3)
            .WithMessage("El nombre debe tener al menos 3 caracteres.");

        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage("La contraseña es obligatoria.");
    }
}
