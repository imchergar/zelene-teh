using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly WarehouseDbContext _db;

    public HomeController(ILogger<HomeController> logger, WarehouseDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public IActionResult Index()
    {
        var objCompanyEntryList = _db.CompanyModels.ToList();
        return View(objCompanyEntryList);
    }

    public IActionResult SelectCompany(int companyId)
    {
        HttpContext.Session.SetInt32("SelectedCompanyId", companyId);

        return RedirectToAction("Index");
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}