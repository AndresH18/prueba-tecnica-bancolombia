using System.ComponentModel.DataAnnotations;

namespace BancolombiaExtractos.Data.ViewModels;

public class ExtractoViewModel
{
    [EmailAddress] public string Email { get; set; } = string.Empty;
    [Range(1, int.MaxValue)] public int Account { get; set; } 
}