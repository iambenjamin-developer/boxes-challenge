namespace Application.DTOs.Workshops
{
    public class WorkshopDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FormattedAddress { get; set; }
        public bool Active { get; set; }
        public string Type { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        // Este es un string que contiene un JSON de Google Places
        public string Address { get; set; }

        // Objeto anidado
        public TimeZoneDto TimeZone { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Otros campos que podrían no usarse pero están en la respuesta
        public string? CountryCode { get; set; }
        public string ZoneInformation { get; set; }
    }
}
