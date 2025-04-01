using System.ComponentModel.DataAnnotations;

namespace ManagerStudent.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required, MaxLength(50)]
        public string RoleName { get; set; } // Admin, Faculty, Student
    }
}
