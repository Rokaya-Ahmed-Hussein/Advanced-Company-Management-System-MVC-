using BL.ViewModels.CompanyAuditLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.CompanyAuditLogManager
{
    public interface ICompanyAuditLogManager
    {
        List<CompanyAuditLogReadVM> GetAllCompanyAuditLog();
        CompanyAuditLogReadVM? GetCompanyAuditLogById(int id);
        public CompanyAuditLogReadVM AddCompanyAuditLog(string username, string operationType, int companyId, string newValues);



    }
}
