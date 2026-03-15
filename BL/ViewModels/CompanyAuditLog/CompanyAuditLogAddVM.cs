using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ViewModels.CompanyAuditLog
{
    public class CompanyAuditLogAddVM
    {
        public int CompanyId { get; set; }
        public string OperationType { get; set; } = null!;
        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
        public string PerformedBy { get; set; } = null!;
        public DateTime PerformedDate { get; set; }
    }
}
