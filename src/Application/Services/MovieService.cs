using Application.DTOs;
using Application.Interfaces;
using AutoMapper;

namespace Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository _movieRepository, IMapper mapper)
        {
            movieRepository = _movieRepository;
            _mapper = mapper;
        }
        public List<MovieDto> GetAllMovies()
        {
            var movies = movieRepository.GetAllMovies();

            var dtos = _mapper.Map<List<MovieDto>>(movies);
            return dtos;
        }
    }
}
