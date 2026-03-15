using System;

namespace BL.ViewModels.Drug
{
    public class DrugDataReadVM
    {
        public int TradeCode { get; set; } // int in DB
        public string? Trade_name { get; set; }
        public string? DrugTypeName { get; set; } // Bio or Human Pharmaceutical
        public string? Applicant { get; set; } // Company Name
        public string? CompAName { get; set; } // Company Arabic Name
        public int? CompKey { get; set; }
        public DateTime? Drug_registration_date { get; set; }
        public DateTime? Drug_registration_expiration_date { get; set; }
        public string? Dosage_form { get; set; }
        public decimal? Strength_value { get; set; } // decimal in DB
        public string? StrengthUnit { get; set; }
        public string? LicStatus { get; set; }
        public string? regstatus { get; set; }
    }
}

