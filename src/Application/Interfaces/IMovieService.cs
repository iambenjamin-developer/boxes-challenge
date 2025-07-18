using Application.DTOs;

namespace Application.Interfaces
{
    public interface IMovieService
    {
        List<MovieDto> GetAllMovies();
    }
}
