using EcoCycle.Application.DTOs;
using FluentValidation;

namespace EcoCycle.Application.Validators
{
    public class CreatePublicacionValidator : AbstractValidator<CreatePublicacionDto>
    {
        public CreatePublicacionValidator()
        {
            RuleFor(x => x.IdUsuario)
                .GreaterThan(0);

            RuleFor(x => x.IdMaterial)
                .GreaterThan(0);

            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0");

            RuleFor(x => x.Descripcion)
                .MaximumLength(255);

            RuleFor(x => x.Ubicacion)
                .MaximumLength(255);

            RuleFor(x => x.Latitud)
                .InclusiveBetween(-90m, 90m)
                .When(x => x.Latitud.HasValue);

            RuleFor(x => x.Longitud)
                .InclusiveBetween(-180m, 180m)
                .When(x => x.Longitud.HasValue);

            RuleFor(x => x.Imagen)
                .MaximumLength(255);
        }
    }

    public class UpdatePublicacionValidator : AbstractValidator<UpdatePublicacionDto>
    {
        public UpdatePublicacionValidator()
        {
            RuleFor(x => x.IdPublicacion)
                .GreaterThan(0);

            RuleFor(x => x.IdMaterial)
                .GreaterThan(0);

            RuleFor(x => x.Cantidad)
                .GreaterThan(0);

            RuleFor(x => x.Descripcion)
                .MaximumLength(255);

            RuleFor(x => x.Latitud)
                .InclusiveBetween(-90m, 90m)
                .When(x => x.Latitud.HasValue);

            RuleFor(x => x.Longitud)
                .InclusiveBetween(-180m, 180m)
                .When(x => x.Longitud.HasValue);
        }
    }
}
