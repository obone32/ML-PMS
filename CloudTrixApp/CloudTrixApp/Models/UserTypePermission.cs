using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("UserTypePermission")]
    public class UserTypePermission
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "User Type I D")]
        public Int32 UserTypeID { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required]
        [Display(Name = "User Type Permission I D")]
        public Int32 UserTypePermissionID { get; set; }

        [Required]
        [Display(Name = "Permission I D")]
        public Int32 PermissionID { get; set; }

        // ComboBox
        public virtual UserType UserType { get; set; }
        public virtual Permission Permission { get; set; }

    }
}
 
