using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("ProjectAssignment")]
    public class ProjectAssignment
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Project Assignment I D")]
        public Int32 ProjectAssignmentID { get; set; }

        [Required]
        [Display(Name = "Project I D")]
        public Int32 ProjectID { get; set; }

        [Required]
        [Display(Name = "Employee I D")]
        public Int32 EmployeeID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Assignment Date")]
        public DateTime AssignmentDate { get; set; }

        [StringLength(8000)]
        [Display(Name = "Remarks")]
        public String Remarks { get; set; }

        [Required]
        [Display(Name = "Add User I D")]
        public Int32 AddUserID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Add Date")]
        public DateTime AddDate { get; set; }

        [Display(Name = "Archive User I D")]
        public Int32? ArchiveUserID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Archive Date")]
        public DateTime? ArchiveDate { get; set; }

        // ComboBox
        public virtual Project Project { get; set; }
        public virtual Employee Employee { get; set; }

    }
}
 
