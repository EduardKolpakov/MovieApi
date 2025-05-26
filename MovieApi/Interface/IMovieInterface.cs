using Microsoft.AspNetCore.Mvc;
using MovieApi.Model;
using MovieApi.Requests;
using NuGet.Protocol.Plugins;
using System.Runtime.CompilerServices;

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
        Task<Users> Registration(RegistrationRequestModel user);
        Task<IActionResult> GetAllUsersAsync();
        Task<IActionResult> GetChatWithUserByID(int SenderId, int ReceiverId);
        Task<IActionResult> GetUserByID(int id);
        Task<IActionResult> GetChatHistory(int id);
        Task<IActionResult> DeleteMovieMessage(int id);
        Task<IActionResult> UpdateMovieMessage(int id, string newMsg);
    }
}
