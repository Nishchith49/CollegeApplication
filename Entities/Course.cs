using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeApplication.Entities
{
    public class Course
    {
        [Key]
        [Column("Course_Id", TypeName = "bigint")] 
        public long CourseId { get; set; }

        [Column("Title", TypeName = "nvarchar(100)")]
        public string? Title { get; set; }

        [Column("Description", TypeName = "nvarchar(500)")]
        public string? Description { get; set; }

        [Column("Credits", TypeName = "bigint")] 
        public long Credits { get; set; }

        // Foreign key
        [Column("Department_Id", TypeName = "bigint")] 
        public long DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        [InverseProperty("Courses")]
        public Department? Department { get; set; }

        // Navigation property
        [InverseProperty("Course")]
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}