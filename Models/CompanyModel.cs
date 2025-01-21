using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class CompanyModel
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Company name is required.")]
    [MaxLength(100, ErrorMessage = "Company name cannot exceed 100 characters.")]
    public string CompanyName { get; set; }
    
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    public string Password { get; set; }
}