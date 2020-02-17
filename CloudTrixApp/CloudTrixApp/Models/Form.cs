using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("Form")]
    public class Form
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "FormID")]
        public Int32 FormID { get; set; }

        [Required]
        [StringLength(8000)]
        [Display(Name = "FormName")]
        public String FormName { get; set; }

        [Required]
        [Display(Name = "Add")]
        public bool Add { get; set; }

        [Required]
        [Display(Name = "Update")]
        public bool Update { get; set; }

        [Required]
        [Display(Name = "Delete")]
        public bool Delete { get; set; }

        [Required]
        [Display(Name = "View")]
        public bool View { get; set; }     

    }
}