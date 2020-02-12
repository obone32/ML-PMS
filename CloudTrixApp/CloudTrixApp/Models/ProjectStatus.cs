using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("ProjectStatus")]
    public class ProjectStatus
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Project Status I D")]
        public Int32 ProjectStatusID { get; set; }

        [Required]
        [StringLength(8000)]
        [Display(Name = "Project Status Name")]
        public String ProjectStatusName { get; set; }


    }
}
 
