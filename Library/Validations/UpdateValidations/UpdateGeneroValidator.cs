using FluentValidation;
using Library.DTOs.Request.Update;

namespace Library.Validations.UpdateValidations
{
    public class UpdateGeneroValidator : AbstractValidator<UpdateGeneroDTO>
    {
        public UpdateGeneroValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El Id es requerido.")
                .Must(x => int.TryParse(x, out var _)).WithMessage("El Id debe ser un número entero válido.");

            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres.");
        }
    }
}
