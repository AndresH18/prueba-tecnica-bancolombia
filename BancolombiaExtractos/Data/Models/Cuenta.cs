using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BancolombiaExtractos.Data.Models;

[Table("cuentas")]
public partial class Cuenta
{
    [Key]
    [Column("numero_cuenta")]
    public int NumeroCuenta { get; set; }

    [Column("usuario_id")]
    public int UsuarioId { get; set; }

    [Column("tipo_cuenta")]
    [StringLength(50)]
    [Unicode(false)]
    public string TipoCuenta { get; set; } = null!;

    [Column("saldo", TypeName = "decimal(18, 2)")]
    public decimal? Saldo { get; set; }

    [InverseProperty("NumeroCuentaNavigation")]
    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    [ForeignKey("UsuarioId")]
    [InverseProperty("Cuenta")]
    public virtual Usuario Usuario { get; set; } = null!;
}
