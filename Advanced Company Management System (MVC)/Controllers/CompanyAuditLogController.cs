using BL.Managers.CompanyAuditLogManager;
using DTO.Data.Models;
using BL.ViewModels.CompanyAuditLog;
using Microsoft.AspNetCore.Mvc;

namespace Advanced_Company_Management_System__MVC_.Controllers
{
    public class CompanyAuditLogController : Controller
    {
        private readonly ICompanyAuditLogManager AuditLogManag;

        public CompanyAuditLogController(ICompanyAuditLogManager Manager)
        {
            AuditLogManag = Manager;
        }

        #region Defoult Viwe
        //CompanyAuditLog/Index
        public IActionResult Index()
        {
            return View();
        }

        //CompanyAuditLog/Index2
        public ActionResult Index2()
        {
            return Content("Controller works!");
        }
        #endregion

        #region Get All Audit Log
        [HttpGet]
        //CompanyAuditLog/GetAll
        public ActionResult<IEnumerable<CompanyAuditLogReadVM>> GetAll()
        {
            var AllAuditData = AuditLogManag.GetAllCompanyAuditLog();
            return View(AllAuditData);
        }
        #endregion

        #region Get Audit By Id
        [HttpGet]
        //CompanyAuditLog/GetById/Id
        public ActionResult<CompanyAuditLogReadVM> GetById(int id)
        {
            var AuditLogData = AuditLogManag.GetCompanyAuditLogById(id);
            return View(AuditLogData);
        }
        #endregion
        
    }
}
