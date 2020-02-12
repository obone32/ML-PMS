using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("Architect")]
    public class Architect
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Architect I D")]
        public Int32 ArchitectID { get; set; }

        [Required]
        [StringLength(8000)]
        [Display(Name = "Architect Name")]
        public String ArchitectName { get; set; }

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
        [Display(Name = "Pincode")]
        public String Pincode { get; set; }

        [StringLength(8000)]
        [Display(Name = "E Mail")]
        public String EMail { get; set; }

        [StringLength(8000)]
        [Display(Name = "Contact No")]
        public String ContactNo { get; set; }

        [Required]
        [Display(Name = "Company I D")]
        public Int32 CompanyID { get; set; }

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

        // ComboBox
        public virtual Company Company { get; set; }

    }
}
 
