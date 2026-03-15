using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DTO.Data.Models;

[Table("CompanyDet")]
public partial class CompanyDet
{
    [Key]
    [Column("CompanyDetID")]
    public int CompanyDetId { get; set; }
    
    [Column("CompName")]
    [StringLength(500)]
    [Unicode(false)]
    public string? CompName { get; set; }

    [Column("CompAName")]
    [StringLength(500)]
    [Unicode(false)]
    public string? CompAname { get; set; }

    [Column("CompTypeID")]
    [StringLength(50)]
    [Unicode(false)]
    public string? CompTypeId { get; set; }

    [StringLength(500)]
    public string? CommRegNo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CommRegIssuDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CommRegExpireDate { get; set; }

    [StringLength(500)]
    public string? TaxCardNo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TaxCardIssuDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TaxCardExpireDate { get; set; }

    [StringLength(500)]
    public string? IndusRegNo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? IndusRegIssuDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? IndusRegExpireDate { get; set; }

    public bool? CompIsSameAddress { get; set; }

    [Column("CompGovID")]
    [StringLength(50)]
    [Unicode(false)]
    public string? CompGovId { get; set; }

    [Column("CompCityID")]
    [StringLength(50)]
    [Unicode(false)]
    public string? CompCityId { get; set; }

    [StringLength(500)]
    public string? CompStreet { get; set; }

    [StringLength(500)]
    public string? CompBuilding { get; set; }

    [StringLength(500)]
    public string? CompFloor { get; set; }

    [StringLength(500)]
    public string? CompAppartment { get; set; }

    [StringLength(500)]
    public string? LawNo { get; set; }

    [StringLength(500)]
    public string? LawDetails { get; set; }

    [Column("License_type_code")]
    [StringLength(50)]
    [Unicode(false)]
    public string? LicenseTypeCode { get; set; }

    [StringLength(50)]
    public string? UserName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UserPassword { get; set; }

    [Column("License_facility")]
    public bool? LicenseFacility { get; set; }

    [Column("Under_Constraction")]
    public bool? UnderConstraction { get; set; }

    [Column("isactive")]
    public bool? Isactive { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Decuserpassword { get; set; }

    [StringLength(50)]
    public string? UserNameOld { get; set; }

    public bool? Validated { get; set; }

    [Column("mergedwithcomp")]
    public int? Mergedwithcomp { get; set; }

    public int? UserDelivery { get; set; }

    [Column("block")]
    public int? Block { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }

    [Column("blockcomp")]
    public int? Blockcomp { get; set; }

    [Column("blockmanf")]
    public int? Blockmanf { get; set; }

    [Column("blockprodline")]
    public int? Blockprodline { get; set; }

    [Column("blockemp")]
    public int? Blockemp { get; set; }

    [Column("blockattach")]
    public int? Blockattach { get; set; }

    [Column("blockletter")]
    public int? Blockletter { get; set; }

    [Column("compname_old")]
    [StringLength(500)]
    [Unicode(false)]
    public string? CompnameOld { get; set; }

    public int? BlockReport { get; set; }

    [Column("ispharmacist")]
    public int? Ispharmacist { get; set; }

    public int? MergReason { get; set; }

    [Column("Deactivate_Reasone")]
    public int? DeactivateReasone { get; set; }

    [Column("Deactivate_Date", TypeName = "datetime")]
    public DateTime? DeactivateDate { get; set; }

    public int? IsAdmin { get; set; }

    [Column("BlocKCosmatic")]
    public int? BlocKcosmatic { get; set; }

    public int? BlockDrugs { get; set; }

    public int? DeattivateReason { get; set; }

    public int? ViewOption { get; set; }

    [Column("isparent")]
    public int? Isparent { get; set; }

    [Column("ismain")]
    public int? Ismain { get; set; }

    public int? TollCardNo { get; set; }

    public int? CommRegType { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? InvAddress { get; set; }

    [Column("blockadministrative")]
    public int? Blockadministrative { get; set; }

    [Column("blockcompreports")]
    public int? Blockcompreports { get; set; }

    [Column("blockothers")]
    public int? Blockothers { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? UserPassword1 { get; set; }

    [Column("Revision_status")]
    public int? RevisionStatus { get; set; }

    [Column("Revision_Comment")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? RevisionComment { get; set; }

    [Column("namGenMapp")]
    public int? NamGenMapp { get; set; }

    [Column("addnewattach")]
    public int? Addnewattach { get; set; }

    [Column("toll_licensetype")]
    public int? TollLicensetype { get; set; }

    [Column("toll_licensedate", TypeName = "datetime")]
    public DateTime? TollLicensedate { get; set; }

    public int? DataValidationFlag { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UserPasswordBackup { get; set; }

    [Column("isactiveBackup")]
    public bool? IsactiveBackup { get; set; }

    [Column("isdeliveryuser")]
    public int? Isdeliveryuser { get; set; }

    public int? Blockstatistic { get; set; }

    [Column("blockweb")]
    public int? Blockweb { get; set; }

    [Column("blockusers")]
    public int? Blockusers { get; set; }

    [Column("GLN")]
    [StringLength(255)]
    public string? Gln { get; set; }

    [Column("companydetmainID")]
    public int? CompanydetmainId { get; set; }

    public bool IsDeleted { get; set; }

    [StringLength(100)]
    public string? DeletedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeletedDate { get; set; }
}
