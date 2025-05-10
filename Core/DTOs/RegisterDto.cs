using System.ComponentModel.DataAnnotations;

namespace Core.DTOs;

public class RegisterDto
{
    [Required]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters")]
    public required string FullName { get; set; }
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; }="Customer";
}