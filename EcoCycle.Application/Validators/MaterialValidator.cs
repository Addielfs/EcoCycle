using EcoCycle.Application.DTOs;
using FluentValidation;

namespace EcoCycle.Application.Validators
{
    public class CreateMaterialValidator : AbstractValidator<CreateMaterialDto>
    {
        public CreateMaterialValidator()
        {
            RuleFor(x => x.NombreMaterial)
                .NotEmpty().WithMessage("El nombre del material es obligatorio")
                .MaximumLength(100);

            RuleFor(x => x.Descripcion)
                .MaximumLength(255);
        }
    }

    public class UpdateMaterialValidator : AbstractValidator<UpdateMaterialDto>
    {
        public UpdateMaterialValidator()
        {
            RuleFor(x => x.IdMaterial)
                .GreaterThan(0);

            RuleFor(x => x.NombreMaterial)
                .NotEmpty().WithMessage("El nombre del material es obligatorio")
                .MaximumLength(100);

            RuleFor(x => x.Descripcion)
                .MaximumLength(255);
        }
    }
}
