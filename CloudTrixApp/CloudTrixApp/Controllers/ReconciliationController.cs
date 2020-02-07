using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using PagedList;
using PagedList.Mvc;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using CloudTrixApp.Models;
using CloudTrixApp.Data;

namespace CloudTrixApp.Controllers
{
    public class ReconciliationController : Controller
    {

        DataTable dtReconciliation = new DataTable();
        DataTable dtInvoice = new DataTable();

        // GET: /Reconciliation/
        public ActionResult Index(string sortOrder,  
                                  String SearchField,
                                  String SearchCondition,
                                  String SearchText,
                                  String Export,
                                  int? PageSize,
                                  int? page, 
                                  string command)
        {

            if (command == "Show All") {
                SearchField = null;
                SearchCondition = null;
                SearchText = null;
                Session["SearchField"] = null;
                Session["SearchCondition"] = null;
                Session["SearchText"] = null; } 
            else if (command == "Add New Record") { return RedirectToAction("Create"); } 
            else if (command == "Export") { Session["Export"] = Export; } 
            else if (command == "Search" | command == "Page Size") {
                if (!string.IsNullOrEmpty(SearchText)) {
                    Session["SearchField"] = SearchField;
                    Session["SearchCondition"] = SearchCondition;
                    Session["SearchText"] = SearchText; }
                } 
            if (command == "Page Size") { Session["PageSize"] = PageSize; }

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Reconciliation I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["ReconciliationIDSortParm"] = sortOrder == "ReconciliationID_asc" ? "ReconciliationID_desc" : "ReconciliationID_asc";
            ViewData["InvoiceIDSortParm"] = sortOrder == "InvoiceID_asc" ? "InvoiceID_desc" : "InvoiceID_asc";
            ViewData["PaymentDateSortParm"] = sortOrder == "PaymentDate_asc" ? "PaymentDate_desc" : "PaymentDate_asc";
            ViewData["PaymentAmountSortParm"] = sortOrder == "PaymentAmount_asc" ? "PaymentAmount_desc" : "PaymentAmount_asc";
            ViewData["TDSAmountSortParm"] = sortOrder == "TDSAmount_asc" ? "TDSAmount_desc" : "TDSAmount_asc";
            ViewData["RemarksSortParm"] = sortOrder == "Remarks_asc" ? "Remarks_desc" : "Remarks_asc";

            dtReconciliation = ReconciliationData.SelectAll();
            dtInvoice = Reconciliation_InvoiceData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtReconciliation = ReconciliationData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowReconciliation in dtReconciliation.AsEnumerable()
                        join rowInvoice in dtInvoice.AsEnumerable() on rowReconciliation.Field<Int32>("InvoiceID") equals rowInvoice.Field<Int32>("InvoiceID")
                        select new Reconciliation() {
                            ReconciliationID = rowReconciliation.Field<Int32>("ReconciliationID")
                           ,
                            Invoice = new Invoice() 
                            {
                                   InvoiceID = rowInvoice.Field<Int32>("InvoiceID")
                                  ,InvoiceNo = rowInvoice.Field<String>("InvoiceNo")
                            }
                           ,PaymentDate = rowReconciliation.Field<DateTime>("PaymentDate")
                           ,PaymentAmount = rowReconciliation.Field<Decimal>("PaymentAmount")
                           ,TDSAmount = rowReconciliation.Field<Decimal>("TDSAmount")
                           ,Remarks = rowReconciliation.Field<String>("Remarks")
                        };

            switch (sortOrder)
            {
                case "ReconciliationID_desc":
                    Query = Query.OrderByDescending(s => s.ReconciliationID);
                    break;
                case "ReconciliationID_asc":
                    Query = Query.OrderBy(s => s.ReconciliationID);
                    break;
                case "InvoiceID_desc":
                    Query = Query.OrderByDescending(s => s.Invoice.InvoiceNo);
                    break;
                case "InvoiceID_asc":
                    Query = Query.OrderBy(s => s.Invoice.InvoiceNo);
                    break;
                case "PaymentDate_desc":
                    Query = Query.OrderByDescending(s => s.PaymentDate);
                    break;
                case "PaymentDate_asc":
                    Query = Query.OrderBy(s => s.PaymentDate);
                    break;
                case "PaymentAmount_desc":
                    Query = Query.OrderByDescending(s => s.PaymentAmount);
                    break;
                case "PaymentAmount_asc":
                    Query = Query.OrderBy(s => s.PaymentAmount);
                    break;
                case "TDSAmount_desc":
                    Query = Query.OrderByDescending(s => s.TDSAmount);
                    break;
                case "TDSAmount_asc":
                    Query = Query.OrderBy(s => s.TDSAmount);
                    break;
                case "Remarks_desc":
                    Query = Query.OrderByDescending(s => s.Remarks);
                    break;
                case "Remarks_asc":
                    Query = Query.OrderBy(s => s.Remarks);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.ReconciliationID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Reconciliation I D", typeof(string));
                dt.Columns.Add("Invoice I D", typeof(string));
                dt.Columns.Add("Payment Date", typeof(string));
                dt.Columns.Add("Payment Amount", typeof(string));
                dt.Columns.Add("T D S Amount", typeof(string));
                dt.Columns.Add("Remarks", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.ReconciliationID
                       ,item.Invoice.InvoiceNo
                       ,item.PaymentDate
                       ,item.PaymentAmount
                       ,item.TDSAmount
                       ,item.Remarks
                    );
                }
                gv.DataSource = dt;
                gv.DataBind();
                ExportData(Export, gv, dt);
            }

