using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private static List<Movie> movies = new List<Movie>()
        {
            new Movie() { Cost=100,Name="M1",MovieId=1},
            new Movie() { Cost=200,Name="M2",MovieId=2},
            new Movie() { Cost=150,Name="M3",MovieId=3},

        };

        public List<Movie> GetAllMovies()
        {
            return movies;

        }
    }
}
