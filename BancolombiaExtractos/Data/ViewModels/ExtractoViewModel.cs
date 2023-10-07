using System.ComponentModel.DataAnnotations;

namespace BancolombiaExtractos.Data.ViewModels;

/// <summary>
/// ViewModel (Modelo-Vista) para encapsular la información del correo y id de la cuenta del usuario para enviarla a la vista
/// </summary>
public class ExtractoViewModel
{
    [Required(ErrorMessage = "Se debe ingresar un correo")]
    [EmailAddress(ErrorMessage = "Se debe ingresar un correo valido")]
    public string Email { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Ingrese un numero de cuenta valido")]
    public int Account { get; set; }
}