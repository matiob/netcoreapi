using FluentValidation;
using Library.DTOs.Request.Create;

namespace Library.Validations.CreateValidations
{
    public class CreateAutorValidator: AbstractValidator<CreateAutorDTO>
    {
        public CreateAutorValidator()
        {

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.")
                .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres.");

            RuleFor(x => x.FechaNacimiento)
                .NotEmpty().WithMessage("La fecha de nacimiento es requerida.")
                .Must(ValidationHelper.BeValidDate).WithMessage("La fecha de nacimiento no es válida.");

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("El formato del correo electrónico no es válido.");

            RuleFor(x => x.PaisId)
                .NotEmpty().WithMessage("El Id del país es requerido.")
                .Must(id => int.TryParse(id, out int result) && result > 0).WithMessage("El Id del país debe ser un número mayor que cero.");
        }
    }
}
