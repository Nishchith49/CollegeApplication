using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeApplication.Entities
{
    public class Department
    {
        [Key]
        [Column("Department_Id", TypeName = "bigint")] // Use bigint instead of long for MySQL
        public long DepartmentId { get; set; }

        [Column("Name", TypeName = "nvarchar(100)")]
        public string? Name { get; set; }

        [Column("Head_Of_Department", TypeName = "nvarchar(100)")]
        public string? HeadOfDepartment { get; set; }

        // Foreign key
        [Column("College_Id", TypeName = "bigint")] // Use bigint instead of long for MySQL
        public long CollegeId { get; set; }

        [ForeignKey(nameof(CollegeId))]
        [InverseProperty("Departments")]
        public College? College { get; set; }

        // Navigation property
        [InverseProperty("Department")]
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}