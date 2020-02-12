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
    public class CompanyController : Controller
    {

        DataTable dtCompany = new DataTable();

        // GET: /Company/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Company I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["CompanyIDSortParm"] = sortOrder == "CompanyID_asc" ? "CompanyID_desc" : "CompanyID_asc";
            ViewData["CompanyNameSortParm"] = sortOrder == "CompanyName_asc" ? "CompanyName_desc" : "CompanyName_asc";
            ViewData["Address1SortParm"] = sortOrder == "Address1_asc" ? "Address1_desc" : "Address1_asc";
            ViewData["Address2SortParm"] = sortOrder == "Address2_asc" ? "Address2_desc" : "Address2_asc";
            ViewData["CitySortParm"] = sortOrder == "City_asc" ? "City_desc" : "City_asc";
            ViewData["DistrictSortParm"] = sortOrder == "District_asc" ? "District_desc" : "District_asc";
            ViewData["StateSortParm"] = sortOrder == "State_asc" ? "State_desc" : "State_asc";
            ViewData["CountrySortParm"] = sortOrder == "Country_asc" ? "Country_desc" : "Country_asc";
            ViewData["PinCodeSortParm"] = sortOrder == "PinCode_asc" ? "PinCode_desc" : "PinCode_asc";
            ViewData["ContactNoSortParm"] = sortOrder == "ContactNo_asc" ? "ContactNo_desc" : "ContactNo_asc";
            ViewData["EMailSortParm"] = sortOrder == "EMail_asc" ? "EMail_desc" : "EMail_asc";
            ViewData["GSTINSortParm"] = sortOrder == "GSTIN_asc" ? "GSTIN_desc" : "GSTIN_asc";
            ViewData["InvoiceInitialsSortParm"] = sortOrder == "InvoiceInitials_asc" ? "InvoiceInitials_desc" : "InvoiceInitials_asc";
            ViewData["AddUserIDSortParm"] = sortOrder == "AddUserID_asc" ? "AddUserID_desc" : "AddUserID_asc";
            ViewData["AddDateSortParm"] = sortOrder == "AddDate_asc" ? "AddDate_desc" : "AddDate_asc";
            ViewData["ArchiveUserIDSortParm"] = sortOrder == "ArchiveUserID_asc" ? "ArchiveUserID_desc" : "ArchiveUserID_asc";
            ViewData["ArchiveDateSortParm"] = sortOrder == "ArchiveDate_asc" ? "ArchiveDate_desc" : "ArchiveDate_asc";

            dtCompany = CompanyData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtCompany = CompanyData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowCompany in dtCompany.AsEnumerable()
                        select new Company() {
                            CompanyID = rowCompany.Field<Int32>("CompanyID")
                           ,CompanyName = rowCompany.Field<String>("CompanyName")
                           ,Address1 = rowCompany.Field<String>("Address1")
                           ,Address2 = rowCompany.Field<String>("Address2")
                           ,City = rowCompany.Field<String>("City")
                           ,District = rowCompany.Field<String>("District")
                           ,State = rowCompany.Field<String>("State")
                           ,Country = rowCompany.Field<String>("Country")
                           ,PinCode = rowCompany.Field<String>("PinCode")
                           ,ContactNo = rowCompany.Field<String>("ContactNo")
                           ,EMail = rowCompany.Field<String>("EMail")
                           ,GSTIN = rowCompany.Field<String>("GSTIN")
                           ,InvoiceInitials = rowCompany.Field<String>("InvoiceInitials")
                           ,AddUserID = rowCompany.Field<Int32?>("AddUserID")
                           ,AddDate = rowCompany.Field<DateTime?>("AddDate")
                           ,ArchiveUserID = rowCompany.Field<Int32?>("ArchiveUserID")
                           ,ArchiveDate = rowCompany.Field<DateTime?>("ArchiveDate")
                        };

            switch (sortOrder)
            {
                case "CompanyID_desc":
                    Query = Query.OrderByDescending(s => s.CompanyID);
                    break;
                case "CompanyID_asc":
                    Query = Query.OrderBy(s => s.CompanyID);
                    break;
                case "CompanyName_desc":
                    Query = Query.OrderByDescending(s => s.CompanyName);
                    break;
                case "CompanyName_asc":
                    Query = Query.OrderBy(s => s.CompanyName);
                    break;
                case "Address1_desc":
                    Query = Query.OrderByDescending(s => s.Address1);
                    break;
                case "Address1_asc":
                    Query = Query.OrderBy(s => s.Address1);
                    break;
                case "Address2_desc":
                    Query = Query.OrderByDescending(s => s.Address2);
                    break;
                case "Address2_asc":
                    Query = Query.OrderBy(s => s.Address2);
                    break;
                case "City_desc":
                    Query = Query.OrderByDescending(s => s.City);
                    break;
                case "City_asc":
                    Query = Query.OrderBy(s => s.City);
                    break;
                case "District_desc":
                    Query = Query.OrderByDescending(s => s.District);
                    break;
                case "District_asc":
                    Query = Query.OrderBy(s => s.District);
                    break;
                case "State_desc":
                    Query = Query.OrderByDescending(s => s.State);
                    break;
                case "State_asc":
                    Query = Query.OrderBy(s => s.State);
                    break;
                case "Country_desc":
                    Query = Query.OrderByDescending(s => s.Country);
                    break;
                case "Country_asc":
                    Query = Query.OrderBy(s => s.Country);
                    break;
                case "PinCode_desc":
                    Query = Query.OrderByDescending(s => s.PinCode);
                    break;
                case "PinCode_asc":
                    Query = Query.OrderBy(s => s.PinCode);
                    break;
                case "ContactNo_desc":
                    Query = Query.OrderByDescending(s => s.ContactNo);
                    break;
                case "ContactNo_asc":
                    Query = Query.OrderBy(s => s.ContactNo);
                    break;
                case "EMail_desc":
                    Query = Query.OrderByDescending(s => s.EMail);
                    break;
                case "EMail_asc":
                    Query = Query.OrderBy(s => s.EMail);
                    break;
                case "GSTIN_desc":
                    Query = Query.OrderByDescending(s => s.GSTIN);
                    break;
                case "GSTIN_asc":
                    Query = Query.OrderBy(s => s.GSTIN);
                    break;
                case "InvoiceInitials_desc":
                    Query = Query.OrderByDescending(s => s.InvoiceInitials);
                    break;
                case "InvoiceInitials_asc":
                    Query = Query.OrderBy(s => s.InvoiceInitials);
                    break;
                case "AddUserID_desc":
                    Query = Query.OrderByDescending(s => s.AddUserID);
                    break;
                case "AddUserID_asc":
                    Query = Query.OrderBy(s => s.AddUserID);
                    break;
                case "AddDate_desc":
                    Query = Query.OrderByDescending(s => s.AddDate);
                    break;
                case "AddDate_asc":
                    Query = Query.OrderBy(s => s.AddDate);
                    break;
                case "ArchiveUserID_desc":
                    Query = Query.OrderByDescending(s => s.ArchiveUserID);
                    break;
                case "ArchiveUserID_asc":
                    Query = Query.OrderBy(s => s.ArchiveUserID);
                    break;
                case "ArchiveDate_desc":
                    Query = Query.OrderByDescending(s => s.ArchiveDate);
                    break;
                case "ArchiveDate_asc":
                    Query = Query.OrderBy(s => s.ArchiveDate);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.CompanyID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Company I D", typeof(string));
                dt.Columns.Add("Company Name", typeof(string));
                dt.Columns.Add("Address1", typeof(string));
                dt.Columns.Add("Address2", typeof(string));
                dt.Columns.Add("City", typeof(string));
                dt.Columns.Add("District", typeof(string));
                dt.Columns.Add("State", typeof(string));
                dt.Columns.Add("Country", typeof(string));
                dt.Columns.Add("Pin Code", typeof(string));
                dt.Columns.Add("Contact No", typeof(string));
                dt.Columns.Add("E Mail", typeof(string));
                dt.Columns.Add("G S T I N", typeof(string));
                dt.Columns.Add("Invoice Initials", typeof(string));
                dt.Columns.Add("Add User I D", typeof(string));
                dt.Columns.Add("Add Date", typeof(string));
                dt.Columns.Add("Archive User I D", typeof(string));
                dt.Columns.Add("Archive Date", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.CompanyID
                       ,item.CompanyName
                       ,item.Address1
                       ,item.Address2
                       ,item.City
                       ,item.District
                       ,item.State
                       ,item.Country
                       ,item.PinCode
                       ,item.ContactNo
                       ,item.EMail
                       ,item.GSTIN
                       ,item.InvoiceInitials
                       ,item.AddUserID
                       ,item.AddDate
                       ,item.ArchiveUserID
                       ,item.ArchiveDate
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

        // GET: /Company/Details/<id>
        public ActionResult Details(
                                      Int32? CompanyID
                                   )
        {
            if (
                    CompanyID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Company Company = new Company();
            Company.CompanyID = System.Convert.ToInt32(CompanyID);
            Company = CompanyData.Select_Record(Company);

            if (Company == null)
            {
                return HttpNotFound();
            }
            return View(Company);
        }

        // GET: /Company/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /Company/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "CompanyName"
				   + "," + "Address1"
				   + "," + "Address2"
				   + "," + "City"
				   + "," + "District"
				   + "," + "State"
				   + "," + "Country"
				   + "," + "PinCode"
				   + "," + "ContactNo"
				   + "," + "EMail"
				   + "," + "GSTIN"
				   + "," + "InvoiceInitials"
				   + "," + "AddUserID"
				   + "," + "AddDate"
				   + "," + "ArchiveUserID"
				   + "," + "ArchiveDate"
				  )] Company Company)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = CompanyData.Add(Company);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(Company);
        }

        // GET: /Company/Edit/<id>
        public ActionResult Edit(
                                   Int32? CompanyID
                                )
        {
            if (
                    CompanyID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Company Company = new Company();
            Company.CompanyID = System.Convert.ToInt32(CompanyID);
            Company = CompanyData.Select_Record(Company);

            if (Company == null)
            {
                return HttpNotFound();
            }

            return View(Company);
        }

        // POST: /Company/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company Company)
        {

            Company oCompany = new Company();
            oCompany.CompanyID = System.Convert.ToInt32(Company.CompanyID);
            oCompany = CompanyData.Select_Record(Company);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = CompanyData.Update(oCompany, Company);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(Company);
        }

        // GET: /Company/Delete/<id>
        public ActionResult Delete(
                                     Int32? CompanyID
                                  )
        {
            if (
                    CompanyID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Company Company = new Company();
            Company.CompanyID = System.Convert.ToInt32(CompanyID);
            Company = CompanyData.Select_Record(Company);

            if (Company == null)
            {
                return HttpNotFound();
            }
            return View(Company);
        }

        // POST: /Company/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? CompanyID
                                            )
        {

            Company Company = new Company();
            Company.CompanyID = System.Convert.ToInt32(CompanyID);
            Company = CompanyData.Select_Record(Company);

            bool bSucess = false;
            bSucess = CompanyData.Delete(Company);
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
            SelectListItem Item1 = new SelectListItem { Text = "Company I D", Value = "Company I D" };
            SelectListItem Item2 = new SelectListItem { Text = "Company Name", Value = "Company Name" };
            SelectListItem Item3 = new SelectListItem { Text = "Address1", Value = "Address1" };
            SelectListItem Item4 = new SelectListItem { Text = "Address2", Value = "Address2" };
            SelectListItem Item5 = new SelectListItem { Text = "City", Value = "City" };
            SelectListItem Item6 = new SelectListItem { Text = "District", Value = "District" };
            SelectListItem Item7 = new SelectListItem { Text = "State", Value = "State" };
            SelectListItem Item8 = new SelectListItem { Text = "Country", Value = "Country" };
            SelectListItem Item9 = new SelectListItem { Text = "Pin Code", Value = "Pin Code" };
            SelectListItem Item10 = new SelectListItem { Text = "Contact No", Value = "Contact No" };
            SelectListItem Item11 = new SelectListItem { Text = "E Mail", Value = "E Mail" };
            SelectListItem Item12 = new SelectListItem { Text = "G S T I N", Value = "G S T I N" };
            SelectListItem Item13 = new SelectListItem { Text = "Invoice Initials", Value = "Invoice Initials" };
            SelectListItem Item14 = new SelectListItem { Text = "Add User I D", Value = "Add User I D" };
            SelectListItem Item15 = new SelectListItem { Text = "Add Date", Value = "Add Date" };
            SelectListItem Item16 = new SelectListItem { Text = "Archive User I D", Value = "Archive User I D" };
            SelectListItem Item17 = new SelectListItem { Text = "Archive Date", Value = "Archive Date" };

                 if (select == "Company I D") { Item1.Selected = true; }
            else if (select == "Company Name") { Item2.Selected = true; }
            else if (select == "Address1") { Item3.Selected = true; }
            else if (select == "Address2") { Item4.Selected = true; }
            else if (select == "City") { Item5.Selected = true; }
            else if (select == "District") { Item6.Selected = true; }
            else if (select == "State") { Item7.Selected = true; }
            else if (select == "Country") { Item8.Selected = true; }
            else if (select == "Pin Code") { Item9.Selected = true; }
            else if (select == "Contact No") { Item10.Selected = true; }
            else if (select == "E Mail") { Item11.Selected = true; }
            else if (select == "G S T I N") { Item12.Selected = true; }
            else if (select == "Invoice Initials") { Item13.Selected = true; }
            else if (select == "Add User I D") { Item14.Selected = true; }
            else if (select == "Add Date") { Item15.Selected = true; }
            else if (select == "Archive User I D") { Item16.Selected = true; }
            else if (select == "Archive Date") { Item17.Selected = true; }

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
            list.Add(Item12);
            list.Add(Item13);
            list.Add(Item14);
            list.Add(Item15);
            list.Add(Item16);
            list.Add(Item17);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Company", "Many");
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
 
