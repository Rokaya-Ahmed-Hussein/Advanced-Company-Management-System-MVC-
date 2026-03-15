using System;
using System.Collections.Generic;

namespace BL.ViewModels.Drug
{
    public class DrugFilterVM
    {
        public string DrugType { get; set; } = "Human Pharmaceutical"; // Default
        public string SearchTerm { get; set; } = string.Empty;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalRecords { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
        public List<DrugDataReadVM> Drugs { get; set; } = new List<DrugDataReadVM>();
        
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}

