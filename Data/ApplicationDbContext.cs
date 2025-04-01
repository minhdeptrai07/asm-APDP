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

            modelBuilder.Entity<FacultyCourse>()
                .HasKey(fc => new { fc.FacultyId, fc.CourseId });

            modelBuilder.Entity<Grade>()
                .HasKey(g => new { g.StudentId, g.CourseId });

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data for Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Admin" },
                new Role { RoleId = 2, RoleName = "Faculty" },
                new Role { RoleId = 3, RoleName = "Student" }
            );

            // Seed data for default Admin and Faculty accounts
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "Admin", Password = "111", FullName = "Ngyen Van Minh ", Email = "nguyenvanminh@example.com", RoleId = 1 },
                new User { UserId = 2, Username = "Faculty", Password = "111", FullName = "Le Duc Trong", Email = "leductrong@example.com", RoleId = 2 }     
            );
        }
    }
}
