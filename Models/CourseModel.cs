using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CollegeApplication.Models
{
    public class CourseModel
    {
        [JsonProperty("courseId")]
        public long CourseId { get; set; }

        [Required]
        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string? Description { get; set; } // Marked as nullable

        [Range(1, 5)]
        [JsonProperty("credits")]
        public long Credits { get; set; }

        [JsonProperty("departmentId")]
        public long DepartmentId { get; set; }
    }
}