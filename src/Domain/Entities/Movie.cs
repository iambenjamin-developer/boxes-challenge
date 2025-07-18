namespace Domain.Entities
{
    public class Movie
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Genre { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public int DurationMinutes { get; private set; }
        public double Rating { get; private set; }

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
