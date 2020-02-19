using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("RoleManagement")]
    public class RoleManagement
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Role ID")]
        public Int32 RoleID { get; set; }

        [Required]
        [Display(Name = "UserTypeID")]
        public Int32 UserTypeID { get; set; }

        [Required]
        [Display(Name = "Add User I D")]
        public Int32? AddUserID { get; set; }

        [Required]
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

        public virtual UserType UserType { get; set; }
        public virtual RoleManagementDetails RoleManagementDetails { get; set; }

    }
}