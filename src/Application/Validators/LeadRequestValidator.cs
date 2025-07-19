using Application.DTOs.Leads;
using Application.Interfaces;
using FluentValidation;
using System.Globalization;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class LeadRequestValidator : AbstractValidator<LeadRequestDto>
    {
        private readonly IWorkshopService _workshopService;

        public LeadRequestValidator(IWorkshopService workshopService)
        {
            _workshopService = workshopService;

            // Validación de place_id - required, mayor que 0
            RuleFor(x => x.PlaceId)
                .GreaterThan(0)
                .WithMessage("PlaceId es requerido y debe ser mayor que 0");

            // Validación de si el taller existe
            RuleFor(x => x.PlaceId)
                .Must(WorkshopExists)
                .When(x => x.PlaceId > 0)
                .WithMessage("El PlaceId no corresponde a un taller que exista o esté activo");

            // Validación de appointment_at - required, formato ISO 8601
            RuleFor(x => x.AppointmentAt)
                .NotEmpty()
                .WithMessage("AppointmentAt es requerido")
                .Must(appointment => appointment > DateTime.UtcNow)
                .WithMessage("AppointmentAt debe ser una fecha futura")
                .Must(appointment => appointment.Kind == DateTimeKind.Utc || appointment.Kind == DateTimeKind.Local)
                .WithMessage("AppointmentAt debe tener una zona horaria válida (UTC o Local)");

            // Validación adicional para formato ISO 8601 específico
            RuleFor(x => x.AppointmentAt)
                .Must(appointment =>
                {
                    // Verificar que la fecha se puede formatear correctamente como ISO 8601
                    try
                    {
                        var isoString = appointment.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
                        return DateTime.TryParse(isoString, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out _);
                    }
                    catch
                    {
                        return false;
                    }
                })
                .WithMessage("AppointmentAt debe tener un formato ISO 8601 válido (ej: 2023-10-01T10:00:00Z)");

            // Validación de service_type - required, enum específico
            RuleFor(x => x.ServiceType)
                .NotEmpty()
                .WithMessage("ServiceType es requerido")
                .Must(serviceType => new[] { "cambio_aceite", "rotacion_neumaticos", "otro" }
                    .Contains(serviceType))
                .WithMessage("ServiceType debe ser uno de: cambio_aceite, rotacion_neumaticos, otro");

            // Validación de contact - required
            RuleFor(x => x.Contact)
                .NotNull()
                .WithMessage("Contact es requerido");

            // Validaciones anidadas para Contact
            When(x => x.Contact != null, () =>
            {
                RuleFor(x => x.Contact.Name)
                    .NotEmpty()
                    .WithMessage("El nombre del contacto es requerido")
                    .MaximumLength(100)
                    .WithMessage("El nombre del contacto no puede exceder 100 caracteres");

                RuleFor(x => x.Contact.Email)
                    .NotEmpty()
                    .WithMessage("El email del contacto es requerido")
                    .EmailAddress()
                    .WithMessage("El email del contacto debe tener un formato válido")
                    .MaximumLength(255)
                    .WithMessage("El email del contacto no puede exceder 255 caracteres");

                RuleFor(x => x.Contact.Phone)
                    .NotEmpty()
                    .WithMessage("El teléfono del contacto es requerido")
                    .MaximumLength(20)
                    .WithMessage("El teléfono del contacto no puede exceder 20 caracteres");
            });

            // Validaciones para Vehicle - optional
            When(x => x.Vehicle != null, () =>
            {
                RuleFor(x => x.Vehicle.Make)
                    .NotEmpty()
                    .WithMessage("La marca del vehículo es requerida cuando se proporciona un vehículo")
                    .MaximumLength(50)
                    .WithMessage("La marca del vehículo no puede exceder 50 caracteres");

                RuleFor(x => x.Vehicle.Model)
                    .NotEmpty()
                    .WithMessage("El modelo del vehículo es requerido cuando se proporciona un vehículo")
                    .MaximumLength(50)
                    .WithMessage("El modelo del vehículo no puede exceder 50 caracteres");

                RuleFor(x => x.Vehicle.Year)
                    .GreaterThan(1900)
                    .WithMessage("El año del vehículo debe ser mayor que 1900")
                    .LessThanOrEqualTo(DateTime.Now.Year + 1)
                    .WithMessage("El año del vehículo no puede ser mayor al año siguiente al actual");

                // Si hay vehículo, la patente es obligatoria
                RuleFor(x => x.Vehicle.LicensePlate)
                    .NotEmpty()
                    .WithMessage("La patente del vehículo es requerida cuando se proporciona un vehículo")
                    .MaximumLength(10)
                    .WithMessage("La patente del vehículo no puede exceder 10 caracteres");
            });

        }

        private bool WorkshopExists(int placeId)
        {
            bool workshopExists = _workshopService.ExistsAsync(placeId).Result;
            return workshopExists;
        }
    }
}
