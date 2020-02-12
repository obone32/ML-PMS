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
    public class InvoiceAdvanceController : Controller
    {

        DataTable dtInvoiceAdvance = new DataTable();
        DataTable dtInvoice = new DataTable();
        DataTable dtAdvancePayment = new DataTable();

        // GET: /InvoiceAdvance/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Invoice I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["InvoiceIDSortParm"] = sortOrder == "InvoiceID_asc" ? "InvoiceID_desc" : "InvoiceID_asc";
            ViewData["AdvancePaymentIDSortParm"] = sortOrder == "AdvancePaymentID_asc" ? "AdvancePaymentID_desc" : "AdvancePaymentID_asc";

            dtInvoiceAdvance = InvoiceAdvanceData.SelectAll();
            dtInvoice = InvoiceAdvance_InvoiceData.SelectAll();
            dtAdvancePayment = InvoiceAdvance_AdvancePaymentData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtInvoiceAdvance = InvoiceAdvanceData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowInvoiceAdvance in dtInvoiceAdvance.AsEnumerable()
                        join rowInvoice in dtInvoice.AsEnumerable() on rowInvoiceAdvance.Field<Int32>("InvoiceID") equals rowInvoice.Field<Int32>("InvoiceID")
                        join rowAdvancePayment in dtAdvancePayment.AsEnumerable() on rowInvoiceAdvance.Field<Int32>("AdvancePaymentID") equals rowAdvancePayment.Field<Int32>("AdvancePaymentID")
                        select new InvoiceAdvance() {
                            Invoice = new Invoice() 
                            {
                                   InvoiceID = rowInvoice.Field<Int32>("InvoiceID")
                            }
                           ,
                            AdvancePayment = new AdvancePayment() 
                            {
                                   AdvancePaymentID = rowAdvancePayment.Field<Int32>("AdvancePaymentID")
                            }
                        };

            switch (sortOrder)
            {
                case "InvoiceID_desc":
                    Query = Query.OrderByDescending(s => s.Invoice.InvoiceID);
                    break;
                case "InvoiceID_asc":
                    Query = Query.OrderBy(s => s.Invoice.InvoiceID);
                    break;
                case "AdvancePaymentID_desc":
                    Query = Query.OrderByDescending(s => s.AdvancePayment.AdvancePaymentID);
                    break;
                case "AdvancePaymentID_asc":
                    Query = Query.OrderBy(s => s.AdvancePayment.AdvancePaymentID);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.InvoiceID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Invoice I D", typeof(string));
                dt.Columns.Add("Advance Payment I D", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.Invoice.InvoiceID
                       ,item.AdvancePayment.AdvancePaymentID
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

        // GET: /InvoiceAdvance/Details/<id>
        public ActionResult Details(
                                      Int32? InvoiceID
                                     ,Int32? AdvancePaymentID
                                   )
        {
            if (
                    InvoiceID == null
                 || AdvancePaymentID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtInvoice = InvoiceAdvance_InvoiceData.SelectAll();
            dtAdvancePayment = InvoiceAdvance_AdvancePaymentData.SelectAll();

            InvoiceAdvance InvoiceAdvance = new InvoiceAdvance();
            InvoiceAdvance.InvoiceID = System.Convert.ToInt32(InvoiceID);
            InvoiceAdvance.AdvancePaymentID = System.Convert.ToInt32(AdvancePaymentID);
            InvoiceAdvance = InvoiceAdvanceData.Select_Record(InvoiceAdvance);
            InvoiceAdvance.Invoice = new Invoice()
            {
                InvoiceID = (Int32)InvoiceAdvance.InvoiceID
            };
            InvoiceAdvance.AdvancePayment = new AdvancePayment()
            {
                AdvancePaymentID = (Int32)InvoiceAdvance.AdvancePaymentID
            };

            if (InvoiceAdvance == null)
            {
                return HttpNotFound();
            }
            return View(InvoiceAdvance);
        }

        // GET: /InvoiceAdvance/Create
        public ActionResult Create()
        {
        // ComboBox
            ViewData["InvoiceID"] = new SelectList(InvoiceAdvance_InvoiceData.List(), "InvoiceID", "InvoiceID");
            ViewData["AdvancePaymentID"] = new SelectList(InvoiceAdvance_AdvancePaymentData.List(), "AdvancePaymentID", "AdvancePaymentID");

            return View();
        }

        // POST: /InvoiceAdvance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "InvoiceID"
				   + "," + "AdvancePaymentID"
				  )] InvoiceAdvance InvoiceAdvance)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = InvoiceAdvanceData.Add(InvoiceAdvance);
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
            ViewData["InvoiceID"] = new SelectList(InvoiceAdvance_InvoiceData.List(), "InvoiceID", "InvoiceID", InvoiceAdvance.InvoiceID);
            ViewData["AdvancePaymentID"] = new SelectList(InvoiceAdvance_AdvancePaymentData.List(), "AdvancePaymentID", "AdvancePaymentID", InvoiceAdvance.AdvancePaymentID);

            return View(InvoiceAdvance);
        }

        // GET: /InvoiceAdvance/Edit/<id>
        public ActionResult Edit(
                                   Int32? InvoiceID
                                  ,Int32? AdvancePaymentID
                                )
        {
            if (
                    InvoiceID == null
                 || AdvancePaymentID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            InvoiceAdvance InvoiceAdvance = new InvoiceAdvance();
            InvoiceAdvance.InvoiceID = System.Convert.ToInt32(InvoiceID);
            InvoiceAdvance.AdvancePaymentID = System.Convert.ToInt32(AdvancePaymentID);
            InvoiceAdvance = InvoiceAdvanceData.Select_Record(InvoiceAdvance);

            if (InvoiceAdvance == null)
            {
                return HttpNotFound();
            }
        // ComboBox
            ViewData["InvoiceID"] = new SelectList(InvoiceAdvance_InvoiceData.List(), "InvoiceID", "InvoiceID", InvoiceAdvance.InvoiceID);
            ViewData["AdvancePaymentID"] = new SelectList(InvoiceAdvance_AdvancePaymentData.List(), "AdvancePaymentID", "AdvancePaymentID", InvoiceAdvance.AdvancePaymentID);

            return View(InvoiceAdvance);
        }

        // POST: /InvoiceAdvance/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InvoiceAdvance InvoiceAdvance)
        {

            InvoiceAdvance oInvoiceAdvance = new InvoiceAdvance();
            oInvoiceAdvance.InvoiceID = System.Convert.ToInt32(InvoiceAdvance.InvoiceID);
            oInvoiceAdvance.AdvancePaymentID = System.Convert.ToInt32(InvoiceAdvance.AdvancePaymentID);
            oInvoiceAdvance = InvoiceAdvanceData.Select_Record(InvoiceAdvance);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = InvoiceAdvanceData.Update(oInvoiceAdvance, InvoiceAdvance);
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
            ViewData["InvoiceID"] = new SelectList(InvoiceAdvance_InvoiceData.List(), "InvoiceID", "InvoiceID", InvoiceAdvance.InvoiceID);
            ViewData["AdvancePaymentID"] = new SelectList(InvoiceAdvance_AdvancePaymentData.List(), "AdvancePaymentID", "AdvancePaymentID", InvoiceAdvance.AdvancePaymentID);

            return View(InvoiceAdvance);
        }

        // GET: /InvoiceAdvance/Delete/<id>
        public ActionResult Delete(
                                     Int32? InvoiceID
                                    ,Int32? AdvancePaymentID
                                  )
        {
            if (
                    InvoiceID == null
                 || AdvancePaymentID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtInvoice = InvoiceAdvance_InvoiceData.SelectAll();
            dtAdvancePayment = InvoiceAdvance_AdvancePaymentData.SelectAll();

            InvoiceAdvance InvoiceAdvance = new InvoiceAdvance();
            InvoiceAdvance.InvoiceID = System.Convert.ToInt32(InvoiceID);
            InvoiceAdvance.AdvancePaymentID = System.Convert.ToInt32(AdvancePaymentID);
            InvoiceAdvance = InvoiceAdvanceData.Select_Record(InvoiceAdvance);
            InvoiceAdvance.Invoice = new Invoice()
            {
                InvoiceID = (Int32)InvoiceAdvance.InvoiceID
            };
            InvoiceAdvance.AdvancePayment = new AdvancePayment()
            {
                AdvancePaymentID = (Int32)InvoiceAdvance.AdvancePaymentID
            };

            if (InvoiceAdvance == null)
            {
                return HttpNotFound();
            }
            return View(InvoiceAdvance);
        }

        // POST: /InvoiceAdvance/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? InvoiceID
                                            ,Int32? AdvancePaymentID
                                            )
        {

            InvoiceAdvance InvoiceAdvance = new InvoiceAdvance();
            InvoiceAdvance.InvoiceID = System.Convert.ToInt32(InvoiceID);
            InvoiceAdvance.AdvancePaymentID = System.Convert.ToInt32(AdvancePaymentID);
            InvoiceAdvance = InvoiceAdvanceData.Select_Record(InvoiceAdvance);

            bool bSucess = false;
            bSucess = InvoiceAdvanceData.Delete(InvoiceAdvance);
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
            SelectListItem Item1 = new SelectListItem { Text = "Invoice I D", Value = "Invoice I D" };
            SelectListItem Item2 = new SelectListItem { Text = "Advance Payment I D", Value = "Advance Payment I D" };

                 if (select == "Invoice I D") { Item1.Selected = true; }
            else if (select == "Advance Payment I D") { Item2.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Invoice Advance", "Many");
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
 
