using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ViewModels.CompanyDet
{
    public class CompanyFilterVM
    {
        public string CompanyType { get; set; } = "All"; // "All", "Human Pharmaceutical", "Bio"
        public string SearchTerm { get; set; } = string.Empty;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalRecords { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
        public List<CompanyDetReadVM> Companies { get; set; } = new List<CompanyDetReadVM>();
        
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}

