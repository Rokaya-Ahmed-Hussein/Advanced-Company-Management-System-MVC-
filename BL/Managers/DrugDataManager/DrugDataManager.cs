using AutoMapper;
using BL.ViewModels.Drug;
using DTO.Repositories.DrugDataRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Managers.DrugDataManager
{
    public class DrugDataManager : IDrugDataManager
    {
        private readonly IDrugDataRepository _drugDataRepo;
        private readonly IMapper _mapper;

        public DrugDataManager(IDrugDataRepository drugDataRepo, IMapper mapper)
        {
            _drugDataRepo = drugDataRepo;
            _mapper = mapper;
        }

        public DrugFilterVM GetFilteredDrugs(string drugType, string searchTerm, int pageNumber, int pageSize)
        {
            // Get filtered query
            var query = _drugDataRepo.GetFilteredDrugsData(drugType, searchTerm);

            // Get total count before pagination
            var totalRecords = query.Count();

            // Apply pagination - Server-Side (only fetch required records)
            var drugs = query
                .OrderBy(d => d.Trade_name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Map to ViewModels
            var drugVMs = _mapper.Map<List<DrugDataReadVM>>(drugs);

            var result = new DrugFilterVM
            {
                DrugType = drugType ?? "Human Pharmaceutical",
                SearchTerm = searchTerm ?? string.Empty,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                Drugs = drugVMs
            };
          
            return result;
        }
    }
}

