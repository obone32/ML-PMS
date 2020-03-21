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

         [Display(Name = "CGSTRate")]
        public Decimal CGSTRate { get; set; }


        [Display(Name = "SGSTRate")]
         public Decimal SGSTRate { get; set; }


        [Display(Name = "IGSTRate")]
        public Decimal IGSTRate { get; set; }

        [Required]
        [Display(Name = "Tax")]
        public String Tax { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public Decimal Amount { get; set; }

        
        [Display(Name = "IGST_Amt")]
        public Decimal IGST_Amt { get; set; }

  
        [Display(Name = "CGST_Amt")]
        public Decimal CGST_Amt { get; set; }

       
        [Display(Name = "SGST_Amt")]
        public Decimal SGST_Amt { get; set; }

        [Required]
        [Display(Name = "Total_Amt")]
        public Decimal Total_Amt { get; set; }

        // ComboBox
        public virtual Invoice Invoice { get; set; }

    }
}
 
