using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace CollegeApplication.Models
{
    public class EnrollmentModel
    {
        [JsonProperty("enrollmentId")]
        public long EnrollmentId { get; set; }

        [JsonProperty("studentId")]
        public long StudentId { get; set; }

        [JsonProperty("courseId")]
        public long CourseId { get; set; }

        [JsonProperty("enrollmentDate")]
        public DateTime EnrollmentDate { get; set; }

        [JsonProperty("grade")]
        public Grade? Grade { get; set; } // Changed to nullable Grade
    }

    public enum Grade
    {
        A, B, C, D, F, None // Added None as a default value
    }
}