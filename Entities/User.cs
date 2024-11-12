using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CollegeApplication.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        public byte [] PasswordHash { get; set; } = Array.Empty<byte> ();
        [Required]
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        public string? PasswordResetToken { get; set; }
        [Required]
        public DateTime Created {  get; set; } = DateTime.Now;
        [Required]
        public DateTime Expires { get; set; } = DateTime.UtcNow.AddMinutes(59);

    }
}
