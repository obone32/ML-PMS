using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("AdvancePayment")]
    public class AdvancePayment
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Advance Payment I D")]
        public Int32 AdvancePaymentID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Required]
        [Display(Name = "Company I D")]
        public Int32 CompanyID { get; set; }

        [Required]
        [Display(Name = "Client I D")]
        public Int32 ClientID { get; set; }

        [Required]
        [Display(Name = "Project I D")]
        public Int32 ProjectID { get; set; }

        [Required]
        [Display(Name = "Gross Amount")]
        public Decimal GrossAmount { get; set; }

        [Required]
        [Display(Name = "T D S Rate")]
        public Decimal TDSRate { get; set; }

        [Required]
        [Display(Name = "C G S T Rate")]
        public Decimal CGSTRate { get; set; }

        [Required]
        [Display(Name = "S G S T Rate")]
        public Decimal SGSTRate { get; set; }

        [Required]
        [Display(Name = "I G S T Rate")]
        public Decimal IGSTRate { get; set; }

        [StringLength(8000)]
        [Display(Name = "Remarks")]
        public String Remarks { get; set; }

        // ComboBox
        public virtual Company Company { get; set; }
        public virtual Client Client { get; set; }
        public virtual Project Project { get; set; }

    }
}
 
