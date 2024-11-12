using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CollegeApplication.Models
{
    public class CollegeModel
    {
        [JsonProperty("collegeId")]
        public long CollegeId { get; set; }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("location")]
        public string? Location { get; set; } // Marked as nullable

        [JsonProperty("dean")]
        public string? Dean { get; set; } // Marked as nullable
    }
}