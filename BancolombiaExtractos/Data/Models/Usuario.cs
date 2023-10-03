using System;
using System.Collections.Generic;

namespace BancolombiaExtractos.Data.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Titular { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();
}
