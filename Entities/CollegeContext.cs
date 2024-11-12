using CollegeApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CollegeApplication.Entities
{
    public class CollegeContext :DbContext
    {
        public CollegeContext(DbContextOptions<CollegeContext> options) : base(options)
        {
        }
        public  DbSet<User> Users { get; set; }
        public DbSet<College> Colleges { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Include Identity related configurations

            // Configure your model here (optional)
        }
    }
}