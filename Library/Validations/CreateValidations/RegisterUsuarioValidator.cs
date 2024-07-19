using FluentValidation;
using Library.DTOs.Request.Auth;

namespace Library.Validations.CreateValidations
{
    public class RegisterUsuarioValidator : AbstractValidator<RegisterUsuarioDTO>
    {
        public RegisterUsuarioValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.")
                .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La constraseña es requerida.")
            .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
            .Matches("[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula.")
            .Matches("[^a-zA-Z0-9]").WithMessage("La contraseña debe contener al menos un carácter especial.");
        }
    }
}
