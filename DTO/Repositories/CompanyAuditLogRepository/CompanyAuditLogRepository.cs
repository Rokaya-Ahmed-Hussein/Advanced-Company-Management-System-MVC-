using DTO.Data.Cnotext;
using DTO.Data.Models;
using DTO.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Repositories.CompanyAuditLogRepository
{
    public class CompanyAuditLogRepository:GenericRepository<CompanyAuditLog> ,ICompanyAuditLogRepository
    {
        private readonly EddbAppContext eddbAuditCon;
        public CompanyAuditLogRepository(EddbAppContext eddbAppCont) : base(eddbAppCont)
        {
            eddbAuditCon = eddbAppCont;

        }



    }
    }
