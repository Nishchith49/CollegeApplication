using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CollegeApplication.Models
{
    public class DepartmentModel
    {
        [JsonProperty("departmentId")]
        public long DepartmentId { get; set; }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("headOfDepartment")]
        public string? HeadOfDepartment { get; set; } 

        [JsonProperty("collegeId")]
        public long CollegeId { get; set; }
    }
}