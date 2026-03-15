using BL.Managers.DrugDataManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Advanced_Company_Management_System__MVC_.Controllers
{
    [Authorize]
    public class DrugController : Controller
    {
        private readonly IDrugDataManager _drugManager;

        public DrugController(IDrugDataManager drugManager)
        {
            _drugManager = drugManager;
        }

        /// <summary>
        /// Main drug listing page with filtering, search, and pagination
        /// Data consumed from GetDrugsData view
        /// </summary>
        [HttpGet]
        public IActionResult Index(string drugType = "Human Pharmaceutical", string searchTerm = "", int page = 1, int pageSize = 10)
        {
            
            var result = _drugManager.GetFilteredDrugs(drugType, searchTerm, page, pageSize);
            
            return View(result);
        }

        /// <summary>
        /// AJAX endpoint for real-time server-side search
        /// Compatible with radio button filter and pagination
        /// </summary>
        [HttpGet]
        public IActionResult SearchDrugs(string drugType = "Human Pharmaceutical", string searchTerm = "", int page = 1, int pageSize = 10)
        {
          var result = _drugManager.GetFilteredDrugs(drugType, searchTerm, page, pageSize);
            
            return PartialView("_DrugListPartial", result);
        }
    }
}

