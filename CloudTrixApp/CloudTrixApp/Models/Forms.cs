using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("Forms")]
    public class Forms
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Form I D")]
        public Int32 FormID { get; set; }

        [Required]
        [StringLength(8000)]
        [Display(Name = "Form Name")]
        public String FormName { get; set; }
    }
}