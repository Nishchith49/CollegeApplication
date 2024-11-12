using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CollegeApplication.Models; 

namespace CollegeApplication.Entities
{
    public class Student
    {
        [Key]
        [Required]
        [Column("Student_Id", TypeName = "bigint")] 
        public long StudentId { get; set; }

        [Column("Name", TypeName = "nvarchar(100)")]
        public string? Name { get; set; }

        [Column("Date_Of_Birth", TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        [Column("Email", TypeName = "nvarchar(100)")]
        [EmailAddress]
        public string? Email { get; set; }

        [Column("Phone_No", TypeName = "nvarchar(20)")]
        [Required(ErrorMessage = "The Phone Number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "The Phone Number should be 10 digits.")]
        public string? Phone { get; set; }

        // Foreign key
        [Column("Department_Id", TypeName = "bigint")] // Use bigint instead of long for MySQL
        public long DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public Department? Department { get; set; }

        // Navigation property
        public List<Enrollment>? Enrollments { get; set; }
    }
}