using System;
using System.Collections.Generic;

namespace BancolombiaExtractos.Data.Models;

public partial class Movimiento
{
    public int Id { get; set; }

    public decimal Valor { get; set; }

    public int? NumeroCuenta { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Cuenta? NumeroCuentaNavigation { get; set; }
}
