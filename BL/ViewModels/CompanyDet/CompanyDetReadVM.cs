﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ViewModels.CompanyDet
{
    public class CompanyDetReadVM
    {
        public int CompanyDetId { get; set; }
        public string? CompName { get; set; }
        public string? CompTypeId { get; set; }
        public string? CommRegNo { get; set; }
        public DateTime? CommRegIssuDate { get; set; }
        public DateTime? CommRegExpireDate { get; set; }
        public string? TaxCardNo { get; set; }
        public DateTime? TaxCardIssuDate { get; set; }
        public DateTime? TaxCardExpireDate { get; set; }
    }
}
