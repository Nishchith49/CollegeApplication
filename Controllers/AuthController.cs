using CollegeApplication.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CollegeApplication.Models;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace CollegeApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CollegeContext collegeContext;
        private readonly IConfiguration configuration;

        public AuthController(CollegeContext collegeContext, IConfiguration configuration)
        {
            this.collegeContext = collegeContext;
            this.configuration = configuration;
        }

        [HttpPost("Register")]
        public ActionResult<User> Register(UserDto request)
        {
            var existingUser = collegeContext.Users.FirstOrDefault(u => u.Email == request.Email);
            if (existingUser != null)
            {
                return BadRequest("Email is already registered.");
            }
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Created = DateTime.UtcNow
            };

            collegeContext.Users.Add(user);
            collegeContext.SaveChanges();
            return Ok(user);
        }

        [HttpPost("Login")]
        public ActionResult<string> Login(UserDto request)
        {
            var user = collegeContext.Users.FirstOrDefault(u => u.UserName == request.UserName && u.Email == request.Email);
            if (user == null)
            {
                return NotFound("User Not Found");
            }
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong Password");
            }
            var token = CreateToken(user);
            return Ok(token);
        }

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordModel request)
        {
            var user = collegeContext.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
            {
                return NotFound("User not found");
            }

            user.PasswordResetToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            user.Expires = DateTime.UtcNow.AddHours(1);

            collegeContext.SaveChanges();

            return Ok("Password reset token generated. Please check your email.");
        }

        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel request)
        {
            var user = collegeContext.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null || user.PasswordResetToken != request.Token || user.Expires < DateTime.UtcNow)
            {
                return BadRequest("Invalid token or token expired");
            }

            CreatePasswordHash(request.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = null; 
            user.Expires = DateTime.UtcNow.AddSeconds(15); 

            collegeContext.SaveChanges();

            return Ok("Password has been reset successfully.");
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"] ?? throw new ArgumentNullException("JwtSettings:Key is not configured.")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(59),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}