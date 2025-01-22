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
    public DbSet<CompanyModel> CompanyModels { get; set; }
    
    public DbSet<InternalNumberSequenceModel> InternalNumberSequences { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CompanyModel>().HasData(
            new CompanyModel() { 
                Id = 1, 
                CompanyName= "Zelene tehnologije", 
                Email= "zelene.tehnologije@gmail.com", 
                Password = "Zelene12."
            },
            new CompanyModel() { 
                Id = 2, 
                CompanyName= "Plave tehnologije", 
                Email= "plave.tehnologije@gmail.com", 
                Password = "Plave12."
            },
            new CompanyModel() { 
                Id = 3, 
                CompanyName= "Cistoca", 
                Email= "cistoca@gmail.com", 
                Password = "Cistoca.1" 
            }

        );

    }
}