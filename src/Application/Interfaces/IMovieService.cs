using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMovieService
    {
        List<Movie> GetAllMovies();
    }
}
