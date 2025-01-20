using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class RedemptionEntryController : Controller
{
    private readonly WarehouseDbContext _db;

    public RedemptionEntryController(WarehouseDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {   
        List<RedemptionModel> objRedemptionModelList = _db.RedemptionModels.ToList();

        return View(objRedemptionModelList);
    }
    public IActionResult Create()
    {
        ViewBag.StateOptions = new List<SelectListItem>
        {
            new SelectListItem { Text = "Nije isplaćeno", Value = "Nije isplaćeno" },
            new SelectListItem { Text = "Pripremljen", Value = "Pripremljen" },
            new SelectListItem { Text = "Isplaćeno", Value = "Isplaćeno" }
        };

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
    
}