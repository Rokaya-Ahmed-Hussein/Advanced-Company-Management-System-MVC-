﻿using BL.ViewModels.CompanyDet;
using DTO.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.CompanyDetManager
{
    public interface ICompanyDetManager
    {
        List<CompanyDetReadVM> GetAllCompanyDet();
        CompanyDetReadVM GetCompanyDetById(int id);
        CompanyDetReadVM AddCompDet(CompanyDetAddVM NewCompDet, string username);
        bool UpdateCompDet(CompanyDetUpdateVM NewDataCompDet, string username);
        void SoftDeleteCompDet(int id, string username);
        byte[] ExportCompaniesToExcel(IEnumerable<CompanyDetReadVM> companies);

        // Filtering, Search, and Pagination
        CompanyFilterVM GetFilteredCompanies(string companyType, string searchTerm, int pageNumber, int pageSize);
    }
}
