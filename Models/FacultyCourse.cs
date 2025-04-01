using System.ComponentModel.DataAnnotations;

namespace ManagerStudent.Models
{
    public class FacultyCourse
    {
        [Key]
        public int FacultyId { get; set; }

        [Key]
        public int CourseId { get; set; }

        public User Faculty { get; set; }
        public Course Course { get; set; }
    }
}
