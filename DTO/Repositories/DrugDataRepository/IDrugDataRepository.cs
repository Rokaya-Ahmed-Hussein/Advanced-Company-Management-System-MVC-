using DTO.Data.Models;
using System.Linq;

namespace DTO.Repositories.DrugDataRepository
{
    public interface IDrugDataRepository
    {
        IQueryable<GetDrugsData> GetAllDrugsData();
        IQueryable<GetDrugsData> GetFilteredDrugsData(string drugType, string searchTerm);
    }
}

