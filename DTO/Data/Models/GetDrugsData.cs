using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DTO.Data.Models
{
    /// <summary>
    /// Model for GetDrugsData view - consolidated drug and company information
    /// Mapped to exact database schema
    /// </summary>
    [Keyless]
    public class GetDrugsData
    {
        // TradeCode is INT (primary identifier)
        public int TradeCode { get; set; }
        
        public string? Drug_license_number { get; set; }
        public DateTime? Drug_registration_date { get; set; }
        public DateTime? Drug_registration_expiration_date { get; set; }
        public string Trade_name { get; set; } = null!; // NOT NULL in DB
        
        // Strength_value is DECIMAL in DB
        public decimal? Strength_value { get; set; }
        
        public int? ShelfLife { get; set; }
        public int? RegUnderReg { get; set; }
        public string? LicStatus { get; set; }
        public string? StrengthUnit { get; set; }
        public string? Dosage_form { get; set; }
        public string? Pack_unit { get; set; }
        public int? RegFlag { get; set; }
        public int? LicFlag { get; set; }
        public int? LicStatusKey { get; set; }
        public int? StrengthKey { get; set; }
        public int? DosageKey { get; set; }
        public int? PackKey { get; set; }
        public string? Generics { get; set; }
        public int? DrugType { get; set; }
        
        // Company Information
        public string? Applicant { get; set; }
        public int? CompKey { get; set; }
        public string? CompAName { get; set; }
        
        // Drug Type Name - KEY FIELD FOR FILTERING (Bio/Human Pharmaceutical)
        public string? DrugTypeName { get; set; }
        
        public string? Strength_unit { get; set; }
        public int? Sys_Key { get; set; }
        
        // tradecodelink is INT in DB
        public int? tradecodelink { get; set; }
        
        public string regstatus { get; set; } = null!; // NOT NULL in DB
        public string? Storecharac { get; set; }
        public DateTime? stabilitycomdate { get; set; }
        public string? physicalcharac { get; set; }
        public int? iscombopack { get; set; }
        public int? Expired { get; set; }
        
        // ApprovedPacks is VARCHAR in DB
        public string? ApprovedPacks { get; set; }
        
        public int? BoxRequestid { get; set; }
        public int? IsInnovator { get; set; }
    }
}

