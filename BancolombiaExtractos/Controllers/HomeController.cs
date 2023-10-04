using BancolombiaExtractos.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BancolombiaExtractos.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View(new ExtractoViewModel());
    }

    [HttpPost]
    public IActionResult Index(ExtractoViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View(viewModel);

        // verify data
        // simple stream to test download
        var stream = new FileStream(@"C:\Users\andre\source\repos\prueba-tecnica-bancolombia\docs\swagger.json", FileMode.Open, FileAccess.Read);
        return File(stream, "application/json", "Swagger.json");
    }
}