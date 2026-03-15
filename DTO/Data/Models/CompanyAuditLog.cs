using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DTO.Data.Models;

[Table("CompanyAuditLog")]
public partial class CompanyAuditLog
{
    [Key]
    public int AuditId { get; set; }

    public int CompanyId { get; set; }

    [StringLength(50)]
    public string OperationType { get; set; } = null!;

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    [StringLength(100)]
    public string PerformedBy { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime PerformedDate { get; set; }
}
