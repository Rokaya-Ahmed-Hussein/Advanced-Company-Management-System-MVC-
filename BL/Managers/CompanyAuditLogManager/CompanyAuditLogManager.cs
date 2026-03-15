using AutoMapper;
using BL.ViewModels.CompanyAuditLog;
using DTO.Data.Models;
using DTO.Repositories.CompanyAuditLogRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.CompanyAuditLogManager
{
    public class CompanyAuditLogManager:ICompanyAuditLogManager
    {
        private readonly ICompanyAuditLogRepository CompAUditRep;
        
        private readonly IMapper Mapper;
        public CompanyAuditLogManager(ICompanyAuditLogRepository AuditRepo , IMapper Mapp)
        { 
            CompAUditRep = AuditRepo;
            Mapper = Mapp;
        }


        public List<CompanyAuditLogReadVM> GetAllCompanyAuditLog()
        {
            var AllUditLog = CompAUditRep.GetAllEntities();
            return Mapper.Map<List<CompanyAuditLogReadVM>>(AllUditLog);
        }

        public CompanyAuditLogReadVM GetCompanyAuditLogById (int Auditid)
        {
            var compauditlog = CompAUditRep.GetEntityById(Auditid);
            if (compauditlog == null)
                return null;
            return Mapper.Map<CompanyAuditLogReadVM>(compauditlog);
        }

        public CompanyAuditLogReadVM AddCompanyAuditLog(string username, string operationType, int companyId, string newValues)
        {
            var auditLog = new CompanyAuditLogAddVM
            {
                PerformedBy = username,
                OperationType = operationType,
                CompanyId = companyId,
                PerformedDate = DateTime.Now,
                NewValues = newValues
            };

            var AuditLogData = Mapper.Map<CompanyAuditLog>(auditLog);

            CompAUditRep.Add(AuditLogData);
            CompAUditRep.SaveChanges();

            return Mapper.Map<CompanyAuditLogReadVM>(AuditLogData);
        }





    }
}
