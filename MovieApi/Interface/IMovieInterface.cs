using Microsoft.AspNetCore.Mvc;
using MovieApi.Model;
using MovieApi.Requests;

namespace MovieApi.Interface
{
    public interface IMovieInterface
    {
        Task<IActionResult> GetAllMoviesAsync();
        Task<Movies> GetMovieInfoAsync(int id);
        Task<IActionResult> CreateNewMovieAsync(CreateNewMovie MovieInfo);
        Task<Movies> UpdateMovieAsync(int id, CreateNewMovie MovieInfo);
        Task<bool> DeleteMovieAsync(int id);
        Task<AuthUser> Authorization(LoginModel data);
        Task<Users> Registration(AuthUser user);
    }
}
