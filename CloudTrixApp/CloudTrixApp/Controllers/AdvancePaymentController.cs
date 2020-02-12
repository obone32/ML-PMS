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
    public class AdvancePaymentController : Controller
    {

        DataTable dtAdvancePayment = new DataTable();
        DataTable dtCompany = new DataTable();
        DataTable dtClient = new DataTable();
        DataTable dtProject = new DataTable();

        // GET: /AdvancePayment/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Advance Payment I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["AdvancePaymentIDSortParm"] = sortOrder == "AdvancePaymentID_asc" ? "AdvancePaymentID_desc" : "AdvancePaymentID_asc";
            ViewData["PaymentDateSortParm"] = sortOrder == "PaymentDate_asc" ? "PaymentDate_desc" : "PaymentDate_asc";
            ViewData["CompanyIDSortParm"] = sortOrder == "CompanyID_asc" ? "CompanyID_desc" : "CompanyID_asc";
            ViewData["ClientIDSortParm"] = sortOrder == "ClientID_asc" ? "ClientID_desc" : "ClientID_asc";
            ViewData["ProjectIDSortParm"] = sortOrder == "ProjectID_asc" ? "ProjectID_desc" : "ProjectID_asc";
            ViewData["GrossAmountSortParm"] = sortOrder == "GrossAmount_asc" ? "GrossAmount_desc" : "GrossAmount_asc";
            ViewData["TDSRateSortParm"] = sortOrder == "TDSRate_asc" ? "TDSRate_desc" : "TDSRate_asc";
            ViewData["CGSTRateSortParm"] = sortOrder == "CGSTRate_asc" ? "CGSTRate_desc" : "CGSTRate_asc";
            ViewData["SGSTRateSortParm"] = sortOrder == "SGSTRate_asc" ? "SGSTRate_desc" : "SGSTRate_asc";
            ViewData["IGSTRateSortParm"] = sortOrder == "IGSTRate_asc" ? "IGSTRate_desc" : "IGSTRate_asc";
            ViewData["RemarksSortParm"] = sortOrder == "Remarks_asc" ? "Remarks_desc" : "Remarks_asc";

            dtAdvancePayment = AdvancePaymentData.SelectAll();
            dtCompany = AdvancePayment_CompanyData.SelectAll();
            dtClient = AdvancePayment_ClientData.SelectAll();
            dtProject = AdvancePayment_ProjectData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtAdvancePayment = AdvancePaymentData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowAdvancePayment in dtAdvancePayment.AsEnumerable()
                        join rowCompany in dtCompany.AsEnumerable() on rowAdvancePayment.Field<Int32>("CompanyID") equals rowCompany.Field<Int32>("CompanyID")
                        join rowClient in dtClient.AsEnumerable() on rowAdvancePayment.Field<Int32>("ClientID") equals rowClient.Field<Int32>("ClientID")
                        join rowProject in dtProject.AsEnumerable() on rowAdvancePayment.Field<Int32>("ProjectID") equals rowProject.Field<Int32>("ProjectID")
                        select new AdvancePayment() {
                            AdvancePaymentID = rowAdvancePayment.Field<Int32>("AdvancePaymentID")
                           ,PaymentDate = rowAdvancePayment.Field<DateTime>("PaymentDate")
                           ,
                            Company = new Company() 
                            {
                                   CompanyID = rowCompany.Field<Int32>("CompanyID")
                                  ,CompanyName = rowCompany.Field<String>("CompanyName")
                            }
                           ,
                            Client = new Client() 
                            {
                                   ClientID = rowClient.Field<Int32>("ClientID")
                                  ,ClientName = rowClient.Field<String>("ClientName")
                            }
                           ,
                            Project = new Project() 
                            {
                                   ProjectID = rowProject.Field<Int32>("ProjectID")
                                  ,ProjectName = rowProject.Field<String>("ProjectName")
                            }
                           ,GrossAmount = rowAdvancePayment.Field<Decimal>("GrossAmount")
                           ,TDSRate = rowAdvancePayment.Field<Decimal>("TDSRate")
                           ,CGSTRate = rowAdvancePayment.Field<Decimal>("CGSTRate")
                           ,SGSTRate = rowAdvancePayment.Field<Decimal>("SGSTRate")
                           ,IGSTRate = rowAdvancePayment.Field<Decimal>("IGSTRate")
                           ,Remarks = rowAdvancePayment.Field<String>("Remarks")
                        };

            switch (sortOrder)
            {
                case "AdvancePaymentID_desc":
                    Query = Query.OrderByDescending(s => s.AdvancePaymentID);
                    break;
                case "AdvancePaymentID_asc":
                    Query = Query.OrderBy(s => s.AdvancePaymentID);
                    break;
                case "PaymentDate_desc":
                    Query = Query.OrderByDescending(s => s.PaymentDate);
                    break;
                case "PaymentDate_asc":
                    Query = Query.OrderBy(s => s.PaymentDate);
                    break;
                case "CompanyID_desc":
                    Query = Query.OrderByDescending(s => s.Company.CompanyName);
                    break;
                case "CompanyID_asc":
                    Query = Query.OrderBy(s => s.Company.CompanyName);
                    break;
                case "ClientID_desc":
                    Query = Query.OrderByDescending(s => s.Client.ClientName);
                    break;
                case "ClientID_asc":
                    Query = Query.OrderBy(s => s.Client.ClientName);
                    break;
                case "ProjectID_desc":
                    Query = Query.OrderByDescending(s => s.Project.ProjectName);
                    break;
                case "ProjectID_asc":
                    Query = Query.OrderBy(s => s.Project.ProjectName);
                    break;
                case "GrossAmount_desc":
                    Query = Query.OrderByDescending(s => s.GrossAmount);
                    break;
                case "GrossAmount_asc":
                    Query = Query.OrderBy(s => s.GrossAmount);
                    break;
                case "TDSRate_desc":
                    Query = Query.OrderByDescending(s => s.TDSRate);
                    break;
                case "TDSRate_asc":
                    Query = Query.OrderBy(s => s.TDSRate);
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
                case "Remarks_desc":
                    Query = Query.OrderByDescending(s => s.Remarks);
                    break;
                case "Remarks_asc":
                    Query = Query.OrderBy(s => s.Remarks);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.AdvancePaymentID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Advance Payment I D", typeof(string));
                dt.Columns.Add("Payment Date", typeof(string));
                dt.Columns.Add("Company I D", typeof(string));
                dt.Columns.Add("Client I D", typeof(string));
                dt.Columns.Add("Project I D", typeof(string));
                dt.Columns.Add("Gross Amount", typeof(string));
                dt.Columns.Add("T D S Rate", typeof(string));
                dt.Columns.Add("C G S T Rate", typeof(string));
                dt.Columns.Add("S G S T Rate", typeof(string));
                dt.Columns.Add("I G S T Rate", typeof(string));
                dt.Columns.Add("Remarks", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.AdvancePaymentID
                       ,item.PaymentDate
                       ,item.Company.CompanyName
                       ,item.Client.ClientName
                       ,item.Project.ProjectName
                       ,item.GrossAmount
                       ,item.TDSRate
                       ,item.CGSTRate
                       ,item.SGSTRate
                       ,item.IGSTRate
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

        // GET: /AdvancePayment/Details/<id>
        public ActionResult Details(
                                      Int32? AdvancePaymentID
                                   )
        {
            if (
                    AdvancePaymentID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtCompany = AdvancePayment_CompanyData.SelectAll();
            dtClient = AdvancePayment_ClientData.SelectAll();
            dtProject = AdvancePayment_ProjectData.SelectAll();

            AdvancePayment AdvancePayment = new AdvancePayment();
            AdvancePayment.AdvancePaymentID = System.Convert.ToInt32(AdvancePaymentID);
            AdvancePayment = AdvancePaymentData.Select_Record(AdvancePayment);
            AdvancePayment.Company = new Company()
            {
                CompanyID = (Int32)AdvancePayment.CompanyID
               ,CompanyName = (from DataRow rowCompany in dtCompany.Rows
                      where AdvancePayment.CompanyID == (int)rowCompany["CompanyID"]
                      select (String)rowCompany["CompanyName"]).FirstOrDefault()
            };
            AdvancePayment.Client = new Client()
            {
                ClientID = (Int32)AdvancePayment.ClientID
               ,ClientName = (from DataRow rowClient in dtClient.Rows
                      where AdvancePayment.ClientID == (int)rowClient["ClientID"]
                      select (String)rowClient["ClientName"]).FirstOrDefault()
            };
            AdvancePayment.Project = new Project()
            {
                ProjectID = (Int32)AdvancePayment.ProjectID
               ,ProjectName = (from DataRow rowProject in dtProject.Rows
                      where AdvancePayment.ProjectID == (int)rowProject["ProjectID"]
                      select (String)rowProject["ProjectName"]).FirstOrDefault()
            };

            if (AdvancePayment == null)
            {
                return HttpNotFound();
            }
            return View(AdvancePayment);
        }

        // GET: /AdvancePayment/Create
        public ActionResult Create()
        {
        // ComboBox
            ViewData["CompanyID"] = new SelectList(AdvancePayment_CompanyData.List(), "CompanyID", "CompanyName");
            ViewData["ClientID"] = new SelectList(AdvancePayment_ClientData.List(), "ClientID", "ClientName");
            ViewData["ProjectID"] = new SelectList(AdvancePayment_ProjectData.List(), "ProjectID", "ProjectName");

            return View();
        }

        // POST: /AdvancePayment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "PaymentDate"
				   + "," + "CompanyID"
				   + "," + "ClientID"
				   + "," + "ProjectID"
				   + "," + "GrossAmount"
				   + "," + "TDSRate"
				   + "," + "CGSTRate"
				   + "," + "SGSTRate"
				   + "," + "IGSTRate"
				   + "," + "Remarks"
				  )] AdvancePayment AdvancePayment)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = AdvancePaymentData.Add(AdvancePayment);
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
            ViewData["CompanyID"] = new SelectList(AdvancePayment_CompanyData.List(), "CompanyID", "CompanyName", AdvancePayment.CompanyID);
            ViewData["ClientID"] = new SelectList(AdvancePayment_ClientData.List(), "ClientID", "ClientName", AdvancePayment.ClientID);
            ViewData["ProjectID"] = new SelectList(AdvancePayment_ProjectData.List(), "ProjectID", "ProjectName", AdvancePayment.ProjectID);

            return View(AdvancePayment);
        }

        // GET: /AdvancePayment/Edit/<id>
        public ActionResult Edit(
                                   Int32? AdvancePaymentID
                                )
        {
            if (
                    AdvancePaymentID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AdvancePayment AdvancePayment = new AdvancePayment();
            AdvancePayment.AdvancePaymentID = System.Convert.ToInt32(AdvancePaymentID);
            AdvancePayment = AdvancePaymentData.Select_Record(AdvancePayment);

            if (AdvancePayment == null)
            {
                return HttpNotFound();
            }
        // ComboBox
            ViewData["CompanyID"] = new SelectList(AdvancePayment_CompanyData.List(), "CompanyID", "CompanyName", AdvancePayment.CompanyID);
            ViewData["ClientID"] = new SelectList(AdvancePayment_ClientData.List(), "ClientID", "ClientName", AdvancePayment.ClientID);
            ViewData["ProjectID"] = new SelectList(AdvancePayment_ProjectData.List(), "ProjectID", "ProjectName", AdvancePayment.ProjectID);

            return View(AdvancePayment);
        }

        // POST: /AdvancePayment/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdvancePayment AdvancePayment)
        {

            AdvancePayment oAdvancePayment = new AdvancePayment();
            oAdvancePayment.AdvancePaymentID = System.Convert.ToInt32(AdvancePayment.AdvancePaymentID);
            oAdvancePayment = AdvancePaymentData.Select_Record(AdvancePayment);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = AdvancePaymentData.Update(oAdvancePayment, AdvancePayment);
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
            ViewData["CompanyID"] = new SelectList(AdvancePayment_CompanyData.List(), "CompanyID", "CompanyName", AdvancePayment.CompanyID);
            ViewData["ClientID"] = new SelectList(AdvancePayment_ClientData.List(), "ClientID", "ClientName", AdvancePayment.ClientID);
            ViewData["ProjectID"] = new SelectList(AdvancePayment_ProjectData.List(), "ProjectID", "ProjectName", AdvancePayment.ProjectID);

            return View(AdvancePayment);
        }

        // GET: /AdvancePayment/Delete/<id>
        public ActionResult Delete(
                                     Int32? AdvancePaymentID
                                  )
        {
            if (
                    AdvancePaymentID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtCompany = AdvancePayment_CompanyData.SelectAll();
            dtClient = AdvancePayment_ClientData.SelectAll();
            dtProject = AdvancePayment_ProjectData.SelectAll();

            AdvancePayment AdvancePayment = new AdvancePayment();
            AdvancePayment.AdvancePaymentID = System.Convert.ToInt32(AdvancePaymentID);
            AdvancePayment = AdvancePaymentData.Select_Record(AdvancePayment);
            AdvancePayment.Company = new Company()
            {
                CompanyID = (Int32)AdvancePayment.CompanyID
               ,CompanyName = (from DataRow rowCompany in dtCompany.Rows
                      where AdvancePayment.CompanyID == (int)rowCompany["CompanyID"]
                      select (String)rowCompany["CompanyName"]).FirstOrDefault()
            };
            AdvancePayment.Client = new Client()
            {
                ClientID = (Int32)AdvancePayment.ClientID
               ,ClientName = (from DataRow rowClient in dtClient.Rows
                      where AdvancePayment.ClientID == (int)rowClient["ClientID"]
                      select (String)rowClient["ClientName"]).FirstOrDefault()
            };
            AdvancePayment.Project = new Project()
            {
                ProjectID = (Int32)AdvancePayment.ProjectID
               ,ProjectName = (from DataRow rowProject in dtProject.Rows
                      where AdvancePayment.ProjectID == (int)rowProject["ProjectID"]
                      select (String)rowProject["ProjectName"]).FirstOrDefault()
            };

            if (AdvancePayment == null)
            {
                return HttpNotFound();
            }
            return View(AdvancePayment);
        }

        // POST: /AdvancePayment/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? AdvancePaymentID
                                            )
        {

            AdvancePayment AdvancePayment = new AdvancePayment();
            AdvancePayment.AdvancePaymentID = System.Convert.ToInt32(AdvancePaymentID);
            AdvancePayment = AdvancePaymentData.Select_Record(AdvancePayment);

            bool bSucess = false;
            bSucess = AdvancePaymentData.Delete(AdvancePayment);
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
            SelectListItem Item1 = new SelectListItem { Text = "Advance Payment I D", Value = "Advance Payment I D" };
            SelectListItem Item2 = new SelectListItem { Text = "Payment Date", Value = "Payment Date" };
            SelectListItem Item3 = new SelectListItem { Text = "Company I D", Value = "Company I D" };
            SelectListItem Item4 = new SelectListItem { Text = "Client I D", Value = "Client I D" };
            SelectListItem Item5 = new SelectListItem { Text = "Project I D", Value = "Project I D" };
            SelectListItem Item6 = new SelectListItem { Text = "Gross Amount", Value = "Gross Amount" };
            SelectListItem Item7 = new SelectListItem { Text = "T D S Rate", Value = "T D S Rate" };
            SelectListItem Item8 = new SelectListItem { Text = "C G S T Rate", Value = "C G S T Rate" };
            SelectListItem Item9 = new SelectListItem { Text = "S G S T Rate", Value = "S G S T Rate" };
            SelectListItem Item10 = new SelectListItem { Text = "I G S T Rate", Value = "I G S T Rate" };
            SelectListItem Item11 = new SelectListItem { Text = "Remarks", Value = "Remarks" };

                 if (select == "Advance Payment I D") { Item1.Selected = true; }
            else if (select == "Payment Date") { Item2.Selected = true; }
            else if (select == "Company I D") { Item3.Selected = true; }
            else if (select == "Client I D") { Item4.Selected = true; }
            else if (select == "Project I D") { Item5.Selected = true; }
            else if (select == "Gross Amount") { Item6.Selected = true; }
            else if (select == "T D S Rate") { Item7.Selected = true; }
            else if (select == "C G S T Rate") { Item8.Selected = true; }
            else if (select == "S G S T Rate") { Item9.Selected = true; }
            else if (select == "I G S T Rate") { Item10.Selected = true; }
            else if (select == "Remarks") { Item11.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);
            list.Add(Item5);
            list.Add(Item6);
            list.Add(Item7);
            list.Add(Item8);
            list.Add(Item9);
            list.Add(Item10);
            list.Add(Item11);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Advance Payment", "Many");
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
 
