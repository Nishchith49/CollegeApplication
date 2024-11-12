using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeApplication.Entities
{
    public class College
    {
        [Key]
        [Column("College_Id", TypeName = "bigint(20)")]
        public long CollegeId { get; set; }

        [Column("Name", TypeName = "nvarchar(100)")]
        public string? Name { get; set; }

        [Column("Location", TypeName = "nvarchar(100)")]
        public string? Location { get; set; }

        [Column("Dean", TypeName = "nvarchar(100)")]
        public string? Dean { get; set; }

        // Navigation property
        [InverseProperty("College")]
        public List<Department> Departments { get; set; } = new List<Department>();
    }
}