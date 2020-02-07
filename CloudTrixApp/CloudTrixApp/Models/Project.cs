using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("Project")]
    public class Project
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Project I D")]
        public Int32 ProjectID { get; set; }

        [Required]
        [StringLength(8000)]
        [Display(Name = "Project Name")]
        public String ProjectName { get; set; }

        [StringLength(8000)]
        [Display(Name = "Billing Name")]
        public String BillingName { get; set; }

        [StringLength(8000)]
        [Display(Name = "Description")]
        public String Description { get; set; }

        [StringLength(8000)]
        [Display(Name = "Location")]
        public String Location { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required]
        [Display(Name = "Project Status I D")]
        public Int32 ProjectStatusID { get; set; }

        [Required]
        [Display(Name = "Client I D")]
        public Int32 ClientID { get; set; }

        [Required]
        [Display(Name = "Architect I D")]
        public Int32 ArchitectID { get; set; }

        [Display(Name = "Company I D")]
        public Int32? CompanyID { get; set; }

        [Required]
        [Display(Name = "Add User I D")]
        public Int32 AddUserID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Add Date")]
        public DateTime AddDate { get; set; }

        [Display(Name = "Archive User I D")]
        public Int32? ArchiveUserID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Archive Date")]
        public DateTime? ArchiveDate { get; set; }

        // ComboBox
        public virtual ProjectStatus ProjectStatus { get; set; }
        public virtual Client Client { get; set; }
        public virtual Architect Architect { get; set; }
        public virtual Company Company { get; set; }

    }
}
 
