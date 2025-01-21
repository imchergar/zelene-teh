using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using X.PagedList.Extensions;

namespace WebApplication1.Controllers;

public class RedemptionEntryController : Controller
{
    private readonly WarehouseDbContext _db;

    public RedemptionEntryController(WarehouseDbContext db)
    {
        _db = db;
    }
    
    public IActionResult Index(int? page)
    {
        int pageNumber = page ?? 1;
        int pageSize = 5;
        
        var objRedemptionModelList = _db.RedemptionModels
            .Include(r => r.Seller) // Ensure the Seller navigation property is loaded
            .ToPagedList(pageNumber, pageSize);
        
        return View(objRedemptionModelList);
    }
    
    public IActionResult Create()
    {
        RedemptionStatusList();
        GetSellers();

        return View();
    }
    
    [HttpPost]
    public IActionResult Create(RedemptionModel obj)
    {
        obj.UpdateDate = DateTime.Now;
        _db.RedemptionModels.Add(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        
        RedemptionModel? redemptionModel = _db.RedemptionModels.Find(id);

        if (redemptionModel == null)
        {
            return NotFound();
        }

        RedemptionStatusList();
        GetSellers();
        
        return View(redemptionModel);
    }
    
    [HttpPost]
    public IActionResult Edit(RedemptionModel obj)
    {
        obj.UpdateDate = DateTime.Now;
        _db.RedemptionModels.Update(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        
        RedemptionModel? redemptionModel = _db.RedemptionModels.Find(id);

        if (redemptionModel == null)
        {
            return NotFound();
        }
        
        return View(redemptionModel);
    }
    
    [HttpPost]
    public IActionResult Delete(RedemptionModel obj)
    {
        _db.RedemptionModels.Remove(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    
    private void GetSellers()
    {
        var sellers = _db.SellerModels
            .Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Oib.ToString()
            })
            .ToList();

        ViewBag.SellerOptions = sellers;
    }

    private void RedemptionStatusList()
    {
        ViewBag.StateOptions = new List<SelectListItem>
        {
            new SelectListItem { Text = "Nije isplaćeno", Value = "Nije isplaćeno" },
            new SelectListItem { Text = "Pripremljen", Value = "Pripremljen" },
            new SelectListItem { Text = "Isplaćeno", Value = "Isplaćeno" }
        };
    }
}