            int pageNumber = (page ?? 1);
            int? pageSZ = (Convert.ToInt32(Session["PageSize"]) == 0 ? 5 : Convert.ToInt32(Session["PageSize"]));
            return View(Query.ToPagedList(pageNumber, (pageSZ ?? 5)));
        }

        // GET: /Reconciliation/Details/<id>
        public ActionResult Details(
                                      Int32? ReconciliationID
                                   )
        {
            if (
                    ReconciliationID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtInvoice = Reconciliation_InvoiceData.SelectAll();

            Reconciliation Reconciliation = new Reconciliation();
            Reconciliation.ReconciliationID = System.Convert.ToInt32(ReconciliationID);
            Reconciliation = ReconciliationData.Select_Record(Reconciliation);
            Reconciliation.Invoice = new Invoice()
            {
                InvoiceID = (Int32)Reconciliation.InvoiceID
               ,InvoiceNo = (from DataRow rowInvoice in dtInvoice.Rows
                      where Reconciliation.InvoiceID == (int)rowInvoice["InvoiceID"]
                      select (String)rowInvoice["InvoiceNo"]).FirstOrDefault()
            };

            if (Reconciliation == null)
            {
                return HttpNotFound();
            }
            return View(Reconciliation);
        }

        // GET: /Reconciliation/Create
        public ActionResult Create()
        {
        // ComboBox
            ViewData["InvoiceID"] = new SelectList(Reconciliation_InvoiceData.List(), "InvoiceID", "InvoiceNo");

            return View();
        }

        // POST: /Reconciliation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "InvoiceID"
				   + "," + "PaymentDate"
				   + "," + "PaymentAmount"
				   + "," + "TDSAmount"
				   + "," + "Remarks"
				  )] Reconciliation Reconciliation)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = ReconciliationData.Add(Reconciliation);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }
        // ComboBox
            ViewData["InvoiceID"] = new SelectList(Reconciliation_InvoiceData.List(), "InvoiceID", "InvoiceNo", Reconciliation.InvoiceID);

            return View(Reconciliation);
        }

        // GET: /Reconciliation/Edit/<id>
        public ActionResult Edit(
                                   Int32? ReconciliationID
                                )
        {
            if (
                    ReconciliationID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Reconciliation Reconciliation = new Reconciliation();
            Reconciliation.ReconciliationID = System.Convert.ToInt32(ReconciliationID);
            Reconciliation = ReconciliationData.Select_Record(Reconciliation);

            if (Reconciliation == null)
            {
                return HttpNotFound();
            }
        // ComboBox
            ViewData["InvoiceID"] = new SelectList(Reconciliation_InvoiceData.List(), "InvoiceID", "InvoiceNo", Reconciliation.InvoiceID);

            return View(Reconciliation);
        }

        // POST: /Reconciliation/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Reconciliation Reconciliation)
        {

            Reconciliation oReconciliation = new Reconciliation();
            oReconciliation.ReconciliationID = System.Convert.ToInt32(Reconciliation.ReconciliationID);
            oReconciliation = ReconciliationData.Select_Record(Reconciliation);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = ReconciliationData.Update(oReconciliation, Reconciliation);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }
        // ComboBox
            ViewData["InvoiceID"] = new SelectList(Reconciliation_InvoiceData.List(), "InvoiceID", "InvoiceNo", Reconciliation.InvoiceID);

            return View(Reconciliation);
        }

        // GET: /Reconciliation/Delete/<id>
        public ActionResult Delete(
                                     Int32? ReconciliationID
                                  )
        {
            if (
                    ReconciliationID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtInvoice = Reconciliation_InvoiceData.SelectAll();

            Reconciliation Reconciliation = new Reconciliation();
            Reconciliation.ReconciliationID = System.Convert.ToInt32(ReconciliationID);
            Reconciliation = ReconciliationData.Select_Record(Reconciliation);
            Reconciliation.Invoice = new Invoice()
            {
                InvoiceID = (Int32)Reconciliation.InvoiceID
               ,InvoiceNo = (from DataRow rowInvoice in dtInvoice.Rows
                      where Reconciliation.InvoiceID == (int)rowInvoice["InvoiceID"]
                      select (String)rowInvoice["InvoiceNo"]).FirstOrDefault()
            };

            if (Reconciliation == null)
            {
                return HttpNotFound();
            }
            return View(Reconciliation);
        }

        // POST: /Reconciliation/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? ReconciliationID
                                            )
        {

            Reconciliation Reconciliation = new Reconciliation();
            Reconciliation.ReconciliationID = System.Convert.ToInt32(ReconciliationID);
            Reconciliation = ReconciliationData.Select_Record(Reconciliation);

            bool bSucess = false;
            bSucess = ReconciliationData.Delete(Reconciliation);
            if (bSucess == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Can Not Delete");
            }
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private static List<SelectListItem> GetFields(String select)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem Item1 = new SelectListItem { Text = "Reconciliation I D", Value = "Reconciliation I D" };
            SelectListItem Item2 = new SelectListItem { Text = "Invoice I D", Value = "Invoice I D" };
            SelectListItem Item3 = new SelectListItem { Text = "Payment Date", Value = "Payment Date" };
            SelectListItem Item4 = new SelectListItem { Text = "Payment Amount", Value = "Payment Amount" };
            SelectListItem Item5 = new SelectListItem { Text = "T D S Amount", Value = "T D S Amount" };
            SelectListItem Item6 = new SelectListItem { Text = "Remarks", Value = "Remarks" };

                 if (select == "Reconciliation I D") { Item1.Selected = true; }
            else if (select == "Invoice I D") { Item2.Selected = true; }
            else if (select == "Payment Date") { Item3.Selected = true; }
            else if (select == "Payment Amount") { Item4.Selected = true; }
            else if (select == "T D S Amount") { Item5.Selected = true; }
            else if (select == "Remarks") { Item6.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);
            list.Add(Item5);
            list.Add(Item6);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Reconciliation", "Many");
                Document document = pdfForm.CreateDocument();
                PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
                renderer.Document = document;
                renderer.RenderDocument();

                MemoryStream stream = new MemoryStream();
                renderer.PdfDocument.Save(stream, false);

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + "Report.pdf");
                Response.ContentType = "application/Pdf.pdf";
                Response.BinaryWrite(stream.ToArray());
                Response.Flush();
                Response.End();
            }
            else
            {
                Response.ClearContent();
                Response.Buffer = true;
                if (Export == "Excel")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Report.xls");
                    Response.ContentType = "application/Excel.xls";
                }
                else if (Export == "Word")
                {
                    Response.AddHeader("content-disposition", "attachment;filename=" + "Report.doc");
                    Response.ContentType = "application/Word.doc";
                }
                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

    }
}
 
