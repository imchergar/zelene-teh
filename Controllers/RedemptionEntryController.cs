using System.Diagnostics.CodeAnalysis;
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
    
    public IActionResult Index(int? page, string issueDate = "")
    {
        int pageNumber = page ?? 1;
        int pageSize = 5;

        var selectedCompanyId = GetCompanyFromSession();

        var redemptionQuery = _db.RedemptionModels
            .Include(r => r.Seller) 
            .Include(r => r.Company)
            .Where(r => r.Company.Id == selectedCompanyId); 
        
        redemptionQuery = GetRedemptionByDate(issueDate, redemptionQuery);
        
        var objRedemptionModelList = redemptionQuery.ToPagedList(pageNumber, pageSize);
        
        return View(objRedemptionModelList);
    }
    
    public IActionResult Create()
    {
        RedemptionStatusList();
        GetSellers();
        GetItems();
        
        return View();
    }

  
    [HttpPost]
    public IActionResult Create(RedemptionModel obj, int? selectedItemId)
    {
        PopulateRedemptionWithCompany(obj);
        PopulateRedemptionWithSeller(obj);

        obj.InternalNumber = GenerateSequenceNumber();
        obj.UpdateDate = DateTime.Now;
        
        var validationResult = ValidateRedemptionModelObject(obj);
        if (validationResult != null)
        {
            return validationResult;
        }
        try
        {
            _db.RedemptionModels.Add(obj);
            _db.SaveChanges();
            
            SaveItemRedemptionRelation(obj, selectedItemId);
            
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
        
        var redemption = _db.RedemptionModels
            .Include(r => r.Seller)
            .Include(r => r.Company)
            .FirstOrDefault(r => r.Id == id);
        
        
        if (redemption == null)
        {
            return NotFound();
        }

        RedemptionStatusList();
        GetSellers();
        GetItems();

        return View(redemption);
    }
    
    [HttpPost]
    public IActionResult Edit(RedemptionModel obj, int? selectedItemId)
    {
        var validationResult = ValidateRedemptionModelObject(obj);
        if (validationResult != null)
        {
            return validationResult;
        }
        
        PopulateRedemptionWithCompany(obj);
        PopulateRedemptionWithSeller(obj);

        obj.UpdateDate = DateTime.Now;
        try
        {
            _db.RedemptionModels.Update(obj);
            _db.SaveChanges();
            
            SaveItemRedemptionRelation(obj, selectedItemId);
            
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
    
    
    private void PopulateRedemptionWithCompany(RedemptionModel obj)
    {
        var selectedCompanyId = GetCompanyFromSession();

        if (!selectedCompanyId.HasValue) return;
        var companyDatabase = GetCompanyByCompanyId(selectedCompanyId);

        if (companyDatabase != null)
        {
            obj.Company = companyDatabase;
        }
    }

    private void PopulateRedemptionWithSeller(RedemptionModel obj)
    {
        if (obj.SellerIdentifier == 0) return;
        var sellerDatabase = GetSellerBySellerId(obj);
            
        if (sellerDatabase != null)
        {
            obj.Seller = sellerDatabase;
        }
    }

    private SellerModel? GetSellerBySellerId(RedemptionModel obj)
    {
        SellerModel? sellerDatabase = _db.SellerModels.Find(obj.SellerIdentifier);
        return sellerDatabase;
    }

    private CompanyModel? GetCompanyByCompanyId([DisallowNull] int? selectedCompanyId)
    {
        int companyId = selectedCompanyId.Value;
        CompanyModel? companyDatabase = _db.CompanyModels.Find(companyId);
        return companyDatabase;
    }

    private int? GetCompanyFromSession()
    {
        int? selectedCompanyId = HttpContext.Session.GetInt32("SelectedCompanyId");
        return selectedCompanyId;
    }
    
    private int GenerateSequenceNumber()
    {
        var sequence = _db.InternalNumberSequences.FirstOrDefault();

        if (sequence == null)
        {
            sequence = new InternalNumberSequenceModel()
            {
                CurrentValue = 1
            };
            _db.InternalNumberSequences.Add(sequence);
            _db.SaveChanges();
        }

        sequence.CurrentValue++;

        _db.SaveChanges();
        
        int year = DateTime.Now.Year;
        return   int.Parse($"{DateTime.Now.Year}{sequence.CurrentValue:D2}");
    }
    
    private static IQueryable<RedemptionModel> GetRedemptionByDate(string issueDate, IQueryable<RedemptionModel> redemptionQuery)
    {
        if (!string.IsNullOrEmpty(issueDate))
        {
            DateTime parsedDate = DateTime.Now;
            if (DateTime.TryParse(issueDate, out DateTime date))
            {
                parsedDate = date;
            }
        
            redemptionQuery = redemptionQuery.Where(r => r.IssueDate.Date == parsedDate.Date); 
        }

        return redemptionQuery;
    }
    
    private void GetItems()
    {
        ViewBag.ItemOptions = _db.ItemModels 
            .Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = i.Product 
            }).ToList();

    } 
    

    private IActionResult? ValidateRedemptionModelObject(RedemptionModel obj)
    {
        
        if (obj.Amount <= 0)
        {
            ModelState.AddModelError("Amount", "Iznos mora biti veci od nula.");
        }

        if (string.IsNullOrWhiteSpace(obj.State))
        {
            ModelState.AddModelError("State", "Odaberite status.");
        }
        
        return ModelState.ErrorCount > 0 ? null : View(obj);
    }
    
    private void SaveItemRedemptionRelation(RedemptionModel obj, int? selectedItemId)
    {
        if (selectedItemId.HasValue)
        {
            var selectedItem = _db.ItemModels.Find(selectedItemId);
            ItemRedemptionModel itemRedemption = new ItemRedemptionModel()
            {
                ItemModelId = selectedItem.Id, 
                RedemptionModelId = obj.Id 
            };
            _db.ItemRedemptionModels.Add(itemRedemption);
            _db.SaveChanges();
        }
    }

}