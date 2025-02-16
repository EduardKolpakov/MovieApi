using Microsoft.AspNetCore.Mvc;
using MovieApi.Interface;
using MovieApi.Model;
using MovieApi.Requests;
using MovieApi.Services;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieInterface _MovieService;
        public MovieController(IMovieInterface movieService)
        {
            _MovieService = movieService;
        }
        [HttpGet]
        [Route("GetAllMovies")]
        public async Task<IActionResult> GetAllMovies()
        {
            return await _MovieService.GetAllMoviesAsync();
        }
        [HttpGet]
        [Route("GetMovieInfo/{id}")]
        public async Task<Movies> GetMovies(int id)
        {
            return await _MovieService.GetMovieInfoAsync(id);
        }
        [HttpPost]
        [Route("CreateNewMovie")]
        public async Task<IActionResult> CreateMovie(CreateNewMovie MovieInfo)
        {
            return await _MovieService.CreateNewMovieAsync(MovieInfo);
        }
        [HttpDelete]
        [Route("DeleteMovie/{id}")]
        public async Task<bool> DeleteMovie(int id)
        {
            return await _MovieService.DeleteMovieAsync(id);
        }
        [HttpPut]
        [Route("UpdateMovie/{id}")]
        public async Task<Movies> UpdateMovie(int id, string name, string description, string genre, DateOnly publishingDate, double rating)
        {
            CreateNewMovie MovieInfo = new CreateNewMovie()
            {
                Name = name,
                Description = description,
                Genre = genre,
                PublishingDate = publishingDate,
                Rating = rating
            };
            return await _MovieService.UpdateMovieAsync(id, MovieInfo);
        }
    }
}
