﻿﻿using AutoMapper;
using BL.ViewModels.CompanyAuditLog;
using BL.ViewModels.CompanyDet;
using BL.ViewModels.User;
using BL.ViewModels.Drug;
using DTO.Data.Models;

namespace BL.AutoMapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() 
        {
            #region Read Data From DB
            CreateMap<CompanyDet ,CompanyDetReadVM>();
            CreateMap<CompanyAuditLog ,CompanyAuditLogReadVM>();
            CreateMap<User, AccountReadVM>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(r => r.Name).ToList()));
            CreateMap<GetDrugsData, DrugDataReadVM>();
            #endregion

            #region Add Data to DB
            CreateMap<CompanyDetAddVM ,CompanyDet>();
            CreateMap<CompanyAuditLogAddVM ,CompanyAuditLog>();
            CreateMap<RegisterVM ,User>();
            #endregion

            #region Update Data in DB
            CreateMap<CompanyDetUpdateVM ,CompanyDet>();
            CreateMap<CompanyAuditLogUpdateVM ,CompanyAuditLog>();
            CreateMap<UpdateUserVM ,User>();
            CreateMap<LoginVM ,User>();
            #endregion
        }
    }
}
