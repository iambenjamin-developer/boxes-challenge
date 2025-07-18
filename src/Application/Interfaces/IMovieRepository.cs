using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMovieRepository
    {
        List<Movie> GetAllMovies();

    }
}
