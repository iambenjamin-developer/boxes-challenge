namespace Domain.Entities
{
    public class Lead
    {
        public long Id { get; set; } // PK para EF Core
        public int PlaceId { get; set; }
        public DateTime AppointmentAt { get; set; }
        public string ServiceType { get; set; }
        public Contact Contact { get; set; }
        public Vehicle? Vehicle { get; set; }
    }
}
