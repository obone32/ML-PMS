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
    public class InvoiceItemController : Controller
    {

        DataTable dtInvoiceItem = new DataTable();
        DataTable dtInvoice = new DataTable();

        // GET: /InvoiceItem/
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
            ViewData["InvoiceItemIDSortParm"] = sortOrder == "InvoiceItemID_asc" ? "InvoiceItemID_desc" : "InvoiceItemID_asc";
            ViewData["DescriptionSortParm"] = sortOrder == "Description_asc" ? "Description_desc" : "Description_asc";
            ViewData["QuantitySortParm"] = sortOrder == "Quantity_asc" ? "Quantity_desc" : "Quantity_asc";
            ViewData["RateSortParm"] = sortOrder == "Rate_asc" ? "Rate_desc" : "Rate_asc";
            ViewData["DiscountAmountSortParm"] = sortOrder == "DiscountAmount_asc" ? "DiscountAmount_desc" : "DiscountAmount_asc";
            ViewData["CGSTRateSortParm"] = sortOrder == "CGSTRate_asc" ? "CGSTRate_desc" : "CGSTRate_asc";
            ViewData["SGSTRateSortParm"] = sortOrder == "SGSTRate_asc" ? "SGSTRate_desc" : "SGSTRate_asc";
            ViewData["IGSTRateSortParm"] = sortOrder == "IGSTRate_asc" ? "IGSTRate_desc" : "IGSTRate_asc";

            dtInvoiceItem = InvoiceItemData.SelectAll();
            dtInvoice = InvoiceItem_InvoiceData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtInvoiceItem = InvoiceItemData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowInvoiceItem in dtInvoiceItem.AsEnumerable()
                        join rowInvoice in dtInvoice.AsEnumerable() on rowInvoiceItem.Field<Int32>("InvoiceID") equals rowInvoice.Field<Int32>("InvoiceID")
                        select new InvoiceItem() {
                            Invoice = new Invoice() 
                            {
                                   InvoiceID = rowInvoice.Field<Int32>("InvoiceID")
                            }
                           ,InvoiceItemID = rowInvoiceItem.Field<Int32>("InvoiceItemID")
                           ,Description = rowInvoiceItem.Field<String>("Description")
                           ,Quantity = rowInvoiceItem.Field<Decimal>("Quantity")
                           ,Rate = rowInvoiceItem.Field<Decimal>("Rate")
                           ,DiscountAmount = rowInvoiceItem.Field<Decimal>("DiscountAmount")
                           ,CGSTRate = rowInvoiceItem.Field<Decimal>("CGSTRate")
                           ,SGSTRate = rowInvoiceItem.Field<Decimal>("SGSTRate")
                           ,IGSTRate = rowInvoiceItem.Field<Decimal>("IGSTRate")
                        };

            switch (sortOrder)
            {
                case "InvoiceID_desc":
                    Query = Query.OrderByDescending(s => s.Invoice.InvoiceID);
                    break;
                case "InvoiceID_asc":
                    Query = Query.OrderBy(s => s.Invoice.InvoiceID);
                    break;
                case "InvoiceItemID_desc":
                    Query = Query.OrderByDescending(s => s.InvoiceItemID);
                    break;
                case "InvoiceItemID_asc":
                    Query = Query.OrderBy(s => s.InvoiceItemID);
                    break;
                case "Description_desc":
                    Query = Query.OrderByDescending(s => s.Description);
                    break;
                case "Description_asc":
                    Query = Query.OrderBy(s => s.Description);
                    break;
                case "Quantity_desc":
                    Query = Query.OrderByDescending(s => s.Quantity);
                    break;
                case "Quantity_asc":
                    Query = Query.OrderBy(s => s.Quantity);
                    break;
                case "Rate_desc":
                    Query = Query.OrderByDescending(s => s.Rate);
                    break;
                case "Rate_asc":
                    Query = Query.OrderBy(s => s.Rate);
                    break;
                case "DiscountAmount_desc":
                    Query = Query.OrderByDescending(s => s.DiscountAmount);
                    break;
                case "DiscountAmount_asc":
                    Query = Query.OrderBy(s => s.DiscountAmount);
                    break;
                case "CGSTRate_desc":
                    Query = Query.OrderByDescending(s => s.CGSTRate);
                    break;
                case "CGSTRate_asc":
                    Query = Query.OrderBy(s => s.CGSTRate);
                    break;
                case "SGSTRate_desc":
                    Query = Query.OrderByDescending(s => s.SGSTRate);
                    break;
                case "SGSTRate_asc":
                    Query = Query.OrderBy(s => s.SGSTRate);
                    break;
                case "IGSTRate_desc":
                    Query = Query.OrderByDescending(s => s.IGSTRate);
                    break;
                case "IGSTRate_asc":
                    Query = Query.OrderBy(s => s.IGSTRate);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.InvoiceID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Invoice I D", typeof(string));
                dt.Columns.Add("Invoice Item I D", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Quantity", typeof(string));
                dt.Columns.Add("Rate", typeof(string));
                dt.Columns.Add("Discount Amount", typeof(string));
                dt.Columns.Add("C G S T Rate", typeof(string));
                dt.Columns.Add("S G S T Rate", typeof(string));
                dt.Columns.Add("I G S T Rate", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.Invoice.InvoiceID
                       ,item.InvoiceItemID
                       ,item.Description
                       ,item.Quantity
                       ,item.Rate
                       ,item.DiscountAmount
                       ,item.CGSTRate
                       ,item.SGSTRate
                       ,item.IGSTRate
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

        // GET: /InvoiceItem/Details/<id>
        public ActionResult Details(
                                      Int32? InvoiceID
                                     ,Int32? InvoiceItemID
                                   )
        {
            if (
                    InvoiceID == null
                 || InvoiceItemID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtInvoice = InvoiceItem_InvoiceData.SelectAll();

            InvoiceItem InvoiceItem = new InvoiceItem();
            InvoiceItem.InvoiceID = System.Convert.ToInt32(InvoiceID);
            InvoiceItem.InvoiceItemID = System.Convert.ToInt32(InvoiceItemID);
            InvoiceItem = InvoiceItemData.Select_Record(InvoiceItem);
            InvoiceItem.Invoice = new Invoice()
            {
                InvoiceID = (Int32)InvoiceItem.InvoiceID
            };

            if (InvoiceItem == null)
            {
                return HttpNotFound();
            }
            return View(InvoiceItem);
        }

        // GET: /InvoiceItem/Create
        public ActionResult Create()
        {
        // ComboBox
            ViewData["InvoiceID"] = new SelectList(InvoiceItem_InvoiceData.List(), "InvoiceID", "InvoiceID");

            return View();
        }

        // POST: /InvoiceItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "InvoiceID"
				   + "," + "InvoiceItemID"
				   + "," + "Description"
				   + "," + "Quantity"
				   + "," + "Rate"
				   + "," + "DiscountAmount"
				   + "," + "CGSTRate"
				   + "," + "SGSTRate"
				   + "," + "IGSTRate"
				  )] InvoiceItem InvoiceItem)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = InvoiceItemData.Add(InvoiceItem);
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
            ViewData["InvoiceID"] = new SelectList(InvoiceItem_InvoiceData.List(), "InvoiceID", "InvoiceID", InvoiceItem.InvoiceID);

            return View(InvoiceItem);
        }

        // GET: /InvoiceItem/Edit/<id>
        public ActionResult Edit(
                                   Int32? InvoiceID
                                  ,Int32? InvoiceItemID
                                )
        {
            if (
                    InvoiceID == null
                 || InvoiceItemID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            InvoiceItem InvoiceItem = new InvoiceItem();
            InvoiceItem.InvoiceID = System.Convert.ToInt32(InvoiceID);
            InvoiceItem.InvoiceItemID = System.Convert.ToInt32(InvoiceItemID);
            InvoiceItem = InvoiceItemData.Select_Record(InvoiceItem);

            if (InvoiceItem == null)
            {
                return HttpNotFound();
            }
        // ComboBox
            ViewData["InvoiceID"] = new SelectList(InvoiceItem_InvoiceData.List(), "InvoiceID", "InvoiceID", InvoiceItem.InvoiceID);

            return View(InvoiceItem);
        }

        // POST: /InvoiceItem/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InvoiceItem InvoiceItem)
        {

            InvoiceItem oInvoiceItem = new InvoiceItem();
            oInvoiceItem.InvoiceID = System.Convert.ToInt32(InvoiceItem.InvoiceID);
            oInvoiceItem.InvoiceItemID = System.Convert.ToInt32(InvoiceItem.InvoiceItemID);
            oInvoiceItem = InvoiceItemData.Select_Record(InvoiceItem);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = InvoiceItemData.Update(oInvoiceItem, InvoiceItem);
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
            ViewData["InvoiceID"] = new SelectList(InvoiceItem_InvoiceData.List(), "InvoiceID", "InvoiceID", InvoiceItem.InvoiceID);

            return View(InvoiceItem);
        }

        // GET: /InvoiceItem/Delete/<id>
        public ActionResult Delete(
                                     Int32? InvoiceID
                                    ,Int32? InvoiceItemID
                                  )
        {
            if (
                    InvoiceID == null
                 || InvoiceItemID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtInvoice = InvoiceItem_InvoiceData.SelectAll();

            InvoiceItem InvoiceItem = new InvoiceItem();
            InvoiceItem.InvoiceID = System.Convert.ToInt32(InvoiceID);
            InvoiceItem.InvoiceItemID = System.Convert.ToInt32(InvoiceItemID);
            InvoiceItem = InvoiceItemData.Select_Record(InvoiceItem);
            InvoiceItem.Invoice = new Invoice()
            {
                InvoiceID = (Int32)InvoiceItem.InvoiceID
            };

            if (InvoiceItem == null)
            {
                return HttpNotFound();
            }
            return View(InvoiceItem);
        }

        // POST: /InvoiceItem/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? InvoiceID
                                            ,Int32? InvoiceItemID
                                            )
        {

            InvoiceItem InvoiceItem = new InvoiceItem();
            InvoiceItem.InvoiceID = System.Convert.ToInt32(InvoiceID);
            InvoiceItem.InvoiceItemID = System.Convert.ToInt32(InvoiceItemID);
            InvoiceItem = InvoiceItemData.Select_Record(InvoiceItem);

            bool bSucess = false;
            bSucess = InvoiceItemData.Delete(InvoiceItem);
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
            SelectListItem Item2 = new SelectListItem { Text = "Invoice Item I D", Value = "Invoice Item I D" };
            SelectListItem Item3 = new SelectListItem { Text = "Description", Value = "Description" };
            SelectListItem Item4 = new SelectListItem { Text = "Quantity", Value = "Quantity" };
            SelectListItem Item5 = new SelectListItem { Text = "Rate", Value = "Rate" };
            SelectListItem Item6 = new SelectListItem { Text = "Discount Amount", Value = "Discount Amount" };
            SelectListItem Item7 = new SelectListItem { Text = "C G S T Rate", Value = "C G S T Rate" };
            SelectListItem Item8 = new SelectListItem { Text = "S G S T Rate", Value = "S G S T Rate" };
            SelectListItem Item9 = new SelectListItem { Text = "I G S T Rate", Value = "I G S T Rate" };

                 if (select == "Invoice I D") { Item1.Selected = true; }
            else if (select == "Invoice Item I D") { Item2.Selected = true; }
            else if (select == "Description") { Item3.Selected = true; }
            else if (select == "Quantity") { Item4.Selected = true; }
            else if (select == "Rate") { Item5.Selected = true; }
            else if (select == "Discount Amount") { Item6.Selected = true; }
            else if (select == "C G S T Rate") { Item7.Selected = true; }
            else if (select == "S G S T Rate") { Item8.Selected = true; }
            else if (select == "I G S T Rate") { Item9.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);
            list.Add(Item5);
            list.Add(Item6);
            list.Add(Item7);
            list.Add(Item8);
            list.Add(Item9);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Invoice Item", "Many");
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
 
