using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BancolombiaExtractos.Data.Models;

[Table("movimientos")]
public partial class Movimiento
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("valor", TypeName = "decimal(18, 2)")]
    public decimal Valor { get; set; }

    [Column("numero_cuenta")]
    public int? NumeroCuenta { get; set; }

    [Column("fecha")]
    public DateTime Fecha { get; set; }

    [ForeignKey("NumeroCuenta")]
    [InverseProperty("Movimientos")]
    public virtual Cuenta? NumeroCuentaNavigation { get; set; }
}
