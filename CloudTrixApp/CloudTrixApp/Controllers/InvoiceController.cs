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
    public class InvoiceController : Controller
    {

        DataTable dtInvoice = new DataTable();
        DataTable dtProject = new DataTable();
        DataTable dtClient = new DataTable();
        DataTable dtCompany = new DataTable();

        // GET: /Invoice/
        //public ActionResult Index(string sortOrder,  
        //                          String SearchField,
        //                          String SearchCondition,
        //                          String SearchText,
        //                          String Export,
        //                          int? PageSize,
        //                          int? page, 
        //                          string command)
        //{

        //    if (command == "Show All") {
        //        SearchField = null;
        //        SearchCondition = null;
        //        SearchText = null;
        //        Session["SearchField"] = null;
        //        Session["SearchCondition"] = null;
        //        Session["SearchText"] = null; } 
        //    else if (command == "Add New Record") { return RedirectToAction("Create"); } 
        //    else if (command == "Export") { Session["Export"] = Export; } 
        //    else if (command == "Search" | command == "Page Size") {
        //        if (!string.IsNullOrEmpty(SearchText)) {
        //            Session["SearchField"] = SearchField;
        //            Session["SearchCondition"] = SearchCondition;
        //            Session["SearchText"] = SearchText; }
        //        } 
        //    if (command == "Page Size") { Session["PageSize"] = PageSize; }

        //    ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Invoice I D" : Convert.ToString(Session["SearchField"])));
        //    ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
        //    ViewData["SearchText"] = Session["SearchText"];
        //    ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
        //    ViewData["PageSizes"] = Library.GetPageSizes();

        //    ViewData["CurrentSort"] = sortOrder;
        //    ViewData["InvoiceIDSortParm"] = sortOrder == "InvoiceID_asc" ? "InvoiceID_desc" : "InvoiceID_asc";
        //    ViewData["InvoiceNoSortParm"] = sortOrder == "InvoiceNo_asc" ? "InvoiceNo_desc" : "InvoiceNo_asc";
        //    ViewData["InvoiceDateSortParm"] = sortOrder == "InvoiceDate_asc" ? "InvoiceDate_desc" : "InvoiceDate_asc";
        //    ViewData["ProjectIDSortParm"] = sortOrder == "ProjectID_asc" ? "ProjectID_desc" : "ProjectID_asc";
        //    ViewData["ClientIDSortParm"] = sortOrder == "ClientID_asc" ? "ClientID_desc" : "ClientID_asc";
        //    ViewData["ClientNameSortParm"] = sortOrder == "ClientName_asc" ? "ClientName_desc" : "ClientName_asc";
        //    ViewData["ClientAddressSortParm"] = sortOrder == "ClientAddress_asc" ? "ClientAddress_desc" : "ClientAddress_asc";
        //    ViewData["ClientGSTINSortParm"] = sortOrder == "ClientGSTIN_asc" ? "ClientGSTIN_desc" : "ClientGSTIN_asc";
        //    ViewData["ClientContactNoSortParm"] = sortOrder == "ClientContactNo_asc" ? "ClientContactNo_desc" : "ClientContactNo_asc";
        //    ViewData["ClientEMailSortParm"] = sortOrder == "ClientEMail_asc" ? "ClientEMail_desc" : "ClientEMail_asc";
        //    ViewData["AdditionalDiscountSortParm"] = sortOrder == "AdditionalDiscount_asc" ? "AdditionalDiscount_desc" : "AdditionalDiscount_asc";
        //    ViewData["RemarksSortParm"] = sortOrder == "Remarks_asc" ? "Remarks_desc" : "Remarks_asc";
        //    ViewData["PDFUrlSortParm"] = sortOrder == "PDFUrl_asc" ? "PDFUrl_desc" : "PDFUrl_asc";
        //    ViewData["CompanyIDSortParm"] = sortOrder == "CompanyID_asc" ? "CompanyID_desc" : "CompanyID_asc";
        //    ViewData["AddUserIDSortParm"] = sortOrder == "AddUserID_asc" ? "AddUserID_desc" : "AddUserID_asc";
        //    ViewData["AddDateSortParm"] = sortOrder == "AddDate_asc" ? "AddDate_desc" : "AddDate_asc";
        //    ViewData["ArchiveUserIDSortParm"] = sortOrder == "ArchiveUserID_asc" ? "ArchiveUserID_desc" : "ArchiveUserID_asc";
        //    ViewData["ArchiveDateSortParm"] = sortOrder == "ArchiveDate_asc" ? "ArchiveDate_desc" : "ArchiveDate_asc";

        //    dtInvoice = InvoiceData.SelectAll();
        //    dtProject = Invoice_ProjectData.SelectAll();
        //    dtClient = Invoice_ClientData.SelectAll();
        //    dtCompany = Invoice_CompanyData.SelectAll();

        //    try
        //    {
        //        if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
        //        {
        //            dtInvoice = InvoiceData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
        //        }
        //    }
        //    catch { }

        //    var Query = from rowInvoice in dtInvoice.AsEnumerable()
        //                join rowProject in dtProject.AsEnumerable() on rowInvoice.Field<Int32?>("ProjectID") equals rowProject.Field<Int32>("ProjectID")
        //                join rowClient in dtClient.AsEnumerable() on rowInvoice.Field<Int32>("ClientID") equals rowClient.Field<Int32>("ClientID")
        //                join rowCompany in dtCompany.AsEnumerable() on rowInvoice.Field<Int32?>("CompanyID") equals rowCompany.Field<Int32>("CompanyID")
        //                select new Invoice() {
        //                    InvoiceID = rowInvoice.Field<Int32>("InvoiceID")
        //                   ,InvoiceNo = rowInvoice.Field<String>("InvoiceNo")
        //                   ,InvoiceDate = rowInvoice.Field<DateTime>("InvoiceDate")
        //                   ,
        //                    Project = new Project() 
        //                    {
        //                           ProjectID = rowProject.Field<Int32>("ProjectID")
        //                          ,ProjectName = rowProject.Field<String>("ProjectName")
        //                    }
        //                   ,
        //                    Client = new Client() 
        //                    {
        //                           ClientID = rowClient.Field<Int32>("ClientID")
        //                          ,ClientName = rowClient.Field<String>("ClientName")
        //                    }
        //                   ,ClientName = rowInvoice.Field<String>("ClientName")
        //                   ,ClientAddress = rowInvoice.Field<String>("ClientAddress")
        //                   ,ClientGSTIN = rowInvoice.Field<String>("ClientGSTIN")
        //                   ,ClientContactNo = rowInvoice.Field<String>("ClientContactNo")
        //                   ,ClientEMail = rowInvoice.Field<String>("ClientEMail")
        //                   ,AdditionalDiscount = rowInvoice.Field<Decimal?>("AdditionalDiscount")
        //                   ,Remarks = rowInvoice.Field<String>("Remarks")
        //                   ,PDFUrl = rowInvoice.Field<String>("PDFUrl")
        //                   ,
        //                    Company = new Company() 
        //                    {
        //                           CompanyID = rowCompany.Field<Int32>("CompanyID")
        //                          ,CompanyName = rowCompany.Field<String>("CompanyName")
        //                    }
        //                   ,AddUserID = rowInvoice.Field<Int32?>("AddUserID")
        //                   ,AddDate = rowInvoice.Field<DateTime?>("AddDate")
        //                   ,ArchiveUserID = rowInvoice.Field<Int32?>("ArchiveUserID")
        //                   ,ArchiveDate = rowInvoice.Field<DateTime?>("ArchiveDate")
        //                };

        //    switch (sortOrder)
        //    {
        //        case "InvoiceID_desc":
        //            Query = Query.OrderByDescending(s => s.InvoiceID);
        //            break;
        //        case "InvoiceID_asc":
        //            Query = Query.OrderBy(s => s.InvoiceID);
        //            break;
        //        case "InvoiceNo_desc":
        //            Query = Query.OrderByDescending(s => s.InvoiceNo);
        //            break;
        //        case "InvoiceNo_asc":
        //            Query = Query.OrderBy(s => s.InvoiceNo);
        //            break;
        //        case "InvoiceDate_desc":
        //            Query = Query.OrderByDescending(s => s.InvoiceDate);
        //            break;
        //        case "InvoiceDate_asc":
        //            Query = Query.OrderBy(s => s.InvoiceDate);
        //            break;
        //        case "ProjectID_desc":
        //            Query = Query.OrderByDescending(s => s.Project.ProjectName);
        //            break;
        //        case "ProjectID_asc":
        //            Query = Query.OrderBy(s => s.Project.ProjectName);
        //            break;
        //        case "ClientID_desc":
        //            Query = Query.OrderByDescending(s => s.Client.ClientName);
        //            break;
        //        case "ClientID_asc":
        //            Query = Query.OrderBy(s => s.Client.ClientName);
        //            break;
        //        case "ClientName_desc":
        //            Query = Query.OrderByDescending(s => s.ClientName);
        //            break;
        //        case "ClientName_asc":
        //            Query = Query.OrderBy(s => s.ClientName);
        //            break;
        //        case "ClientAddress_desc":
        //            Query = Query.OrderByDescending(s => s.ClientAddress);
        //            break;
        //        case "ClientAddress_asc":
        //            Query = Query.OrderBy(s => s.ClientAddress);
        //            break;
        //        case "ClientGSTIN_desc":
        //            Query = Query.OrderByDescending(s => s.ClientGSTIN);
        //            break;
        //        case "ClientGSTIN_asc":
        //            Query = Query.OrderBy(s => s.ClientGSTIN);
        //            break;
        //        case "ClientContactNo_desc":
        //            Query = Query.OrderByDescending(s => s.ClientContactNo);
        //            break;
        //        case "ClientContactNo_asc":
        //            Query = Query.OrderBy(s => s.ClientContactNo);
        //            break;
        //        case "ClientEMail_desc":
        //            Query = Query.OrderByDescending(s => s.ClientEMail);
        //            break;
        //        case "ClientEMail_asc":
        //            Query = Query.OrderBy(s => s.ClientEMail);
        //            break;
        //        case "AdditionalDiscount_desc":
        //            Query = Query.OrderByDescending(s => s.AdditionalDiscount);
        //            break;
        //        case "AdditionalDiscount_asc":
        //            Query = Query.OrderBy(s => s.AdditionalDiscount);
        //            break;
        //        case "Remarks_desc":
        //            Query = Query.OrderByDescending(s => s.Remarks);
        //            break;
        //        case "Remarks_asc":
        //            Query = Query.OrderBy(s => s.Remarks);
        //            break;
        //        case "PDFUrl_desc":
        //            Query = Query.OrderByDescending(s => s.PDFUrl);
        //            break;
        //        case "PDFUrl_asc":
        //            Query = Query.OrderBy(s => s.PDFUrl);
        //            break;
        //        case "CompanyID_desc":
        //            Query = Query.OrderByDescending(s => s.Company.CompanyName);
        //            break;
        //        case "CompanyID_asc":
        //            Query = Query.OrderBy(s => s.Company.CompanyName);
        //            break;
        //        case "AddUserID_desc":
        //            Query = Query.OrderByDescending(s => s.AddUserID);
        //            break;
        //        case "AddUserID_asc":
        //            Query = Query.OrderBy(s => s.AddUserID);
        //            break;
        //        case "AddDate_desc":
        //            Query = Query.OrderByDescending(s => s.AddDate);
        //            break;
        //        case "AddDate_asc":
        //            Query = Query.OrderBy(s => s.AddDate);
        //            break;
        //        case "ArchiveUserID_desc":
        //            Query = Query.OrderByDescending(s => s.ArchiveUserID);
        //            break;
        //        case "ArchiveUserID_asc":
        //            Query = Query.OrderBy(s => s.ArchiveUserID);
        //            break;
        //        case "ArchiveDate_desc":
        //            Query = Query.OrderByDescending(s => s.ArchiveDate);
        //            break;
        //        case "ArchiveDate_asc":
        //            Query = Query.OrderBy(s => s.ArchiveDate);
        //            break;
        //        default:  // Name ascending 
        //            Query = Query.OrderBy(s => s.InvoiceID);
        //            break;
        //    }

        //    if (command == "Export") {
        //        GridView gv = new GridView();
        //        DataTable dt = new DataTable();
        //        dt.Columns.Add("Invoice I D", typeof(string));
        //        dt.Columns.Add("Invoice No", typeof(string));
        //        dt.Columns.Add("Invoice Date", typeof(string));
        //        dt.Columns.Add("Project I D", typeof(string));
        //        dt.Columns.Add("Client I D", typeof(string));
        //        dt.Columns.Add("Client Name", typeof(string));
        //        dt.Columns.Add("Client Address", typeof(string));
        //        dt.Columns.Add("Client G S T I N", typeof(string));
        //        dt.Columns.Add("Client Contact No", typeof(string));
        //        dt.Columns.Add("Client E Mail", typeof(string));
        //        dt.Columns.Add("Additional Discount", typeof(string));
        //        dt.Columns.Add("Remarks", typeof(string));
        //        dt.Columns.Add("P D F Url", typeof(string));
        //        dt.Columns.Add("Company I D", typeof(string));
        //        dt.Columns.Add("Add User I D", typeof(string));
        //        dt.Columns.Add("Add Date", typeof(string));
        //        dt.Columns.Add("Archive User I D", typeof(string));
        //        dt.Columns.Add("Archive Date", typeof(string));
        //        foreach (var item in Query)
        //        {
        //            dt.Rows.Add(
        //                item.InvoiceID
        //               ,item.InvoiceNo
        //               ,item.InvoiceDate
        //               ,item.Project.ProjectName
        //               ,item.Client.ClientName
        //               ,item.ClientName
        //               ,item.ClientAddress
        //               ,item.ClientGSTIN
        //               ,item.ClientContactNo
        //               ,item.ClientEMail
        //               ,item.AdditionalDiscount
        //               ,item.Remarks
        //               ,item.PDFUrl
        //               ,item.Company.CompanyName
        //               ,item.AddUserID
        //               ,item.AddDate
        //               ,item.ArchiveUserID
        //               ,item.ArchiveDate
        //            );
        //        }
        //        gv.DataSource = dt;
        //        gv.DataBind();
        //        ExportData(Export, gv, dt);
        //    }

        //    int pageNumber = (page ?? 1);
        //    int? pageSZ = (Convert.ToInt32(Session["PageSize"]) == 0 ? 5 : Convert.ToInt32(Session["PageSize"]));
        //    return View(Query.ToPagedList(pageNumber, (pageSZ ?? 5)));
        //}
        public ActionResult Index()
        {
            var Data = InvoiceData.SelectAll();

            List<Invoice> InvoiceList = new List<Invoice>();
            for (int i = 0; i < Data.Rows.Count; i++)
            {
                Invoice invoice = new Invoice();
                invoice.InvoiceID = Convert.ToInt32(Data.Rows[i]["InvoiceID"]);
                invoice.InvoiceNo = Data.Rows[i]["InvoiceNo"].ToString();
                invoice.InvoiceDate = Convert.ToDateTime(Data.Rows[i]["InvoiceDate"].ToString());
                invoice.ClientName = Data.Rows[i]["ClientName"].ToString();
                invoice.TotalAmt = Convert.ToDecimal(Data.Rows[i]["TotalAmt"]);
                InvoiceList.Add(invoice);
            }
            return View(InvoiceList.ToList());
        }
        // Invoice1
        public ActionResult Invoice1(string sortOrder,
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Invoice I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["InvoiceIDSortParm"] = sortOrder == "InvoiceID_asc" ? "InvoiceID_desc" : "InvoiceID_asc";
            ViewData["InvoiceNoSortParm"] = sortOrder == "InvoiceNo_asc" ? "InvoiceNo_desc" : "InvoiceNo_asc";
            ViewData["InvoiceDateSortParm"] = sortOrder == "InvoiceDate_asc" ? "InvoiceDate_desc" : "InvoiceDate_asc";
            ViewData["ProjectIDSortParm"] = sortOrder == "ProjectID_asc" ? "ProjectID_desc" : "ProjectID_asc";
            ViewData["ClientIDSortParm"] = sortOrder == "ClientID_asc" ? "ClientID_desc" : "ClientID_asc";
            ViewData["ClientNameSortParm"] = sortOrder == "ClientName_asc" ? "ClientName_desc" : "ClientName_asc";
            ViewData["ClientAddressSortParm"] = sortOrder == "ClientAddress_asc" ? "ClientAddress_desc" : "ClientAddress_asc";
            ViewData["ClientGSTINSortParm"] = sortOrder == "ClientGSTIN_asc" ? "ClientGSTIN_desc" : "ClientGSTIN_asc";
            ViewData["ClientContactNoSortParm"] = sortOrder == "ClientContactNo_asc" ? "ClientContactNo_desc" : "ClientContactNo_asc";
            ViewData["ClientEMailSortParm"] = sortOrder == "ClientEMail_asc" ? "ClientEMail_desc" : "ClientEMail_asc";
            ViewData["AdditionalDiscountSortParm"] = sortOrder == "AdditionalDiscount_asc" ? "AdditionalDiscount_desc" : "AdditionalDiscount_asc";
            ViewData["RemarksSortParm"] = sortOrder == "Remarks_asc" ? "Remarks_desc" : "Remarks_asc";
            ViewData["PDFUrlSortParm"] = sortOrder == "PDFUrl_asc" ? "PDFUrl_desc" : "PDFUrl_asc";
            ViewData["CompanyIDSortParm"] = sortOrder == "CompanyID_asc" ? "CompanyID_desc" : "CompanyID_asc";
            ViewData["AddUserIDSortParm"] = sortOrder == "AddUserID_asc" ? "AddUserID_desc" : "AddUserID_asc";
            ViewData["AddDateSortParm"] = sortOrder == "AddDate_asc" ? "AddDate_desc" : "AddDate_asc";
            ViewData["ArchiveUserIDSortParm"] = sortOrder == "ArchiveUserID_asc" ? "ArchiveUserID_desc" : "ArchiveUserID_asc";
            ViewData["ArchiveDateSortParm"] = sortOrder == "ArchiveDate_asc" ? "ArchiveDate_desc" : "ArchiveDate_asc";

            dtInvoice = InvoiceData.SelectAll();
            dtProject = Invoice_ProjectData.SelectAll();
            dtClient = Invoice_ClientData.SelectAll();
            dtCompany = Invoice_CompanyData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtInvoice = InvoiceData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowInvoice in dtInvoice.AsEnumerable()
                        join rowProject in dtProject.AsEnumerable() on rowInvoice.Field<Int32?>("ProjectID") equals rowProject.Field<Int32>("ProjectID")
                        join rowClient in dtClient.AsEnumerable() on rowInvoice.Field<Int32>("ClientID") equals rowClient.Field<Int32>("ClientID")
                        join rowCompany in dtCompany.AsEnumerable() on rowInvoice.Field<Int32?>("CompanyID") equals rowCompany.Field<Int32>("CompanyID")
                        select new Invoice()
                        {
                            InvoiceID = rowInvoice.Field<Int32>("InvoiceID")
                           ,
                            InvoiceNo = rowInvoice.Field<String>("InvoiceNo")
                           ,
                            InvoiceDate = rowInvoice.Field<DateTime>("InvoiceDate")
                           ,
                            Project = new Project()
                            {
                                ProjectID = rowProject.Field<Int32>("ProjectID")
                                  ,
                                ProjectName = rowProject.Field<String>("ProjectName")
                            }
                           ,
                            Client = new Client()
                            {
                                ClientID = rowClient.Field<Int32>("ClientID")
                                  ,
                                ClientName = rowClient.Field<String>("ClientName")
                            }
                           ,
                            ClientName = rowInvoice.Field<String>("ClientName")
                           ,
                            ClientAddress = rowInvoice.Field<String>("ClientAddress")
                           ,
                            ClientGSTIN = rowInvoice.Field<String>("ClientGSTIN")
                           ,
                            ClientContactNo = rowInvoice.Field<String>("ClientContactNo")
                           ,
                            ClientEMail = rowInvoice.Field<String>("ClientEMail")
                           ,
                            AdditionalDiscount = rowInvoice.Field<Decimal?>("AdditionalDiscount")
                           ,
                            Remarks = rowInvoice.Field<String>("Remarks")
                           ,
                            PDFUrl = rowInvoice.Field<String>("PDFUrl")
                           ,
                            Company = new Company()
                            {
                                CompanyID = rowCompany.Field<Int32>("CompanyID")
                                  ,
                                CompanyName = rowCompany.Field<String>("CompanyName")
                            }
                           ,
                            AddUserID = rowInvoice.Field<Int32?>("AddUserID")
                           ,
                            AddDate = rowInvoice.Field<DateTime?>("AddDate")
                           ,
                            ArchiveUserID = rowInvoice.Field<Int32?>("ArchiveUserID")
                           ,
                            ArchiveDate = rowInvoice.Field<DateTime?>("ArchiveDate")
                        };

            switch (sortOrder)
            {
                case "InvoiceID_desc":
                    Query = Query.OrderByDescending(s => s.InvoiceID);
                    break;
                case "InvoiceID_asc":
                    Query = Query.OrderBy(s => s.InvoiceID);
                    break;
                case "InvoiceNo_desc":
                    Query = Query.OrderByDescending(s => s.InvoiceNo);
                    break;
                case "InvoiceNo_asc":
                    Query = Query.OrderBy(s => s.InvoiceNo);
                    break;
                case "InvoiceDate_desc":
                    Query = Query.OrderByDescending(s => s.InvoiceDate);
                    break;
                case "InvoiceDate_asc":
                    Query = Query.OrderBy(s => s.InvoiceDate);
                    break;
                case "ProjectID_desc":
                    Query = Query.OrderByDescending(s => s.Project.ProjectName);
                    break;
                case "ProjectID_asc":
                    Query = Query.OrderBy(s => s.Project.ProjectName);
                    break;
                case "ClientID_desc":
                    Query = Query.OrderByDescending(s => s.Client.ClientName);
                    break;
                case "ClientID_asc":
                    Query = Query.OrderBy(s => s.Client.ClientName);
                    break;
                case "ClientName_desc":
                    Query = Query.OrderByDescending(s => s.ClientName);
                    break;
                case "ClientName_asc":
                    Query = Query.OrderBy(s => s.ClientName);
                    break;
                case "ClientAddress_desc":
                    Query = Query.OrderByDescending(s => s.ClientAddress);
                    break;
                case "ClientAddress_asc":
                    Query = Query.OrderBy(s => s.ClientAddress);
                    break;
                case "ClientGSTIN_desc":
                    Query = Query.OrderByDescending(s => s.ClientGSTIN);
                    break;
                case "ClientGSTIN_asc":
                    Query = Query.OrderBy(s => s.ClientGSTIN);
                    break;
                case "ClientContactNo_desc":
                    Query = Query.OrderByDescending(s => s.ClientContactNo);
                    break;
                case "ClientContactNo_asc":
                    Query = Query.OrderBy(s => s.ClientContactNo);
                    break;
                case "ClientEMail_desc":
                    Query = Query.OrderByDescending(s => s.ClientEMail);
                    break;
                case "ClientEMail_asc":
                    Query = Query.OrderBy(s => s.ClientEMail);
                    break;
                case "AdditionalDiscount_desc":
                    Query = Query.OrderByDescending(s => s.AdditionalDiscount);
                    break;
                case "AdditionalDiscount_asc":
                    Query = Query.OrderBy(s => s.AdditionalDiscount);
                    break;
                case "Remarks_desc":
                    Query = Query.OrderByDescending(s => s.Remarks);
                    break;
                case "Remarks_asc":
                    Query = Query.OrderBy(s => s.Remarks);
                    break;
                case "PDFUrl_desc":
                    Query = Query.OrderByDescending(s => s.PDFUrl);
                    break;
                case "PDFUrl_asc":
                    Query = Query.OrderBy(s => s.PDFUrl);
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
                    Query = Query.OrderBy(s => s.InvoiceID);
                    break;
            }

            if (command == "Export")
            {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Invoice I D", typeof(string));
                dt.Columns.Add("Invoice No", typeof(string));
                dt.Columns.Add("Invoice Date", typeof(string));
                dt.Columns.Add("Project I D", typeof(string));
                dt.Columns.Add("Client I D", typeof(string));
                dt.Columns.Add("Client Name", typeof(string));
                dt.Columns.Add("Client Address", typeof(string));
                dt.Columns.Add("Client G S T I N", typeof(string));
                dt.Columns.Add("Client Contact No", typeof(string));
                dt.Columns.Add("Client E Mail", typeof(string));
                dt.Columns.Add("Additional Discount", typeof(string));
                dt.Columns.Add("Remarks", typeof(string));
                dt.Columns.Add("P D F Url", typeof(string));
                dt.Columns.Add("Company I D", typeof(string));
                dt.Columns.Add("Add User I D", typeof(string));
                dt.Columns.Add("Add Date", typeof(string));
                dt.Columns.Add("Archive User I D", typeof(string));
                dt.Columns.Add("Archive Date", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.InvoiceID
                       , item.InvoiceNo
                       , item.InvoiceDate
                       , item.Project.ProjectName
                       , item.Client.ClientName
                       , item.ClientName
                       , item.ClientAddress
                       , item.ClientGSTIN
                       , item.ClientContactNo
                       , item.ClientEMail
                       , item.AdditionalDiscount
                       , item.Remarks
                       , item.PDFUrl
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
            return View(Query.ToPagedList(pageNumber, (pageSZ ?? 5)));
        }

        //End of Invooice1

        // GET: /Invoice/Details/<id>
        public ActionResult Details(
                                      Int32? InvoiceID
                                   )
        {
            if (
                    InvoiceID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtProject = Invoice_ProjectData.SelectAll();
            dtClient = Invoice_ClientData.SelectAll();
            dtCompany = Invoice_CompanyData.SelectAll();

            Invoice Invoice = new Invoice();
            Invoice.InvoiceID = System.Convert.ToInt32(InvoiceID);
            Invoice = InvoiceData.Select_Record(Invoice);
            Invoice.Project = new Project()
            {
                ProjectID = (Int32)Invoice.ProjectID
               ,
                ProjectName = (from DataRow rowProject in dtProject.Rows
                               where Invoice.ProjectID == (int)rowProject["ProjectID"]
                               select (String)rowProject["ProjectName"]).FirstOrDefault()
            };
            Invoice.Client = new Client()
            {
                ClientID = (Int32)Invoice.ClientID
               ,
                ClientName = (from DataRow rowClient in dtClient.Rows
                              where Invoice.ClientID == (int)rowClient["ClientID"]
                              select (String)rowClient["ClientName"]).FirstOrDefault()
            };
            Invoice.Company = new Company()
            {
                CompanyID = (Int32)Invoice.CompanyID
               ,
                CompanyName = (from DataRow rowCompany in dtCompany.Rows
                               where Invoice.CompanyID == (int)rowCompany["CompanyID"]
                               select (String)rowCompany["CompanyName"]).FirstOrDefault()
            };

            if (Invoice == null)
            {
                return HttpNotFound();
            }
            return View(Invoice);
        }

        // GET: /Invoice/Create
        public ActionResult Create()
        {
            // ComboBox
            //ViewData["ProjectID"] = new SelectList(Invoice_ProjectData.List(), "ProjectID", "ProjectName");
            ViewData["ClientID"] = new SelectList(Invoice_ClientData.List(), "ClientID", "ClientName");
            ViewData["CompanyID"] = new SelectList(Invoice_CompanyData.List(), "CompanyID", "CompanyName");

            return View();
        }

        // POST: /Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Invoice Invoice)
        {
            //if (ModelState.IsValid)
            //{
            bool bSucess = false; int invoiceID = 0;
            invoiceID = InvoiceData.Add(Invoice);
            foreach (var item in Invoice.Items)
            {
                item.InvoiceID = invoiceID;
                bSucess = InvoiceItemData.Add(item);
            }
            if (bSucess == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Can Not Insert");
            }
            //}
            // ComboBox
            //ViewData["ProjectID"] = new SelectList(Invoice_ProjectData.List(), "ProjectID", "ProjectName", Invoice.ProjectID);
            ViewData["ClientID"] = new SelectList(Invoice_ClientData.List(), "ClientID", "ClientName", Invoice.ClientID);
            ViewData["CompanyID"] = new SelectList(Invoice_CompanyData.List(), "CompanyID", "CompanyName", Invoice.CompanyID);

            return View(Invoice);
        }

        // GET: /Invoice/Edit/<id>
        public ActionResult Edit(int InvoiceID)
        {
            if (InvoiceID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Invoice Invoice = new Invoice();
            Invoice.InvoiceID = System.Convert.ToInt32(InvoiceID);
            Invoice = InvoiceData.Select_Record(Invoice);
            InvoiceItem InvoiceItem = new InvoiceItem();
            InvoiceItem.InvoiceID = InvoiceID;
            ViewBag.InvoiceItem = InvoiceItemData.List(InvoiceItem);

            // ComboBox
            ViewData["ProjectID"] = new SelectList(Invoice_ProjectData.List(), "ProjectID", "ProjectName", Invoice.ProjectID);
            ViewData["ClientID"] = new SelectList(Invoice_ClientData.List(), "ClientID", "ClientName", Invoice.ClientID);
            ViewData["CompanyID"] = new SelectList(Invoice_CompanyData.List(), "CompanyID", "CompanyName", Invoice.CompanyID);

            return View(Invoice);
        }

        // POST: /Invoice/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(Invoice Invoice)
        {

            //Invoice oInvoice = new Invoice();
            //oInvoice.InvoiceID = System.Convert.ToInt32(Invoice.InvoiceID);
            //oInvoice = InvoiceData.Select_Record(Invoice);

            //if (ModelState.IsValid)
            //{
            bool bSucess = false;
            bSucess = InvoiceData.Update(Invoice);
            foreach (var item in Invoice.Items)
            {
                item.InvoiceID = Invoice.InvoiceID;
                if (item.InvoiceItemID == 0)
                {
                    InvoiceItemData.Add(item);
                }
                
            }
            if (bSucess == true)
            {
                return RedirectToAction("Index","Invoice");
            }
            else
            {
                ModelState.AddModelError("", "Can Not Update");
            }
            //}
            // ComboBox
            ViewData["ProjectID"] = new SelectList(Invoice_ProjectData.List(), "ProjectID", "ProjectName", Invoice.ProjectID);
            ViewData["ClientID"] = new SelectList(Invoice_ClientData.List(), "ClientID", "ClientName", Invoice.ClientID);
            ViewData["CompanyID"] = new SelectList(Invoice_CompanyData.List(), "CompanyID", "CompanyName", Invoice.CompanyID);

            return View(Invoice);
        }

        // GET: /Invoice/Delete/<id>
        public ActionResult Delete(
                                     Int32? InvoiceID
                                  )
        {
            if (
                    InvoiceID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtProject = Invoice_ProjectData.SelectAll();
            dtClient = Invoice_ClientData.SelectAll();
            dtCompany = Invoice_CompanyData.SelectAll();

            Invoice Invoice = new Invoice();
            Invoice.InvoiceID = System.Convert.ToInt32(InvoiceID);
            Invoice = InvoiceData.Select_Record(Invoice);
            Invoice.Project = new Project()
            {
                ProjectID = (Int32)Invoice.ProjectID
               ,
                ProjectName = (from DataRow rowProject in dtProject.Rows
                               where Invoice.ProjectID == (int)rowProject["ProjectID"]
                               select (String)rowProject["ProjectName"]).FirstOrDefault()
            };
            Invoice.Client = new Client()
            {
                ClientID = (Int32)Invoice.ClientID
               ,
                ClientName = (from DataRow rowClient in dtClient.Rows
                              where Invoice.ClientID == (int)rowClient["ClientID"]
                              select (String)rowClient["ClientName"]).FirstOrDefault()
            };
            Invoice.Company = new Company()
            {
                CompanyID = (Int32)Invoice.CompanyID
               ,
                CompanyName = (from DataRow rowCompany in dtCompany.Rows
                               where Invoice.CompanyID == (int)rowCompany["CompanyID"]
                               select (String)rowCompany["CompanyName"]).FirstOrDefault()
            };

            if (Invoice == null)
            {
                return HttpNotFound();
            }
            return View(Invoice);
        }

        // POST: /Invoice/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? InvoiceID
                                            )
        {

            Invoice Invoice = new Invoice();
            Invoice.InvoiceID = System.Convert.ToInt32(InvoiceID);
            Invoice = InvoiceData.Select_Record(Invoice);

            bool bSucess = false;
            bSucess = InvoiceData.Delete(Invoice);
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
            SelectListItem Item2 = new SelectListItem { Text = "Invoice No", Value = "Invoice No" };
            SelectListItem Item3 = new SelectListItem { Text = "Invoice Date", Value = "Invoice Date" };
            SelectListItem Item4 = new SelectListItem { Text = "Project I D", Value = "Project I D" };
            SelectListItem Item5 = new SelectListItem { Text = "Client I D", Value = "Client I D" };
            SelectListItem Item6 = new SelectListItem { Text = "Client Name", Value = "Client Name" };
            SelectListItem Item7 = new SelectListItem { Text = "Client Address", Value = "Client Address" };
            SelectListItem Item8 = new SelectListItem { Text = "Client G S T I N", Value = "Client G S T I N" };
            SelectListItem Item9 = new SelectListItem { Text = "Client Contact No", Value = "Client Contact No" };
            SelectListItem Item10 = new SelectListItem { Text = "Client E Mail", Value = "Client E Mail" };
            SelectListItem Item11 = new SelectListItem { Text = "Additional Discount", Value = "Additional Discount" };
            SelectListItem Item12 = new SelectListItem { Text = "Remarks", Value = "Remarks" };
            SelectListItem Item13 = new SelectListItem { Text = "P D F Url", Value = "P D F Url" };
            SelectListItem Item14 = new SelectListItem { Text = "Company I D", Value = "Company I D" };
            SelectListItem Item15 = new SelectListItem { Text = "Add User I D", Value = "Add User I D" };
            SelectListItem Item16 = new SelectListItem { Text = "Add Date", Value = "Add Date" };
            SelectListItem Item17 = new SelectListItem { Text = "Archive User I D", Value = "Archive User I D" };
            SelectListItem Item18 = new SelectListItem { Text = "Archive Date", Value = "Archive Date" };

            if (select == "Invoice I D") { Item1.Selected = true; }
            else if (select == "Invoice No") { Item2.Selected = true; }
            else if (select == "Invoice Date") { Item3.Selected = true; }
            else if (select == "Project I D") { Item4.Selected = true; }
            else if (select == "Client I D") { Item5.Selected = true; }
            else if (select == "Client Name") { Item6.Selected = true; }
            else if (select == "Client Address") { Item7.Selected = true; }
            else if (select == "Client G S T I N") { Item8.Selected = true; }
            else if (select == "Client Contact No") { Item9.Selected = true; }
            else if (select == "Client E Mail") { Item10.Selected = true; }
            else if (select == "Additional Discount") { Item11.Selected = true; }
            else if (select == "Remarks") { Item12.Selected = true; }
            else if (select == "P D F Url") { Item13.Selected = true; }
            else if (select == "Company I D") { Item14.Selected = true; }
            else if (select == "Add User I D") { Item15.Selected = true; }
            else if (select == "Add Date") { Item16.Selected = true; }
            else if (select == "Archive User I D") { Item17.Selected = true; }
            else if (select == "Archive Date") { Item18.Selected = true; }

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
            list.Add(Item18);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Invoice", "Many");
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
        public JsonResult GetCustomerState(int clientID = 0)
        {
            Client client = new Client();
            client.ClientID = System.Convert.ToInt32(clientID);
            client.CompanyID = 0;
            var result = InvoiceData.Client_StateVerify(client);
            if (result == null)
                return null;
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public JsonResult GetProjectByCustomer(int clientID = 0)
        {
            if (clientID == 0)
                return Json(Invoice_ProjectData.List(), JsonRequestBehavior.AllowGet);
            return Json(Invoice_ProjectData.List().Where(x => x.ClientID == clientID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteInvoiceItem(int invoiceitemID = 0)
        {
            InvoiceItem _objItem = new InvoiceItem();
            bool result = false;
            if (invoiceitemID > 0)
            {
                _objItem.InvoiceItemID = invoiceitemID;
                result=InvoiceItemData.Delete(_objItem);
            }
            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}

