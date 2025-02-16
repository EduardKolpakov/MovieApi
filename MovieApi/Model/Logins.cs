using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Model
{
    public class Logins
    {
        [Key]
        public int ID_Login { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        [Required]
        [ForeignKey("Users")]
        public int ID_User { get; set; }
        public Users Users { get; set; }
    }
}
