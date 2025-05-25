using Microsoft.EntityFrameworkCore;
using MovieApi.Model;

namespace MovieApi.DataBaseContext
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Logins> Logins { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Movies> Movies { get; set; }
        public DbSet<MovieChatMessage> MovieChatMessages { get; set; }
        public DbSet<PrivateChatMessage> PrivateChatMessages { get; set; }
    }
}
