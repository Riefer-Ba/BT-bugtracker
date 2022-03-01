using System.ComponentModel.DataAnnotations;

namespace bugtracker.Models
{
    public class Role
    {
        [Required]
        public string RoleName { get; set; }

        public string RoleId { get; set; }

        public List<string> Users { get; set;}

    }
}
