using FluentValidation;
using Library.DTOs.Request.Update;

namespace Library.Validations.UpdateValidations
{
    public class UpdateAutorValidator : AbstractValidator<UpdateAutorDTO>
    {
        public UpdateAutorValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El Id es requerido.")
                .Must(x => Guid.TryParse(x, out var _)).WithMessage("El formato del Id debe ser un Guid válido.");

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
                .GreaterThan(0).WithMessage("El Id del país debe ser mayor que cero.");
        }
    }
}
