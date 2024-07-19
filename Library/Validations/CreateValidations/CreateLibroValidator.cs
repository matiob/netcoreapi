﻿using FluentValidation;
using Library.DTOs.Request.Create;
using Library.DTOs.Request.Update;

namespace Library.Validations.CreateValidations
{
    public class CreateLibroValidator : AbstractValidator<CreateLibroDTO>
    {
        public CreateLibroValidator()
        {

            RuleFor(x => x.ISBN)
                .MaximumLength(20).WithMessage("El ISBN no puede exceder los 20 caracteres.");

            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("El título es requerido.")
                .MinimumLength(5).WithMessage("El título debe tener al menos 5 caracteres.")
                .MaximumLength(100).WithMessage("El título no puede exceder los 100 caracteres.");

            RuleFor(x => x.FechaPublicacion)
                .Empty().When(x => string.IsNullOrWhiteSpace(x.FechaPublicacion))
                //.Must(ValidationHelper.BeValidDate).WithMessage("La Fecha de Publicación no es válida.")
                .Must(dateString => string.IsNullOrWhiteSpace(dateString) || ValidationHelper.BeValidDate(dateString!)).WithMessage("La Fecha de Publicación no es válida.");

            RuleFor(x => x.GeneroId)
                .NotEmpty().WithMessage("El Id del Género es requerido.")
                .GreaterThan(0).WithMessage("El Id del Género debe ser mayor que cero.");

            RuleFor(x => x.AutorId)
                .NotEmpty().WithMessage("El Id del Autor es requerido.")
                .Must(x => Guid.TryParse(x, out var _)).WithMessage("El formato del AutorId debe ser un Guid válido.");
        }
    }
}