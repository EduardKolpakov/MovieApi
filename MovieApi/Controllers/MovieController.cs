using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieApi.Interface;
using MovieApi.Model;
using MovieApi.Requests;
using MovieApi.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieInterface _MovieService;
        private readonly IConfiguration _configuration;
        public MovieController(IMovieInterface movieService, IConfiguration cfg)
        {
            _MovieService = movieService;
            _configuration = cfg;
        }
        [HttpPost]
        [Route("Authorization")]
        public async Task<IActionResult> Authorization([FromBody] LoginModel data)
        {
            try
            {

                var authUser = await _MovieService.Authorization(data);
                var token = GenerateJwtToken(authUser.Name, authUser.Description, authUser.ID_Role);

                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        [HttpPost]
        [Route("Registration")]
        public async Task<IActionResult> Register([FromBody] AuthUser user)
        {
            var newUser = await _MovieService.Registration(user);

            var token = GenerateJwtToken(newUser.Name, newUser.Description, newUser.ID_Role);

            return Ok(new
            {
                token,
                user = new
                {
                    newUser.ID_User,
                    newUser.Name,
                    newUser.Description,
                    newUser.ID_Role
                }
            });
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
        [Route("UpdateMovie/{id}")] //ПРИВЕТ ОТ КОД10!!!!!!
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
        private string GenerateJwtToken(string username, string description, int role)
        {
            var jwtConfig = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.Name, username),
        new Claim("ID_Role", role.ToString()),
        new Claim("Description", description)//,
        //new Claim("", )

    };

            var token = new JwtSecurityToken(
                issuer: jwtConfig["Issuer"],
                audience: jwtConfig["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtConfig["ExpireMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}