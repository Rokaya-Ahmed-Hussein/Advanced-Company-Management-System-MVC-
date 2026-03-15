using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DTO.Data.Models;

[PrimaryKey("UserId", "RoleId")]
[Table("UserRole")]
public partial class UserRole
{
    [Key]
    public int UserId { get; set; }

    [Key]
    public int RoleId { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserRoles")]
    public virtual User User { get; set; } = null!;
}
