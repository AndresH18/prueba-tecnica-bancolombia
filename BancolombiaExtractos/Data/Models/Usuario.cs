using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BancolombiaExtractos.Data.Models;

[Table("usuarios")]
[Index("Email", Name = "UQ__usuarios__AB6E6164FD35CE4C", IsUnique = true)]
public partial class Usuario
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("titular")]
    [StringLength(100)]
    [Unicode(false)]
    public string Titular { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [InverseProperty("Usuario")]
    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();
}
