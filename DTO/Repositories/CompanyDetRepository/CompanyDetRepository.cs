using DTO.Data.Cnotext;
using DTO.Data.Models;
using DTO.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Repositories.CompanyDetRepository
{
    public class CompanyDetRepository:GenericRepository<CompanyDet> ,ICompanyDetRepository 
    {
        private readonly EddbAppContext companyDetcon; 
        
        public CompanyDetRepository(EddbAppContext eddbAppCont) :base(eddbAppCont)
        {
            companyDetcon = eddbAppCont;
            
        }

    }

}
