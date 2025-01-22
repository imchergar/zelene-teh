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
        
        var validationResult = ValidateItemModelObject(obj);
        if (validationResult != null)
        {
            return validationResult;
        }
        
        try
        {
            _db.ItemModels.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "An error occurred while saving the item. Please try again.");
            return View(obj); 
        }
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
        var validationResult = ValidateItemModelObject(obj);
        if (validationResult != null)
        {
            return validationResult;
        }
        
        try
        {
            _db.ItemModels.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index"); 
        
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Provjerite sve podatke i probajte opet");
            return View(obj); 
        }
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
    
    private IActionResult? ValidateItemModelObject(ItemModel obj)
    {
        if (!ModelState.IsValid)
        {
            return View(obj);
        }

        if (obj.Product.Length < 3)
        {
            ModelState.AddModelError("Product", "Naziv proizvoda mora biti veći od 3 karaktera.");
        }

        if (obj.Amount <= 0)
        {
            ModelState.AddModelError("Amount", "Cijena ne smije biti nula.");
        }

        if (string.IsNullOrWhiteSpace(obj.UnitOfQuantity))
        {
            ModelState.AddModelError("UnitOfQuantity", "Upišite jediničnu količinu.");
        }

        if (obj.PricePerPeace <= 0)
        {
            ModelState.AddModelError("PricePerPeace", "Upišite cijenu po jedinici količine.");
        }

        return ModelState.IsValid ? null : View(obj);
    }
}