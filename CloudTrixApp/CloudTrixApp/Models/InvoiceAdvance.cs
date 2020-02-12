using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("InvoiceAdvance")]
    public class InvoiceAdvance
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Invoice I D")]
        public Int32 InvoiceID { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required]
        [Display(Name = "Advance Payment I D")]
        public Int32 AdvancePaymentID { get; set; }

        // ComboBox
        public virtual Invoice Invoice { get; set; }
        public virtual AdvancePayment AdvancePayment { get; set; }

    }
}
 
