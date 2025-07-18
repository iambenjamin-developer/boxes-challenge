using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Retrieves a list of all movies.
        /// </summary>
        [HttpGet("Get")]
        public IActionResult GetMovies()
        {
            var movieList = _movieService.GetAllMovies();
            return Ok(movieList);
        }
    }
}
