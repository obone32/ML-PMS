using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("Company")]
    public class Company
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Company I D")]
        public Int32 CompanyID { get; set; }

        [Required]
        [StringLength(8000)]
        [Display(Name = "Company Name")]
        public String CompanyName { get; set; }

        [StringLength(8000)]
        [Display(Name = "Address1")]
        public String Address1 { get; set; }

        [StringLength(8000)]
        [Display(Name = "Address2")]
        public String Address2 { get; set; }

        [StringLength(8000)]
        [Display(Name = "City")]
        public String City { get; set; }

        [StringLength(8000)]
        [Display(Name = "District")]
        public String District { get; set; }

        [StringLength(8000)]
        [Display(Name = "State")]
        public String State { get; set; }

        [StringLength(8000)]
        [Display(Name = "Country")]
        public String Country { get; set; }

        [StringLength(8000)]
        [Display(Name = "Pin Code")]
        public String PinCode { get; set; }

        [StringLength(8000)]
        [Display(Name = "Contact No")]
        public String ContactNo { get; set; }

        [StringLength(8000)]
        [Display(Name = "E Mail")]
        public String EMail { get; set; }

        [StringLength(8000)]
        [Display(Name = "G S T I N")]
        public String GSTIN { get; set; }

        [StringLength(8000)]
        [Display(Name = "Invoice Initials")]
        public String InvoiceInitials { get; set; }

        [Display(Name = "Add User I D")]
        public Int32? AddUserID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Add Date")]
        public DateTime? AddDate { get; set; }

        [Display(Name = "Archive User I D")]
        public Int32? ArchiveUserID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Archive Date")]
        public DateTime? ArchiveDate { get; set; }


    }
}
 
