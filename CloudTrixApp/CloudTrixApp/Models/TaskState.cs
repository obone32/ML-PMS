using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("TaskState")]
    public class TaskState
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Task State I D")]
        public Int32 TaskStateID { get; set; }

        [Required]
        [StringLength(8000)]
        [Display(Name = "Task State Name")]
        public String TaskStateName { get; set; }


    }
}
 
