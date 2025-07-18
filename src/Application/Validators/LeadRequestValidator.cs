using Application.DTOs.Leads;
using FluentValidation;

namespace Application.Validators
{
    public class LeadRequestValidator : AbstractValidator<LeadRequestDto>
    {
        public LeadRequestValidator()
        {
            RuleFor(x => x.PlaceId).GreaterThan(0);
            RuleFor(x => x.ServiceType)
                .NotEmpty()
                .Must(v => new[] { "cambio_aceite", "rotacion_neumaticos", "otro" }
                .Contains(v))
                .WithMessage("ServiceType inválido");

            RuleFor(x => x.Contact).NotNull();
            RuleFor(x => x.Contact.Name).NotEmpty();
            RuleFor(x => x.Contact.Email).EmailAddress();

            When(x => x.Vehicle != null, () =>
            {
                RuleFor(x => x.Vehicle.LicensePlate).NotEmpty();
            });
        }
    }
}
