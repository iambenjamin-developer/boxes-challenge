using System.Text.Json.Serialization;

namespace Application.DTOs.Leads
{
    public class LeadResponseDto
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("place_id")]
        public int PlaceId { get; set; }

        [JsonPropertyName("appointment_at")]
        public DateTime AppointmentAt { get; set; }

        [JsonPropertyName("service_type")]
        public string ServiceType { get; set; }

        [JsonPropertyName("contact")]
        public ContactDto Contact { get; set; }

        [JsonPropertyName("vehicle")]
        public VehicleDto? Vehicle { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } // opcional, para dar feedback al cliente
    }
}
