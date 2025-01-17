using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class SellerModel
{
    public int Id { get; set; }
    
    [Required] 
    [MaxLength(11)] 
    public int Oib { get; set; }
    
    [Required]
    [StringLength(100)] 
    public required string Name { get; set; } 
    
    [Required]
    [StringLength(100)] 
    public required string Surname { get; set; }
}