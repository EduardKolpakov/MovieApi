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

        [ForeignKey("Roles")]

        public int Id_Role { get; set; }
        public Roles role { get; set; }
    }
}