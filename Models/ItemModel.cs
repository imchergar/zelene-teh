using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class ItemModel
{
    public int Id { get; set; }

    [Required] [StringLength(255)] public string Product { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; } = decimal.Zero;

    [StringLength(20)] public string UnitOfQuantity { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal PricePerPeace { get; set; } = decimal.Zero;

    [DataType(DataType.Date)] public DateTime CreationDate { get; set; } = DateTime.Now;

    [DataType(DataType.Date)] public DateTime UpdateDate { get; set; } = DateTime.Now;
    
}