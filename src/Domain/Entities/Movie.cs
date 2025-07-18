namespace Domain.Entities
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int DurationMinutes { get; set; }
        public double Rating { get; set; }

        // Constructor para inicialización
        public Movie(string title, string description, string genre, DateTime releaseDate, int durationMinutes, double rating)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Genre = genre;
            ReleaseDate = releaseDate;
            DurationMinutes = durationMinutes;
            Rating = rating;
        }
    }
}
