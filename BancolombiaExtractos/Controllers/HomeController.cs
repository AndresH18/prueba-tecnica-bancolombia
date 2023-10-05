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
        if (isValidResult)
        {
            // use pdf service

            var r = await _pdfService.CreateExtractoPdfStream(model.Account);
            if (r)
                return File(r.Value!, "application/pdf", "Extracto.pdf");


            ModelState.AddModelError("", "No se pudo generar el extracto");
            return View(model);

            // simple stream to test download
        }

        return View(model);
    }
}