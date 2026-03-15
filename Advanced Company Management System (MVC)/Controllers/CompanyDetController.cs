﻿using BL.Managers.CompanyDetManager;
using BL.ViewModels.CompanyDet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Advanced_Company_Management_System__MVC_.Controllers
{
    [Authorize]
    public class CompanyDetController : Controller
    {
        private readonly ICompanyDetManager CompDetManag;
        
        public CompanyDetController(ICompanyDetManager compdetManager)
        {
            CompDetManag = compdetManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Get All Companies with Filtering, Search, Pagination
        [HttpGet]
        //CompanyDet/GetAllcomp
        public ActionResult GetAllcomp(string companyType = "All", string searchTerm = "", int page = 1, int pageSize = 10)
        {
            var filterResult = CompDetManag.GetFilteredCompanies(companyType, searchTerm, page, pageSize);
            return View(filterResult);
        }

        // AJAX endpoint for real-time search
        [HttpGet]
        public IActionResult SearchCompanies(string companyType = "All", string searchTerm = "", int page = 1, int pageSize = 10)
        {
            var filterResult = CompDetManag.GetFilteredCompanies(companyType, searchTerm, page, pageSize);
            return PartialView("_CompanyListPartial", filterResult);
        }
        #endregion

        #region Get Company By Id
        
        [HttpGet]
        //CompanyDet/GetCompById/Id
        public ActionResult<CompanyDetReadVM> GetCompById(int id)
        {
            var CompanyData = CompDetManag.GetCompanyDetById(id);
            return View(CompanyData);
        }
        #endregion

        #region Add Company
        //CompanyDet/ViewOAdd

        [HttpGet]
        public IActionResult ViewOAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveNew(CompanyDetAddVM DataModel)
        {
            if(DataModel.CompName == null)
            {
                return View("ViewOAdd", DataModel);
            }
            
            // Get the logged-in username
            var username = User.Identity?.Name ?? "Unknown";
            
            CompDetManag.AddCompDet(DataModel, username);
            return RedirectToAction("GetAllcomp");
        }

        #endregion

        #region update
        [HttpGet]
        public ActionResult UpdateCompDet (int id)
        {
            var company = CompDetManag.GetCompanyDetById(id);

            if (company == null)
                return NotFound();

            var updateVM = new CompanyDetUpdateVM
            {
                CompanyDetId = company.CompanyDetId,
                CompName = company.CompName,
                CompTypeId = company.CompTypeId,
                CommRegNo = company.CommRegNo,
                CommRegIssuDate = company.CommRegIssuDate,
                CommRegExpireDate = company.CommRegExpireDate,
                TaxCardNo = company.TaxCardNo,
                TaxCardIssuDate = company.TaxCardIssuDate,
                TaxCardExpireDate = company.TaxCardExpireDate
            };

            return View(updateVM);

        }

        [HttpPost]
        public ActionResult SaveUpadtes(CompanyDetUpdateVM DataModel) 
        {
            // Get the logged-in username
            var username = User.Identity?.Name ?? "Unknown";
            
            CompDetManag.UpdateCompDet(DataModel, username);
            return RedirectToAction("GetAllcomp");
        }
            
        #endregion

        #region Delete Company
        [HttpPost]
        public IActionResult Delete(int id) 
        {
            // Check if user has Admin role
            if (!User.IsInRole("Admin"))
            {
                TempData["ErrorMessage"] = "Access Denied: Only administrators can delete companies.";
                return RedirectToAction("GetAllcomp");
            }

            // Get the logged-in username
            var username = User.Identity?.Name ?? "Unknown";
            
            CompDetManag.SoftDeleteCompDet(id, username);
            TempData["SuccessMessage"] = "Company deleted successfully.";
            return RedirectToAction("GetAllcomp");
        }

        // AJAX endpoint for soft delete with role check
        [HttpPost]
        public IActionResult SoftDelete(int id)
        {
            try
            {
                if (!User.IsInRole("Admin"))
                {
                    return Json(new 
                    { 
                        success = false, 
                        message = "Access Denied",
                        details = "Only administrators can delete companies. Please contact your system administrator if you need to delete this company.",
                        isUnauthorized = true
                    });
                }

                // Get the logged-in username
                var username = User.Identity?.Name ?? "Unknown";
                
                CompDetManag.SoftDeleteCompDet(id, username);
                
                return Ok(new 
                { 
                    success = true, 
                    message = "Company deleted successfully",
                    isUnauthorized = false
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new 
                { 
                    success = false, 
                    message = "An error occurred while deleting the company",
                    details = ex.Message,
                    isUnauthorized = false
                });
            }
        }

        #endregion

        #region Export Excel Sheet
        [HttpGet]
        public IActionResult Export()
        {
            var companies = CompDetManag.GetAllCompanyDet();

            var bytes = CompDetManag.ExportCompaniesToExcel(companies);

            return File(
                bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Companies.xlsx"
            );
        }

        #endregion
    }
}
