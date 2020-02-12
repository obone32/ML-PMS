using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("Permission")]
    public class Permission
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Permission I D")]
        public Int32 PermissionID { get; set; }

        [Required]
        [StringLength(8000)]
        [Display(Name = "Permission Name")]
        public String PermissionName { get; set; }


    }
}
 
