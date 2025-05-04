using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace MovieApi.Model
{
    public class Users
    {
        [Key]
        public int ID_User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [ForeignKey("Roles")]
        public int ID_Role { get; set; }
        public Roles Roles { get; set; }
    }
}