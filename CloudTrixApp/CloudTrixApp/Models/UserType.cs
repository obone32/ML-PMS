using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("UserType")]
    public class UserType
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "User Type I D")]
        public Int32 UserTypeID { get; set; }

        [Required]
        [StringLength(8000)]
        [Display(Name = "User Type Name")]
        public String UserTypeName { get; set; }


    }
}
 
