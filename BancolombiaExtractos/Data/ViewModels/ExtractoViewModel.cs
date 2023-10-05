using System.ComponentModel.DataAnnotations;

namespace BancolombiaExtractos.Data.ViewModels;

public class ExtractoViewModel
{
    [EmailAddress(ErrorMessage = "Se debe ingresar un correo")]
    public string Email { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Ingrese un numero de cuenta valido")]
    public int Account { get; set; }
}