using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("InvoiceItem")]
    public class InvoiceItem
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Invoice I D")]
        public Int32 InvoiceID { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required]
        [Display(Name = "Invoice Item I D")]
        public Int32 InvoiceItemID { get; set; }

        [StringLength(8000)]
        [Display(Name = "Description")]
        public String Description { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public Decimal Quantity { get; set; }

        [Required]
        [Display(Name = "Rate")]
        public Decimal Rate { get; set; }

        [Required]
        [Display(Name = "Discount Amount")]
        public Decimal DiscountAmount { get; set; }

        [Required]
        [Display(Name = "C G S T Rate")]
        public Decimal CGSTRate { get; set; }

        [Required]
        [Display(Name = "S G S T Rate")]
        public Decimal SGSTRate { get; set; }

        [Required]
        [Display(Name = "I G S T Rate")]
        public Decimal IGSTRate { get; set; }

        // ComboBox
        public virtual Invoice Invoice { get; set; }

    }
}
 
