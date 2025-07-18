using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private static List<Movie> movies = new List<Movie>()
        {
            new Movie(
                "The Shawshank Redemption",
                "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                "Drama",
                new DateTime(1994, 9, 23),
                142,
                9.3
            ),
            new Movie(
                "The Godfather",
                "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                "Crime",
                new DateTime(1972, 3, 24),
                175,
                9.2
            ),
            new Movie(
                "Inception",
                "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea.",
                "Science Fiction",
                new DateTime(2010, 7, 16),
                148,
                8.8
            ),
            new Movie(
                "The Dark Knight",
                "When the menace known as the Joker wreaks havoc, Batman must accept one of the greatest psychological and physical tests of his ability.",
                "Action",
                new DateTime(2008, 7, 18),
                152,
                9.0
            ),
            new Movie(
                "Pulp Fiction",
                "The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in tales of violence and redemption.",
                "Crime",
                new DateTime(1994, 10, 14),
                154,
                8.9
            )

        };

        public List<Movie> GetAllMovies()
        {
            return movies;

        }
    }
}
