using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("BackupLog")]
    public class BackupLog
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Backup Log I D")]
        public Int32 BackupLogID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Backup Date")]
        public DateTime BackupDate { get; set; }

        [Required]
        [StringLength(8000)]
        [Display(Name = "File Path")]
        public String FilePath { get; set; }

        [StringLength(8000)]
        [Display(Name = "Remarks")]
        public String Remarks { get; set; }


    }
}
 
