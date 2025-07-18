namespace Domain.Entities
{
    public class Movie
    {
        public Movie()
        {
        }
        public Movie(int MovieId, string Name, int Cost)
        {
            this.MovieId = MovieId;
            this.Name = Name;
            this.Cost = Cost;
        }
        public int MovieId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Cost { get; set; }

    }
}
