using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("TaskAttachment")]
    public class TaskAttachment
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Task I D")]
        public Int32 TaskID { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required]
        [Display(Name = "Task Attachment I D")]
        public Int32 TaskAttachmentID { get; set; }

        [Required]
        [StringLength(8000)]
        [Display(Name = "Attachment Name")]
        public String AttachmentName { get; set; }

        [StringLength(8000)]
        [Display(Name = "Decription")]
        public String Decription { get; set; }

        [Required]
        [StringLength(8000)]
        [Display(Name = "File Path")]
        public String FilePath { get; set; }

        // ComboBox
        public virtual Task Task { get; set; }

    }
}
 
