using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.DataBaseContext;
using MovieApi.Interface;
using MovieApi.Model;
using MovieApi.Requests;
using NuGet.Protocol.Plugins;

namespace MovieApi.Services
{
    public class MovieService : IMovieInterface
    {
        private readonly ContextDb _context;
        public MovieService(ContextDb context)
        {
            _context = context;
        }

        public async Task<AuthUser> Authorization(LoginModel data)
        {
            var log = await _context.Logins.Where(x => x.login == data.Login && x.password == data.Password).FirstOrDefaultAsync();
            if (log == null)
            {
                throw new UnauthorizedAccessException();
            }
            var user = await _context.Users.Where(x => x.ID_User == log.ID_User).FirstOrDefaultAsync();
            AuthUser au = new ()
            {
                ID_User = log.ID_User,
                Name = user.Name,
                Description = user.Description,
                ID_Role = user.ID_Role,
                login = data.Login,
                password = data.Password
            };
            return au;
        }

        public async Task<IActionResult> CreateNewMovieAsync(CreateNewMovie MovieInfo)
        {
            var movie = new Movies()
            {
                Name = MovieInfo.Name,
                Description = MovieInfo.Description,
                Genre = MovieInfo.Genre,
                PublishingDate = MovieInfo.PublishingDate,
                Rating = MovieInfo.Rating
            };
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return new OkObjectResult(new
            {
                status = true
            });
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return false;
            }
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IActionResult> GetAllMoviesAsync()
        {
            var movies = await _context.Movies.ToListAsync();
            return new OkObjectResult(new
            {
                data = new { movies = movies },
                status = true
            });
        }

        public async Task<Movies> GetMovieInfoAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return null;
            }
            return movie;
        }

        public async Task<Users> Registration(RegistrationRequestModel user)
        {
            Users us = new Users() {
            Name = user.Name,
            Description = user.Description,
            ID_Role = user.ID_Role
            };
            await _context.Users.AddAsync(us);
            await _context.SaveChangesAsync();
            Logins log = new Logins()
            {
                login = user.login,
                password = user.password,
                ID_User = us.ID_User
            };
            await _context.Logins.AddAsync(log);
            await _context.SaveChangesAsync();
            return us;
        }

        public async Task<Movies> UpdateMovieAsync(int id, CreateNewMovie MovieInfo)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return null;
            }
            movie.Name = MovieInfo.Name ?? movie.Name;
            movie.Description = MovieInfo.Description ?? movie.Description;
            movie.Genre = MovieInfo.Genre ?? movie.Genre;
            movie.PublishingDate = MovieInfo.PublishingDate;
            movie.Rating = MovieInfo.Rating;
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return new OkObjectResult( new
                { users = users,
                status = true
            }
                );
        }

        public async Task<IActionResult> GetChatWithUserByID(int SenderId, int ReceiverId)
        {
            bool result = await _context.PrivateChatMessages.AnyAsync(c => (c.SenderId == SenderId.ToString() && c.ReceiverId == ReceiverId.ToString()) ||
            (c.SenderId == ReceiverId.ToString() && c.ReceiverId == SenderId.ToString()));
            return new OkObjectResult(new
            {
                result = result,
                status = true
            });
        }
    }
}
