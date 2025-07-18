using System.Text.Json.Serialization;

namespace Application.DTOs
{
    public class VehicleDto
    {
        [JsonPropertyName("make")]
        public string Make { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("license_plate")]
        public string LicensePlate { get; set; }
    }
}
