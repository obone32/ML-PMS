using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Invoice I D")]
        public Int32 InvoiceID { get; set; }

        [Required]
        [StringLength(8000)]
        [Display(Name = "Invoice No")]
        public String InvoiceNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Invoice Date")]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Project I D")]
        public Int32? ProjectID { get; set; }

        [Required]
        [Display(Name = "Client I D")]
        public Int32 ClientID { get; set; }

        [StringLength(8000)]
        [Display(Name = "Client Name")]
        public String ClientName { get; set; }

        [StringLength(8000)]
        [Display(Name = "Client Address")]
        public String ClientAddress { get; set; }

        [StringLength(8000)]
        [Display(Name = "Client G S T I N")]
        public String ClientGSTIN { get; set; }

        [StringLength(8000)]
        [Display(Name = "Client Contact No")]
        public String ClientContactNo { get; set; }

        [StringLength(8000)]
        [Display(Name = "Client E Mail")]
        public String ClientEMail { get; set; }

        [Display(Name = "Additional Discount")]
        public Decimal? AdditionalDiscount { get; set; }

        [StringLength(8000)]
        [Display(Name = "Remarks")]
        public String Remarks { get; set; }

        [StringLength(8000)]
        [Display(Name = "P D F Url")]
        public String PDFUrl { get; set; }

        [Display(Name = "Company I D")]
        public Int32? CompanyID { get; set; }

        [Display(Name = "Add User I D")]
        public Int32? AddUserID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Add Date")]
        public DateTime? AddDate { get; set; }

        [Display(Name = "Archive User I D")]
        public Int32? ArchiveUserID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Archive Date")]
        public DateTime? ArchiveDate { get; set; }

        public Decimal? TotalAmt { get; set; }
        // ComboBox
        public virtual Project Project { get; set; }
        public virtual Client Client { get; set; }
        public virtual Company Company { get; set; }
        public List<InvoiceItem> Items { get; set; }
    }
}
 
