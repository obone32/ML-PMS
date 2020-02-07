using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudTrixApp.Models.ViewModel
{
    public class InvoiceOrderDetailsViewModel
    {
        public int InvoiceID { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal DiscountAmount { get; set; }

    }
}