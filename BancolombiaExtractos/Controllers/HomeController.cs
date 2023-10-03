using BancolombiaExtractos.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BancolombiaExtractos.Controllers;

[ApiController]
[Route("[controller]")]
[Route("/")]
public class HomeController : Controller
{
    /// <summary>
    ///     Returns the Index view
    /// </summary>
    /// <returns>sdsdsdsdsdsdsd</returns>
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    ///     Regresa un extracto
    /// </summary>
    /// <param name="extractoViewModel">El ViewModel para realizar la solicitud</param>
    [HttpGet("extracto")]
    public IActionResult Extracto([FromForm] ExtractoViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return View("Index", viewModel) 

        return Ok("Hello");
    }
}