using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("RoleManagementDetails")]
    public class RoleManagementDetails
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "RoleManagementDetailsID")]
        public Int32 RoleManagementDetailsID { get; set; }

        [Required]
        [Display(Name = "Add Permission")]
        public bool AddPermission { get; set; }

        [Required]
        [Display(Name = "Update Permission")]
        public bool UpdatePermission { get; set; }

        [Required]
        [Display(Name = "Delete Permission")]
        public bool DeletePermission { get; set; }

        [Required]
        [Display(Name = "View Permission")]
        public bool ViewPermission { get; set; }

        [Required]
        [Display(Name = "Role ID")]
        public Int32 RoleID { get; set; }

        [Required]
        [Display(Name = "Form ID")]
        public Int32 FormID { get; set; }

        public virtual Form Form { get; set; }
        public virtual RoleManagement RoleManagement { get; set; }
    }
}