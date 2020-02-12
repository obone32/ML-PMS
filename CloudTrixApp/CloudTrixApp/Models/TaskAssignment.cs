using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("TaskAssignment")]
    public class TaskAssignment
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Task Assignment I D")]
        public Int32 TaskAssignmentID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Assignment Date")]
        public DateTime AssignmentDate { get; set; }

        [Required]
        [Display(Name = "Task I D")]
        public Int32 TaskID { get; set; }

        [Required]
        [Display(Name = "Employee I D")]
        public Int32 EmployeeID { get; set; }

        [Required]
        [Display(Name = "Task State I D")]
        public Int32 TaskStateID { get; set; }

        // ComboBox
        public virtual Task Task { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual TaskState TaskState { get; set; }

    }
}
 
