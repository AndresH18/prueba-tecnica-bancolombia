using System;
using System.Collections.Generic;

namespace BancolombiaExtractos.Data.Models;

public partial class Cuenta
{
    public int NumeroCuenta { get; set; }

    public int UsuarioId { get; set; }

    public string TipoCuenta { get; set; } = null!;

    public decimal? Saldo { get; set; }

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    public virtual Usuario Usuario { get; set; } = null!;
}
