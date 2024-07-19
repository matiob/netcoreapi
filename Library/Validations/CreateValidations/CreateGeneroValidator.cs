using FluentValidation;
using Library.DTOs.Request.Create;
using Library.DTOs.Request.Update;

namespace Library.Validations.CreateValidations
{
    public class CreateGeneroValidator : AbstractValidator<CreateGeneroDTO>
    {
        public CreateGeneroValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres.");
        }
    }
}
