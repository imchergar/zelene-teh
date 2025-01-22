using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Data;
using X.PagedList.Extensions;

namespace WebApplication1.Controllers;

public class SellerEntryController : Controller
{
    private readonly WarehouseDbContext _db;

    public SellerEntryController(WarehouseDbContext db)
    {
        _db = db;
    }
    
    
    public IActionResult Index(int? page)
    {
        int pageNumber = page ?? 1;
        int pageSize = 5;
        
        var objSellerEntryList = _db.SellerModels.ToList().ToPagedList(pageNumber, pageSize);
        
        return View(objSellerEntryList);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(SellerModel obj)
    {
        var validationResult = ValidateSellerModelObject(obj);
        if (validationResult != null)
        {
            return validationResult;
        }
        
        try
        {
            _db.SellerModels.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", "Provjerite sve podatke i probajte opet");
            return View(obj); 
        }
       
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
        var validationResult = ValidateSellerModelObject(obj);
        if (validationResult != null)
        {
            return validationResult;
        }
        
        _db.SellerModels.Update(obj);
        try
        {
            _db.SellerModels.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        catch (Exception e)
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
    
    private IActionResult? ValidateSellerModelObject(SellerModel obj)
    {
        if (!ModelState.IsValid)
        {
            return View(obj);
        }

        if (string.IsNullOrWhiteSpace(obj.Oib) || !System.Text.RegularExpressions.Regex.IsMatch(obj.Oib, @"^\d{11}$"))
        {
            ModelState.AddModelError("Oib", "OIB mora biti tocno 11 znamenaka");
        }

        if (string.IsNullOrWhiteSpace(obj.Name) || obj.Name.Length > 100)
        {
            ModelState.AddModelError("Name", "Ime je obavezno polje.");
        }

        if (string.IsNullOrWhiteSpace(obj.Surname) || obj.Surname.Length > 100)
        {
            ModelState.AddModelError("Surname", "Prezime je obavezno polje.");
        }

        if (!ModelState.IsValid)
        {
            return View(obj);
        }

        return ModelState.IsValid ? null : View(obj);
    }
}
