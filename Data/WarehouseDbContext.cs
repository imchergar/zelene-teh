using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class WarehouseDbContext: DbContext
{
    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options): base(options)
    {
        
    }
    
    public DbSet<ItemModel> ItemModels { get; set; }
    public DbSet<SellerModel> SellerModels { get; set; }
    public DbSet<RedemptionModel> RedemptionModels { get; set; }
}