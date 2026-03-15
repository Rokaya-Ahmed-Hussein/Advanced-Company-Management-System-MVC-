using DTO.Data.Cnotext;
using DTO.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DTO.Repositories.DrugDataRepository
{
    public class DrugDataRepository : IDrugDataRepository
    {
        private readonly EddbAppContext _context;

        public DrugDataRepository(EddbAppContext context)
        {
            _context = context;
        }

        public IQueryable<GetDrugsData> GetAllDrugsData()
        {
            return _context.GetDrugsDataView.AsQueryable();
        }

        public IQueryable<GetDrugsData> GetFilteredDrugsData(string drugType, string searchTerm)
        {
           
            var query = _context.GetDrugsDataView.AsQueryable();
            var totalBeforeFilter = query.Count();
          
            // Filter by DrugTypeName (Bio or Human Pharmaceutical)
            if (!string.IsNullOrEmpty(drugType) && drugType != "All")
            {
                query = query.Where(d => d.DrugTypeName == drugType);
                var afterTypeFilter = query.Count();
            }

            // Search filter (search in drug name, company name, or trade code)
            if (!string.IsNullOrEmpty(searchTerm))
            {  
                // Try to parse search term as integer for TradeCode search
                bool isNumericSearch = int.TryParse(searchTerm, out int tradeCodeSearch);
                
                // Use case-insensitive search
                var searchPattern = $"%{searchTerm}%"; 
                query = query.Where(d =>
                    (d.Trade_name != null && EF.Functions.Like(d.Trade_name, searchPattern)) ||
                    (d.Applicant != null && EF.Functions.Like(d.Applicant, searchPattern)) ||
                    (isNumericSearch && d.TradeCode == tradeCodeSearch)
                );
                
                var afterSearchFilter = query.Count();
             
            }

            return query;
        }
    }
}

