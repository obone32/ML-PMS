using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("Reconciliation")]
    public class Reconciliation
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Reconciliation I D")]
        public Int32 ReconciliationID { get; set; }

        [Required]
        [Display(Name = "Invoice I D")]
        public Int32 InvoiceID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Required]
        [Display(Name = "Payment Amount")]
        public Decimal PaymentAmount { get; set; }

        [Required]
        [Display(Name = "T D S Amount")]
        public Decimal TDSAmount { get; set; }

        [StringLength(8000)]
        [Display(Name = "Remarks")]
        public String Remarks { get; set; }

        // ComboBox
        public virtual Invoice Invoice { get; set; }

    }
}
 
