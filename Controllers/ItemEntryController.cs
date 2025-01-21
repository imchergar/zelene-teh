using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using X.PagedList.Extensions;

namespace WebApplication1.Controllers;

public class ItemEntryController : Controller
{
    private readonly WarehouseDbContext _db;

    public ItemEntryController(WarehouseDbContext db)
    {
        _db = db;
    }

    public IActionResult Index(int? page)
    {
        int pageNumber = page ?? 1;
        int pageSize = 5;
        
        var objItemEntryList = _db.ItemModels.ToList().ToPagedList(pageNumber, pageSize);
        
        return View(objItemEntryList);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(ItemModel obj)
    {
        _db.ItemModels.Add(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        
        ItemModel? itemModel = _db.ItemModels.Find(id);

        if (itemModel == null)
        {
            return NotFound();
        }
        
        return View(itemModel);
    }
    
    [HttpPost]
    public IActionResult Edit(ItemModel obj)
    {
        _db.ItemModels.Update(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        
        ItemModel? itemModel = _db.ItemModels.Find(id);

        if (itemModel == null)
        {
            return NotFound();
        }
        
        return View(itemModel);
    }
    
    [HttpPost]
    public IActionResult Delete(ItemModel obj)
    {
        _db.ItemModels.Remove(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
}