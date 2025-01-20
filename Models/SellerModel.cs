using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class SellerModel
{
    public int Id { get; set; }
    
    [Required]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "Oib must be exactly 11 numeric digits.")]
    public string Oib { get; set; }
    
    [Required]
    [StringLength(100)] 
    public required string Name { get; set; } 
    
    [Required]
    [StringLength(100)] 
    public required string Surname { get; set; }
}