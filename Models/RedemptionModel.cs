using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class RedemptionModel
{
    public int Id { get; set; }
    
    [Range(1, 999999)] 
    public int InternalNumber { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime IssueDate { get; set; }= DateTime.Now;
    
    [Required] 
    [Column(TypeName = "decimal(18,2)")] 
    public decimal Amount { get; set; } = decimal.Zero;
    
    [Required] 
    [StringLength(100)] 
    public string State { get; set; } = string.Empty;
    
    [StringLength(100)] 
    public string Note { get; set; } = string.Empty;
    
    public required SellerModel Seller { get; set; }
    
    [DataType(DataType.Date)] 
    public DateTime CreationDate { get; set; } = DateTime.Now;
    
    [DataType(DataType.Date)]
    public DateTime UpdateDate { get; set; } = DateTime.Now;
    
    public required CompanyModel Company { get; set; }

}