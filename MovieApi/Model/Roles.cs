using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace MovieApi.Model
{
    public class Roles
    {
        [Key]
        public int ID_Role { get; set; }
        public string Role;
    }
}
