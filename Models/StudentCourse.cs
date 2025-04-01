using System.ComponentModel.DataAnnotations;

namespace ManagerStudent.Models
{
    public class StudentCourse
    {
        [Key]
        public int StudentId { get; set; }

        [Key]
        public int CourseId { get; set; }

        public User Student { get; set; }
        public Course Course { get; set; }
    }
}
