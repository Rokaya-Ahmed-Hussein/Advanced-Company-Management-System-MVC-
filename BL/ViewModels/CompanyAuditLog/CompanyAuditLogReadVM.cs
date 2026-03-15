using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ViewModels.CompanyAuditLog
{
    public class CompanyAuditLogReadVM
    {
        public int AuditId { get; set; }
        public int CompanyId { get; set; }
        public string OperationType { get; set; } = null!;
        public string PerformedBy { get; set; } = null!;
        public DateTime PerformedDate { get; set; }
    }
}
