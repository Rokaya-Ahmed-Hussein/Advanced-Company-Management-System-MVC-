﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ViewModels.CompanyDet
{
    public class CompanyDetAddVM
    {
        [Required(ErrorMessage = "Company Name is required")]
        [Display(Name = "Company Name")]
        public string? CompName { get; set; }

        [Display(Name = "Company Type")]
        public string? CompTypeId { get; set; }

        [Display(Name = "Commercial Registration Number")]
        public string? CommRegNo { get; set; }

        [Display(Name = "Commercial Registration Issue Date")]
        [DataType(DataType.Date)]
        public DateTime? CommRegIssuDate { get; set; }

        [Display(Name = "Commercial Registration Expire Date")]
        [DataType(DataType.Date)]
        public DateTime? CommRegExpireDate { get; set; }

        [Display(Name = "Tax Card Number")]
        public string? TaxCardNo { get; set; }

        [Display(Name = "Tax Card Issue Date")]
        [DataType(DataType.Date)]
        public DateTime? TaxCardIssuDate { get; set; }

        [Display(Name = "Tax Card Expire Date")]
        [DataType(DataType.Date)]
        public DateTime? TaxCardExpireDate { get; set; }
    }
}
