using BL.ViewModels.Drug;

namespace BL.Managers.DrugDataManager
{
    public interface IDrugDataManager
    {
        DrugFilterVM GetFilteredDrugs(string drugType, string searchTerm, int pageNumber, int pageSize);
    }
}

