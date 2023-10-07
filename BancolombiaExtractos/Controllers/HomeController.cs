using BancolombiaExtractos.Data.Repository;
using BancolombiaExtractos.Data.ViewModels;
using BancolombiaExtractos.Services;
using Microsoft.AspNetCore.Mvc;

namespace BancolombiaExtractos.Controllers;

/// <summary>
///     Controlador para la ruta <i>Home/*</i> y <i>/*</i>
/// </summary>
public class HomeController : Controller
{
    private readonly IPdfService _pdfService;
    private readonly IRepository _repo;

    public HomeController(IRepository repo, IPdfService pdfService)
    {
        _repo = repo;
        _pdfService = pdfService;
    }

    /// <summary>
    ///     Endpoint para la ruta <i>Index</i> o la principal del Controller
    /// </summary>
    public IActionResult Index()
    {
        return View(new ExtractoViewModel());
    }

    /// <summary>
    ///     Endpoint para solicitar la creación del pdf
    /// </summary>
    /// <param name="model">Modelo con la información de validación</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
            if (r.IsSuccess)
                return File(r.Value!, "application/pdf", "Extracto.pdf");


            ModelState.AddModelError("", "No se pudo generar el extracto");
            return NotFound();
        }

        return View(model);
    }
}