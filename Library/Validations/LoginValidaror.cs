using FluentValidation;
using Library.DTOs.Request.Auth;

namespace Library.Validations
{
    public class LoginValidaror: AbstractValidator<LoginDTO>
    {
        public LoginValidaror() 
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("El nombre de usuario es requerido.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La constraseña es requerida.")
            .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
            .Matches("[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula.")
            .Matches("[^a-zA-Z0-9]").WithMessage("La contraseña debe contener al menos un carácter especial.");
        
        }
    }
}
