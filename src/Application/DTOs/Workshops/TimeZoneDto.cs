namespace Application.DTOs.Workshops
{
    public class TimeZoneDto
    {
        public string Id { get; set; }
        public bool HasIanaId { get; set; }
        public string DisplayName { get; set; }
        public string StandardName { get; set; }
        public string DaylightName { get; set; }
        public string BaseUtcOffset { get; set; }
        public bool SupportsDaylightSavingTime { get; set; }
    }
}
