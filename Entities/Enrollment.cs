using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeApplication.Entities
{
    public class Enrollment
    {
        [Key]
        [Column("Enrollment_Id", TypeName = "bigint")] // Use bigint instead of long for MySQL
        public long EnrollmentId { get; set; }

        // Foreign keys
        [Column("Student_Id", TypeName = "bigint")] // Use bigint instead of long for MySQL
        public long StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        [InverseProperty("Enrollments")]
        public Student? Student { get; set; }

        [Column("Course_Id", TypeName = "bigint")] // Use bigint instead of long for MySQL
        public long CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("Enrollments")]
        public Course? Course { get; set; }

        [Column("Enrollment_Date", TypeName = "datetime")]
        public DateTime EnrollmentDate { get; set; }

        [Column("Grade", TypeName = "char(1)")]
        public Grade? Grade { get; set; }
    }

    public enum Grade
    {
        A = 'A',
        B = 'B',
        C = 'C',
        D = 'D',
        F = 'F'
    }
}