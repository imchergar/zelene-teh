using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Controllers;

public class SellerEntryController : Controller
{
    private readonly WarehouseDbContext _db;

    public SellerEntryController(WarehouseDbContext db)
    {
        _db = db;
    }

    public IActionResult Index()
    {   
        List<SellerModel> objSellerEntryList = _db.SellerModels.ToList();

        return View(objSellerEntryList);
    }
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(SellerModel obj)
    {
        _db.SellerModels.Add(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        
        SellerModel? sellerModel = _db.SellerModels.Find(id);

        if (sellerModel == null)
        {
            return NotFound();
        }
        
        return View(sellerModel);
    }
    
    [HttpPost]
    public IActionResult Edit(SellerModel obj)
    {
        _db.SellerModels.Update(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        
        SellerModel? sellerModel = _db.SellerModels.Find(id);

        if (sellerModel == null)
        {
            return NotFound();
        }
        
        return View(sellerModel);
    }
    
    [HttpPost]
    public IActionResult Delete(SellerModel obj)
    {
        _db.SellerModels.Remove(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
}
