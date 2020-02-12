using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudTrixApp.Models.ViewModel
{
    public class InvoiceOrderViewModel
    {
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int ProjectID { get; set; }
        public int ClientID { get; set; }

        public IEnumerable<InvoiceOrderDetailsViewModel> ListOfInvoiceOrderDetails { get; set; }

    }
}