using BancolombiaExtractos.Data.Repository;
using BancolombiaExtractos.Data.ViewModels;
using BancolombiaExtractos.Services;
using Microsoft.AspNetCore.Mvc;

namespace BancolombiaExtractos.Controllers;

public class HomeController : Controller
{
    private readonly IRepository _repo;
    private readonly IPdfService _pdfService;

    public HomeController(IRepository repo, IPdfService pdfService)
    { 
        _repo = repo;
        _pdfService = pdfService;
    }

    public IActionResult Index()
    {
        return View(new ExtractoViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Index(ExtractoViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // verify data
        var isValidResult = await _repo.IsCredentialsValid(model.Email, model.Account);
        if (!isValidResult)
            return View(model);
        
        // use pdf service
        
        
        // simple stream to test download
        var stream = new FileStream(@"C:\Users\andre\source\repos\prueba-tecnica-bancolombia\docs\swagger.json",
            FileMode.Open, FileAccess.Read);
        return File(stream, "application/json", "Swagger.json");
    }
}