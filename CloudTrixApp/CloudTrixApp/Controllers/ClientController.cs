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
    public class ClientController : Controller
    {

        DataTable dtClient = new DataTable();
        DataTable dtCompany = new DataTable();
        DataTable dtRolePermission = new DataTable();
        int FormID = 2;

        // GET: /Client/
        public ActionResult Index(string sortOrder,
                                  String SearchField,
                                  String SearchCondition,
                                  String SearchText,
                                  String Export,
                                  int? PageSize,
                                  int? page,
                                  string command)
        {

            if (command == "Show All")
            {
                SearchField = null;
                SearchCondition = null;
                SearchText = null;
                Session["SearchField"] = null;
                Session["SearchCondition"] = null;
                Session["SearchText"] = null;
            }
            else if (command == "Add New Record") { return RedirectToAction("Create"); }
            else if (command == "Export") { Session["Export"] = Export; }
            else if (command == "Search" | command == "Page Size")
            {
                if (!string.IsNullOrEmpty(SearchText))
                {
                    Session["SearchField"] = SearchField;
                    Session["SearchCondition"] = SearchCondition;
                    Session["SearchText"] = SearchText;
                }
            }
            if (command == "Page Size") { Session["PageSize"] = PageSize; }

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Client I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["ClientIDSortParm"] = sortOrder == "ClientID_asc" ? "ClientID_desc" : "ClientID_asc";
            ViewData["ClientNameSortParm"] = sortOrder == "ClientName_asc" ? "ClientName_desc" : "ClientName_asc";
            ViewData["Address1SortParm"] = sortOrder == "Address1_asc" ? "Address1_desc" : "Address1_asc";
            ViewData["Address2SortParm"] = sortOrder == "Address2_asc" ? "Address2_desc" : "Address2_asc";
            ViewData["CitySortParm"] = sortOrder == "City_asc" ? "City_desc" : "City_asc";
            ViewData["DistrictSortParm"] = sortOrder == "District_asc" ? "District_desc" : "District_asc";
            ViewData["StateSortParm"] = sortOrder == "State_asc" ? "State_desc" : "State_asc";
            ViewData["PinCodeSortParm"] = sortOrder == "PinCode_asc" ? "PinCode_desc" : "PinCode_asc";
            ViewData["ContactNoSortParm"] = sortOrder == "ContactNo_asc" ? "ContactNo_desc" : "ContactNo_asc";
            ViewData["EMailSortParm"] = sortOrder == "EMail_asc" ? "EMail_desc" : "EMail_asc";
            ViewData["GSTINSortParm"] = sortOrder == "GSTIN_asc" ? "GSTIN_desc" : "GSTIN_asc";
            ViewData["CompanyIDSortParm"] = sortOrder == "CompanyID_asc" ? "CompanyID_desc" : "CompanyID_asc";
            ViewData["AddUserIDSortParm"] = sortOrder == "AddUserID_asc" ? "AddUserID_desc" : "AddUserID_asc";
            ViewData["AddDateSortParm"] = sortOrder == "AddDate_asc" ? "AddDate_desc" : "AddDate_asc";
            ViewData["ArchiveUserIDSortParm"] = sortOrder == "ArchiveUserID_asc" ? "ArchiveUserID_desc" : "ArchiveUserID_asc";
            ViewData["ArchiveDateSortParm"] = sortOrder == "ArchiveDate_asc" ? "ArchiveDate_desc" : "ArchiveDate_asc";

            dtClient = ClientData.SelectAll();
            dtCompany = Client_CompanyData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtClient = ClientData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowClient in dtClient.AsEnumerable()
                        join rowCompany in dtCompany.AsEnumerable() on rowClient.Field<Int32>("CompanyID") equals rowCompany.Field<Int32>("CompanyID")
                        select new Client()
                        {
                            ClientID = rowClient.Field<Int32>("ClientID")
                            ,
                            ClientName = rowClient.Field<String>("ClientName")
                            ,
                            Address1 = rowClient.Field<String>("Address1")
                            ,
                            Address2 = rowClient.Field<String>("Address2")
                            ,
                            City = rowClient.Field<String>("City")
                            ,
                            District = rowClient.Field<String>("District")
                            ,
                            State = rowClient.Field<String>("State")
                            ,
                            PinCode = rowClient.Field<String>("PinCode")
                            ,
                            ContactNo = rowClient.Field<String>("ContactNo")
                            ,
                            EMail = rowClient.Field<String>("EMail")
                            ,
                            GSTIN = rowClient.Field<String>("GSTIN")
                            ,
                            Company = new Company()
                            {
                                CompanyID = rowCompany.Field<Int32>("CompanyID")
                                  ,
                                CompanyName = rowCompany.Field<String>("CompanyName")
                            }
                            ,
                            AddUserID = rowClient.Field<Int32>("AddUserID")
                            ,
                            AddDate = rowClient.Field<DateTime>("AddDate")
                            ,
                            ArchiveUserID = rowClient.Field<Int32?>("ArchiveUserID")
                            ,
                            ArchiveDate = rowClient.Field<DateTime?>("ArchiveDate")
                        };

            switch (sortOrder)
            {
                case "ClientID_desc":
                    Query = Query.OrderByDescending(s => s.ClientID);
                    break;
                case "ClientID_asc":
                    Query = Query.OrderBy(s => s.ClientID);
                    break;
                case "ClientName_desc":
                    Query = Query.OrderByDescending(s => s.ClientName);
                    break;
                case "ClientName_asc":
                    Query = Query.OrderBy(s => s.ClientName);
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
                case "CompanyID_desc":
                    Query = Query.OrderByDescending(s => s.Company.CompanyName);
                    break;
                case "CompanyID_asc":
                    Query = Query.OrderBy(s => s.Company.CompanyName);
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
                    Query = Query.OrderBy(s => s.ClientID);
                    break;
            }

            if (command == "Export")
            {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Client I D", typeof(string));
                dt.Columns.Add("Client Name", typeof(string));
                dt.Columns.Add("Address1", typeof(string));
                dt.Columns.Add("Address2", typeof(string));
                dt.Columns.Add("City", typeof(string));
                dt.Columns.Add("District", typeof(string));
                dt.Columns.Add("State", typeof(string));
                dt.Columns.Add("Pin Code", typeof(string));
                dt.Columns.Add("Contact No", typeof(string));
                dt.Columns.Add("E Mail", typeof(string));
                dt.Columns.Add("G S T I N", typeof(string));
                dt.Columns.Add("Company I D", typeof(string));
                dt.Columns.Add("Add User I D", typeof(string));
                dt.Columns.Add("Add Date", typeof(string));
                dt.Columns.Add("Archive User I D", typeof(string));
                dt.Columns.Add("Archive Date", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.ClientID
                       , item.ClientName
                       , item.Address1
                       , item.Address2
                       , item.City
                       , item.District
                       , item.State
                       , item.PinCode
                       , item.ContactNo
                       , item.EMail
                       , item.GSTIN
                       , item.Company.CompanyName
                       , item.AddUserID
                       , item.AddDate
                       , item.ArchiveUserID
                       , item.ArchiveDate
                    );
                }
                gv.DataSource = dt;
                gv.DataBind();
                ExportData(Export, gv, dt);
            }

            int pageNumber = (page ?? 1);
            int? pageSZ = (Convert.ToInt32(Session["PageSize"]) == 0 ? 5 : Convert.ToInt32(Session["PageSize"]));

            //Role Permission
            
            int EmployeeID = 0;
            if (User.Identity.Name.Contains("|"))
                EmployeeID = Convert.ToInt32(User.Identity.Name.Split('|')[1]);

            dtRolePermission = ClientData.Role_Permission(EmployeeID, FormID);
            DataRow[] rows = dtRolePermission.Select();
            if (dtRolePermission != null)
            {   
                //AddPermission
                if (rows[0]["AddPermission"].ToString() == "0" || rows[0]["AddPermission"].ToString() == "False")
                { ViewData["Addbutton"] = "0"; }
                else
                { ViewData["Addbutton"] = "1"; }

                //UpdatePermission
                if (rows[0]["UpdatePermission"].ToString() == "0" || rows[0]["UpdatePermission"].ToString() == "False")
                { ViewData["Updatebutton"] = "0"; }
                else
                { ViewData["Updatebutton"] = "1"; }

                //ViewPermission
                if (rows[0]["ViewPermission"].ToString() == "0" || rows[0]["ViewPermission"].ToString() == "False")
                { ViewData["ViewPermission"] = "0"; }
                else
                { ViewData["ViewPermission"] = "1"; }

                //DeletePermission
                if (rows[0]["DeletePermission"].ToString() == "0" || rows[0]["DeletePermission"].ToString() == "False")
                { ViewData["Deletebutton"] = "0"; }
                else
                { ViewData["Deletebutton"] = "1"; }
            }
            return View(Query.ToPagedList(pageNumber, (pageSZ ?? 5)));
        }

        // GET: /Client/Details/<id>
        public ActionResult Details(
                                      Int32? ClientID
                                   )
        {
            if (
                    ClientID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtCompany = Client_CompanyData.SelectAll();

            Client Client = new Client();
            Client.ClientID = System.Convert.ToInt32(ClientID);
            Client = ClientData.Select_Record(Client);
            Client.Company = new Company()
            {
                CompanyID = (Int32)Client.CompanyID
               ,
                CompanyName = (from DataRow rowCompany in dtCompany.Rows
                               where Client.CompanyID == (int)rowCompany["CompanyID"]
                               select (String)rowCompany["CompanyName"]).FirstOrDefault()
            };

            if (Client == null)
            {
                return HttpNotFound();
            }
            return View(Client);
        }



        // GET: /Client/Create
        public ActionResult Create()
        {
            // ComboBox
            ViewData["CompanyID"] = new SelectList(Client_CompanyData.List(), "CompanyID", "CompanyName");

            return View();
        }

        // POST: /Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include =
                           "ClientName"
                   + "," + "Address1"
                   + "," + "Address2"
                   + "," + "City"
                   + "," + "District"
                   + "," + "State"
                   + "," + "PinCode"
                   + "," + "ContactNo"
                   + "," + "EMail"
                   + "," + "GSTIN"
                   + "," + "CompanyID"
                   + "," + "AddUserID"
                   + "," + "AddDate"
                   + "," + "ArchiveUserID"
                   + "," + "ArchiveDate"
                  )] Client Client)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = ClientData.Add(Client);
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
            ViewData["CompanyID"] = new SelectList(Client_CompanyData.List(), "CompanyID", "CompanyName", Client.CompanyID);

            return View(Client);
        }

        // GET: /Client/Edit/<id>
        public ActionResult Edit(
                                   Int32? ClientID
                                )
        {
            if (
                    ClientID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client Client = new Client();
            Client.ClientID = System.Convert.ToInt32(ClientID);
            Client = ClientData.Select_Record(Client);

            if (Client == null)
            {
                return HttpNotFound();
            }
            // ComboBox
            ViewData["CompanyID"] = new SelectList(Client_CompanyData.List(), "CompanyID", "CompanyName", Client.CompanyID);

            return View(Client);
        }

        // POST: /Client/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Client Client)
        {

            Client oClient = new Client();
            oClient.ClientID = System.Convert.ToInt32(Client.ClientID);
            oClient = ClientData.Select_Record(Client);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = ClientData.Update(oClient, Client);
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
            ViewData["CompanyID"] = new SelectList(Client_CompanyData.List(), "CompanyID", "CompanyName", Client.CompanyID);

            return View(Client);
        }

        // GET: /Client/Delete/<id>
        public ActionResult Delete(
                                     Int32? ClientID
                                  )
        {
            if (
                    ClientID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtCompany = Client_CompanyData.SelectAll();

            Client Client = new Client();
            Client.ClientID = System.Convert.ToInt32(ClientID);
            Client = ClientData.Select_Record(Client);
            Client.Company = new Company()
            {
                CompanyID = (Int32)Client.CompanyID
               ,
                CompanyName = (from DataRow rowCompany in dtCompany.Rows
                               where Client.CompanyID == (int)rowCompany["CompanyID"]
                               select (String)rowCompany["CompanyName"]).FirstOrDefault()
            };

            if (Client == null)
            {
                return HttpNotFound();
            }
            return View(Client);
        }

        // POST: /Client/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? ClientID
                                            )
        {

            Client Client = new Client();
            Client.ClientID = System.Convert.ToInt32(ClientID);
            Client = ClientData.Select_Record(Client);

            bool bSucess = false;
            bSucess = ClientData.Delete(Client);
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
            SelectListItem Item1 = new SelectListItem { Text = "Client I D", Value = "Client I D" };
            SelectListItem Item2 = new SelectListItem { Text = "Client Name", Value = "Client Name" };
            SelectListItem Item3 = new SelectListItem { Text = "Address1", Value = "Address1" };
            SelectListItem Item4 = new SelectListItem { Text = "Address2", Value = "Address2" };
            SelectListItem Item5 = new SelectListItem { Text = "City", Value = "City" };
            SelectListItem Item6 = new SelectListItem { Text = "District", Value = "District" };
            SelectListItem Item7 = new SelectListItem { Text = "State", Value = "State" };
            SelectListItem Item8 = new SelectListItem { Text = "Pin Code", Value = "Pin Code" };
            SelectListItem Item9 = new SelectListItem { Text = "Contact No", Value = "Contact No" };
            SelectListItem Item10 = new SelectListItem { Text = "E Mail", Value = "E Mail" };
            SelectListItem Item11 = new SelectListItem { Text = "G S T I N", Value = "G S T I N" };
            SelectListItem Item12 = new SelectListItem { Text = "Company I D", Value = "Company I D" };
            SelectListItem Item13 = new SelectListItem { Text = "Add User I D", Value = "Add User I D" };
            SelectListItem Item14 = new SelectListItem { Text = "Add Date", Value = "Add Date" };
            SelectListItem Item15 = new SelectListItem { Text = "Archive User I D", Value = "Archive User I D" };
            SelectListItem Item16 = new SelectListItem { Text = "Archive Date", Value = "Archive Date" };

            if (select == "Client I D") { Item1.Selected = true; }
            else if (select == "Client Name") { Item2.Selected = true; }
            else if (select == "Address1") { Item3.Selected = true; }
            else if (select == "Address2") { Item4.Selected = true; }
            else if (select == "City") { Item5.Selected = true; }
            else if (select == "District") { Item6.Selected = true; }
            else if (select == "State") { Item7.Selected = true; }
            else if (select == "Pin Code") { Item8.Selected = true; }
            else if (select == "Contact No") { Item9.Selected = true; }
            else if (select == "E Mail") { Item10.Selected = true; }
            else if (select == "G S T I N") { Item11.Selected = true; }
            else if (select == "Company I D") { Item12.Selected = true; }
            else if (select == "Add User I D") { Item13.Selected = true; }
            else if (select == "Add Date") { Item14.Selected = true; }
            else if (select == "Archive User I D") { Item15.Selected = true; }
            else if (select == "Archive Date") { Item16.Selected = true; }

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

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Client", "Many");
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

