using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("Task")]
    public class Task
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Task I D")]
        public Int32 TaskID { get; set; }

        [Required]
        [StringLength(8000)]
        [Display(Name = "Task Name")]
        public String TaskName { get; set; }

        [StringLength(8000)]
        [Display(Name = "Description")]
        public String Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }


    }
}
 
