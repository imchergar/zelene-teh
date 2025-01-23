namespace WebApplication1.Models;

public class ItemRedemptionModel
{
    public int Id { get; set; } 
    public int ItemModelId { get; set; }
    public int RedemptionModelId { get; set; } 

    public ItemModel ItemModel { get; set; }
    public RedemptionModel RedemptionModel { get; set; }
}