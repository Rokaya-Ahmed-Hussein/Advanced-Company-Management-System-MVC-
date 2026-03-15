using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DTO.Data.Models;

public partial class DrugType
{
    [Key]
    [Column("Sys_Key")]
    public int SysKey { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Type { get; set; }
}
