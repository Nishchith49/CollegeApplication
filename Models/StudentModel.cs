using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class StudentModel
{
    [JsonProperty("studentId")]
    public long StudentId { get; set; }

    [Required]
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty; 

    [Column(TypeName = "Date")]
    [JsonProperty("dateOfBirth")]
    public DateTime DateOfBirth { get; set; }

    [EmailAddress]
    [JsonProperty("email")]
    public string Email { get; set; } = string.Empty; 

    [JsonProperty("phone")]
    public string? Phone { get; set; } 

    [JsonProperty("departmentId")]
    public long DepartmentId { get; set; }

    // Parameterless constructor
    public StudentModel() { }

    // Constructor with parameters
    public StudentModel(string name, string email)
    {
        Name = name;
        Email = email;
    }
}