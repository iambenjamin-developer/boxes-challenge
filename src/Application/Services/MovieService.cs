using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;

        public MovieService(IMovieRepository _movieRepository)
        {
            movieRepository = _movieRepository;
        }
        public List<Movie> GetAllMovies()
        {
            var movies = movieRepository.GetAllMovies();

            return movies;
        }
    }
}
