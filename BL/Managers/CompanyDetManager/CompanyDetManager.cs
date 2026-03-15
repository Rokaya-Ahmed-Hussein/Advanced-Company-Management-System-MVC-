﻿using AutoMapper;
using BL.Managers.CompanyAuditLogManager;
using BL.ViewModels.CompanyAuditLog;
using BL.ViewModels.CompanyDet;
using DTO.Data.Models;
using DTO.Repositories.CompanyAuditLogRepository;
using DTO.Repositories.CompanyDetRepository;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.CompanyDetManager
{
    public class CompanyDetManager: ICompanyDetManager
    { 
        private readonly ICompanyDetRepository CompDetRepo;
        private readonly IMapper Mappp;
        private readonly ICompanyAuditLogManager AuditDataRepo;
   

        public CompanyDetManager(ICompanyDetRepository CompanDet ,IMapper mapper ,ICompanyAuditLogManager AuditData )
        {
            CompDetRepo = CompanDet;
            Mappp = mapper;
            AuditDataRepo = AuditData;
        }
        #region GetAllCompanyDet
        public List<CompanyDetReadVM> GetAllCompanyDet()
        {
            var AllCompDet = CompDetRepo.GetAllEntities().Where(x => x.IsDeleted == false).ToList();
            return Mappp.Map<List<CompanyDetReadVM>>(AllCompDet);
        }
        #endregion

        #region GetCompanyDetById
        public CompanyDetReadVM GetCompanyDetById (int id)
        {
            var CompData = CompDetRepo.GetEntityById(id);
            if (CompData == null || CompData.IsDeleted == true)
                return null;
            return Mappp.Map<CompanyDetReadVM>(CompData);
        }
        #endregion

        #region AddCompDet
        public CompanyDetReadVM AddCompDet (CompanyDetAddVM NewDataCompDet, string username)
        {
            var NewCompDet = Mappp.Map<CompanyDet>(NewDataCompDet);
            CompDetRepo.Add(NewCompDet);
            CompDetRepo.SaveChanges();

            AuditDataRepo.AddCompanyAuditLog(username, "Create" ,NewCompDet.CompanyDetId, NewCompDet.CompName);

            return Mappp.Map<CompanyDetReadVM>(NewCompDet);

        }
        #endregion
        
        #region UpdateCompDet
        public bool UpdateCompDet(CompanyDetUpdateVM UpdatedData, string username)
        {
            var OldCompData = CompDetRepo.GetEntityById(UpdatedData.CompanyDetId);
            if (OldCompData == null || OldCompData.IsDeleted == true)
                return false;
            Mappp.Map(UpdatedData, OldCompData);
            CompDetRepo.Update(OldCompData);
            CompDetRepo.SaveChanges();

            AuditDataRepo.AddCompanyAuditLog(username, "Update", OldCompData.CompanyDetId, OldCompData.CompName);

            return true;
        }
        #endregion

        #region SoftDeleteCompDet
        public void SoftDeleteCompDet(int id, string username)
        {
            var DeleteCompDet = CompDetRepo.GetEntityById(id);
            if (DeleteCompDet == null || DeleteCompDet.IsDeleted == true) 
                return;
            DeleteCompDet.IsDeleted = true;
            DeleteCompDet.DeletedBy = username;
            DeleteCompDet.DeletedDate = DateTime.Now;
            
            CompDetRepo.Update(DeleteCompDet);
            CompDetRepo.SaveChanges();

            AuditDataRepo.AddCompanyAuditLog(username, "Delete", DeleteCompDet.CompanyDetId, DeleteCompDet.CompName);

        }
        #endregion

        #region Filter, Search & Pagination
        public CompanyFilterVM GetFilteredCompanies(string companyType, string searchTerm, int pageNumber, int pageSize)
        {
            // Start with non-deleted companies
            var query = CompDetRepo.GetAllEntities().Where(x => x.IsDeleted == false);

            // Apply expiry status filter
            if (!string.IsNullOrEmpty(companyType) && companyType != "All")
            {
                var currentDate = DateTime.Now;
                var threeMonthsFromNow = currentDate.AddMonths(3);

                switch (companyType)
                {
                    case "Expired":
                        // Companies where registration has expired
                        query = query.Where(c => c.CommRegExpireDate.HasValue && c.CommRegExpireDate.Value < currentDate);
                        break;
                    case "Active":
                        // Companies where registration is still valid (not expired)
                        query = query.Where(c => !c.CommRegExpireDate.HasValue || c.CommRegExpireDate.Value >= currentDate);
                        break;
                    case "ExpiringSoon":
                        // Companies expiring within the next 3 months
                        query = query.Where(c => c.CommRegExpireDate.HasValue && 
                                               c.CommRegExpireDate.Value >= currentDate && 
                                               c.CommRegExpireDate.Value <= threeMonthsFromNow);
                        break;
                }
            }

            // Apply search filter (search in company name)
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(c => c.CompName != null && 
                                        c.CompName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            // Get total count before pagination
            var totalRecords = query.Count();

            // Apply pagination - only fetch required page (sorted by ID descending - newest first)
            var companies = query
                .OrderByDescending(c => c.CompanyDetId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Map to ViewModels
            var companyVMs = Mappp.Map<List<CompanyDetReadVM>>(companies);

            // Return filter result
            return new CompanyFilterVM
            {
                CompanyType = companyType ?? "All",
                SearchTerm = searchTerm ?? string.Empty,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                Companies = companyVMs
            };
        }
        #endregion
        
        #region Excel Sheet
        public byte[] ExportCompaniesToExcel(IEnumerable<CompanyDetReadVM> companies)
        {
            // Use EPPlus to create Excel file
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Companies");
            
            // Header Row
            worksheet.Cells[1, 1].Value = "Company Name";
            worksheet.Cells[1, 2].Value = "Company Type Id";
            worksheet.Cells[1, 3].Value = "Comm Reg Issu Date";
            worksheet.Cells[1, 4].Value = "Comm Reg Expire Date";
            worksheet.Cells[1, 5].Value = "Tax Card No";
            worksheet.Cells[1, 6].Value = "Tax Card Expire Date";
            
            // Style header row
            using (var range = worksheet.Cells[1, 1, 1, 6])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                // Using ARGB bytes instead of System.Drawing.Color for macOS compatibility (Light Blue: #ADD8E6)
                range.Style.Fill.BackgroundColor.SetColor(255, 173, 216, 230); // Alpha=255, Red=173, Green=216, Blue=230
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            }
            
            // Data Rows
            int row = 2;
            foreach (var company in companies)
            {
                worksheet.Cells[row, 1].Value = company.CompName;
                worksheet.Cells[row, 2].Value = company.CompTypeId;
                worksheet.Cells[row, 3].Value = company.CommRegIssuDate?.ToString("yyyy-MM-dd");
                worksheet.Cells[row, 4].Value = company.CommRegExpireDate?.ToString("yyyy-MM-dd");
                worksheet.Cells[row, 5].Value = company.TaxCardNo;
                worksheet.Cells[row, 6].Value = company.TaxCardExpireDate?.ToString("yyyy-MM-dd");
                row++;
            }
            
            // Manually set column widths instead of AutoFitColumns (macOS compatible)
            worksheet.Column(1).Width = 30; // Company Name
            worksheet.Column(2).Width = 18; // Company Type Id
            worksheet.Column(3).Width = 20; // Comm Reg Issu Date
            worksheet.Column(4).Width = 22; // Comm Reg Expire Date
            worksheet.Column(5).Width = 15; // Tax Card No
            worksheet.Column(6).Width = 22; // Tax Card Expire Date
            
            // Return Excel as byte array
            return package.GetAsByteArray();
        }

        #endregion
    }
    
}
