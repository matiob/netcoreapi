using FluentValidation;
using Library.DTOs.Request.Auth;

namespace Library.Validations.UpdateValidations
{
    public class EditUsuarioValidator : AbstractValidator<EditUsuarioDTO>
    {
        public EditUsuarioValidator()
        {
            RuleFor(x => x.Id)
               .NotEmpty().WithMessage("El Id es requerido.")
               .Must(x => Guid.TryParse(x, out var _)).WithMessage("El formato del Id debe ser un Guid válido.");

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("El formato del correo electrónico no es válido.");
        }
    }
}
