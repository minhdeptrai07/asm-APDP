using ManagerStudent.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagerStudent.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<FacultyCourse> FacultyCourses { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
       .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany()
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.NoAction);  // ✅ Tránh multiple cascade paths

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany()
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FacultyCourse>()
                .HasKey(fc => new { fc.FacultyId, fc.CourseId });

            modelBuilder.Entity<FacultyCourse>()
                .HasOne(fc => fc.Faculty)
                .WithMany()
                .HasForeignKey(fc => fc.FacultyId)
                .OnDelete(DeleteBehavior.NoAction); // ✅ Tránh multiple cascade paths

            modelBuilder.Entity<FacultyCourse>()
                .HasOne(fc => fc.Course)
                .WithMany()
                .HasForeignKey(fc => fc.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Grade>()
                .HasKey(g => new { g.StudentId, g.CourseId });

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.SetNull); // ✅ Khi xóa Role, User không bị xóa
            // Seed data for Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Admin" },
                new Role { RoleId = 2, RoleName = "Faculty" },
                new Role { RoleId = 3, RoleName = "Student" }
            );
        }
    }
}
