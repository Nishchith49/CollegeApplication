using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CollegeApplication.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        [Required]
        [JsonProperty("userName")]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;
        [JsonProperty("passwordHash")]
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        [JsonProperty("passwordSalt")]
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        [JsonProperty("passwordResetToken")]
        public string PasswordResetToken { get; set; } = string.Empty;
        [JsonProperty("resetTokenExpiryTime")]
        public DateTime? ResetTokenExpiryTime { get; set; }
    }

    public class UserDto
    {
        [Required]
        [JsonProperty("userName")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [JsonProperty("password")]
        public string Password { get; set; }

        public UserDto()
        {
            UserName = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
        }
    }

    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        [JsonProperty("email")]
        public string Email { get; set; }

        public ForgotPasswordModel()
        {
            Email = string.Empty;
        }
    }

    public class ResetPasswordModel
    {
        [Required]
        [JsonProperty("token")]
        public string Token { get; set; }

        [Required]
        [EmailAddress]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [JsonProperty("newpassword")]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [JsonProperty("confirmPassword")]
        public string ConfirmPassword { get; set; }

        public ResetPasswordModel()
        {
            Token = string.Empty;
            Email = string.Empty;
            NewPassword = string.Empty;
            ConfirmPassword = string.Empty;
        }
    }

    public class JwtSettings
    {
        [JsonProperty("key")]
        public string? Key { get; set; }

        [JsonProperty("issuer")]
        public string? Issuer { get; set; }

        [JsonProperty("audience")]
        public string? Audience { get; set; }

        [JsonProperty("expireminutes")]
        public double ExpireMinutes { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {
        // Additional properties can be added here if needed
    }
}