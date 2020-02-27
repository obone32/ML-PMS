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
using System.Reflection;


namespace CloudTrixApp.Controllers
{
    public class RoleManagementController : Controller
    {
        DataTable dtRoleManagement = new DataTable();
        DataTable dtUserType = new DataTable();
        //
        // GET: /RoleManagement/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Role I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["RoleIDSortParm"] = sortOrder == "RoleID_asc" ? "RoleID_desc" : "RoleID_asc";
            ViewData["AddUserIDSortParm"] = sortOrder == "AddUserID_asc" ? "AddUserID_desc" : "AddUserID_asc";
            ViewData["AddDateSortParm"] = sortOrder == "AddDate_asc" ? "AddDate_desc" : "AddDate_asc";
            ViewData["ArchiveUserIDSortParm"] = sortOrder == "ArchiveUserID_asc" ? "ArchiveUserID_desc" : "ArchiveUserID_asc";
            ViewData["ArchiveDateSortParm"] = sortOrder == "ArchiveDate_asc" ? "ArchiveDate_desc" : "ArchiveDate_asc";
            ViewData["UserTypeIDSortParm"] = sortOrder == "UserTypeID_asc" ? "UserTypeID_desc" : "UserTypeID_asc";

            dtRoleManagement = RoleManagementData.SelectAll();
            dtUserType = RoleManagement_UserTypeData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtRoleManagement = RoleManagementData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowRoleManagement in dtRoleManagement.AsEnumerable()
                        join rowUserType in dtUserType.AsEnumerable() on rowRoleManagement.Field<Int32?>("UserTypeID") equals rowUserType.Field<Int32>("UserTypeID")
                        select new RoleManagement()
                        {
                            RoleID = rowRoleManagement.Field<Int32>("RoleID")
                            ,
                            //AddUserID = rowRoleManagement.Field<Int32?>("AddUserID")
                            //,
                            //AddDate = rowRoleManagement.Field<DateTime?>("AddDate")
                            //,
                            //ArchiveUserID = rowRoleManagement.Field<Int32?>("ArchiveUserID")
                            //,
                            //ArchiveDate = rowRoleManagement.Field<DateTime?>("ArchiveDate")
                            //,
                            UserType = new UserType()
                            {
                                UserTypeID = rowUserType.Field<Int32>("UserTypeID")
                                  ,
                                UserTypeName = rowUserType.Field<String>("UserTypeName")
                            }
                        };

            switch (sortOrder)
            {
                case "RoleID_desc":
                    Query = Query.OrderByDescending(s => s.RoleID);
                    break;
                case "RoleID_asc":
                    Query = Query.OrderBy(s => s.RoleID);
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
                case "UserTypeID_desc":
                    Query = Query.OrderByDescending(s => s.UserType.UserTypeName);
                    break;
                case "UserTypeID_asc":
                    Query = Query.OrderBy(s => s.UserType.UserTypeName);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.RoleID);
                    break;
            }

            if (command == "Export")
            {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Role I D", typeof(string));
                dt.Columns.Add("Add User I D", typeof(string));
                dt.Columns.Add("Add Date", typeof(string));
                dt.Columns.Add("Archive User I D", typeof(string));
                dt.Columns.Add("Archive Date", typeof(string));
                dt.Columns.Add("User Type I D", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.RoleID
                       , item.AddUserID
                       , item.AddDate
                       , item.ArchiveUserID
                       , item.ArchiveDate
                       , item.UserType.UserTypeName
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
        //
        // GET: /RoleManagement/Details/5
        public ActionResult Details(int? RoleID)
        {
            if (RoleID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleManagement RoleManagement = new RoleManagement();
            RoleManagement.RoleID = System.Convert.ToInt32(RoleID);
            RoleManagement = RoleManagementData.Select_Record(RoleManagement);
            RoleManagement.UserType = new UserType()
            {
                UserTypeID = (Int32)RoleManagement.UserTypeID
               ,
                UserTypeName = (from DataRow rowUserType in dtUserType.Rows
                                where RoleManagement.UserTypeID == (int)rowUserType["UserTypeID"]
                                select (String)rowUserType["UserTypeName"]).FirstOrDefault()

            };
            ViewBag.UserTypeName = RoleManagement.UserType.UserTypeName;
            //Load All data
            DataTable dtForm = new DataTable();
            dtForm = FormsData.SelectAll();
            List<Forms> FormDetails = new List<Forms>();
            FormDetails = ConvertDataTable<Forms>(dtForm);
            ViewBag.MyList = FormDetails;

            //Load Save data
            if (RoleManagement.RoleID != null)
            {
                DataTable dtRoleManagementDetails = new DataTable();
                dtRoleManagementDetails = RoleManagementData.SelectRoleManagementData(System.Convert.ToInt32(RoleManagement.RoleID));
                List<RoleManagementDetails> RoleManagementDetails = new List<RoleManagementDetails>();
                RoleManagementDetails = ConvertDataTable<RoleManagementDetails>(dtRoleManagementDetails);
                ViewBag.RoleManagementDetailsList = RoleManagementDetails;
            }
            if (RoleManagement == null)
            {
                return HttpNotFound();
            }
            return View(RoleManagement);
        }

        //
        // GET: /RoleManagement/Create
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        public ActionResult Create()
        {
            ViewData["UserTypeID"] = new SelectList(UserTypePermission_UserTypeData.List(), "UserTypeID", "UserTypeName");
            DataTable dtForm = new DataTable();
            dtForm = FormsData.SelectAll();
            List<Form> FormDetails = new List<Form>();
            FormDetails = ConvertDataTable<Form>(dtForm);
            ViewBag.MyList = FormDetails;
            return View();
        }

        //
        // POST: /RoleManagement/Create
        [HttpPost]
        //  [ValidateAntiForgeryToken]
        public ActionResult Create(RoleManagement RoleManagement)
        {
            //if (ModelState.IsValid)
            //{
            bool bSucess = false;
            int RoleID = 0;
            RoleID = RoleManagementData.Add(RoleManagement);
            foreach (var item in RoleManagement.Items)
            {
                item.RoleID = RoleID;
                bSucess = RoleManagementDetailsData.Add(item);
            }
            //}
            // ComboBox
            ViewData["UserTypeID"] = new SelectList(RoleManagement_UserTypeData.List(), "UserTypeID", "UserTypeName", RoleManagement.UserTypeID);
            DataTable dtForm = new DataTable();
            dtForm = FormsData.SelectAll();
            List<Forms> FormDetails = new List<Forms>();
            FormDetails = ConvertDataTable<Forms>(dtForm);
            ViewBag.MyList = FormDetails;
            if (bSucess == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Can Not Insert");
            }
            return View(RoleManagement);
        }

        //
        // GET: /Form/Edit/5
        public ActionResult Edit(Int32? RoleID)
        {

            if (RoleID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleManagement RoleManagement = new RoleManagement();
            RoleManagement.RoleID = System.Convert.ToInt32(RoleID);
            RoleManagement = RoleManagementData.Select_Record(RoleManagement);

            if (RoleManagement == null)
            {
                return HttpNotFound();
            }
            // ComboBox
            ViewData["UserTypeID"] = new SelectList(RoleManagement_UserTypeData.List(), "UserTypeID", "UserTypeName", RoleManagement.UserTypeID);
            TempData["Description"] = RoleManagement.Description;
            //Load All data
            DataTable dtForm = new DataTable();
            dtForm = FormsData.SelectAll();
            List<Forms> FormDetails = new List<Forms>();
            FormDetails = ConvertDataTable<Forms>(dtForm);
            ViewBag.MyList = FormDetails;

            //Load Save data
            if (RoleManagement.RoleID != null)
            {
                DataTable dtRoleManagementDetails = new DataTable();
                dtRoleManagementDetails = RoleManagementData.SelectRoleManagementData(System.Convert.ToInt32(RoleManagement.RoleID));
                List<RoleManagementDetails> RoleManagementDetails = new List<RoleManagementDetails>();
                RoleManagementDetails = ConvertDataTable<RoleManagementDetails>(dtRoleManagementDetails);
                ViewBag.RoleManagementDetailsList = RoleManagementDetails;
            }
            return View(RoleManagement);
        }

        //
        // POST: /Form/Edit/5
        [HttpPost]
        public ActionResult Edit(RoleManagement RoleManagement)
        {
            RoleManagement oRoleManagement = new RoleManagement();
            oRoleManagement.RoleID = System.Convert.ToInt32(RoleManagement.RoleID);
            oRoleManagement = RoleManagementData.Select_Record(RoleManagement);

            //if (ModelState.IsValid)
            //{
            bool bSucess = false;
            bSucess = RoleManagementData.Update(oRoleManagement, RoleManagement);
            foreach (var item in RoleManagement.Items)
            {
                item.RoleID = RoleManagement.RoleID;
                if (item.RoleManagementDetailsID == 0)
                {
                    RoleManagementDetailsData.Add(item);
                }
                else
                {
                    RoleManagementDetails oRoleManagementDetails = new RoleManagementDetails();
                    oRoleManagementDetails.RoleManagementDetailsID = System.Convert.ToInt32(item.RoleManagementDetailsID);
                    oRoleManagementDetails = RoleManagementDetailsData.Select_Record(item);
                    RoleManagementDetailsData.Update(oRoleManagementDetails, item);
                }
            }
            if (bSucess == true)
            {
                return RedirectToAction("Index", "RoleManagement");
            }
            else
            {
                ModelState.AddModelError("", "Can Not Update");
            }
            //}
            // ComboBox
            ViewData["UserTypeID"] = new SelectList(RoleManagement_UserTypeData.List(), "UserTypeID", "UserTypeName", RoleManagement.UserTypeID);
            return View(RoleManagement);
        }

        //
        // GET: /Form/Delete/5
        public ActionResult Delete(int? RoleID)
        {
            if (RoleID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoleManagement RoleManagement = new RoleManagement();
            RoleManagement.RoleID = System.Convert.ToInt32(RoleID);
            RoleManagement = RoleManagementData.Select_Record(RoleManagement);
            //Load All data
            DataTable dtForm = new DataTable();
            dtForm = FormsData.SelectAll();
            List<Forms> FormDetails = new List<Forms>();
            FormDetails = ConvertDataTable<Forms>(dtForm);
            ViewBag.MyList = FormDetails;

            //Load Save data
            if (RoleManagement.RoleID != null)
            {
                DataTable dtRoleManagementDetails = new DataTable();
                dtRoleManagementDetails = RoleManagementData.SelectRoleManagementData(System.Convert.ToInt32(RoleManagement.RoleID));
                List<RoleManagementDetails> RoleManagementDetails = new List<RoleManagementDetails>();
                RoleManagementDetails = ConvertDataTable<RoleManagementDetails>(dtRoleManagementDetails);
                ViewBag.RoleManagementDetailsList = RoleManagementDetails;
            }
            if (RoleManagement == null)
            {
                return HttpNotFound();
            }
            return View(RoleManagement);
        }

        //
        // POST: /Form/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? RoleID
                                            )
        {

            RoleManagement RoleManagement = new RoleManagement();
            RoleManagement.RoleID = System.Convert.ToInt32(RoleID);
            RoleManagement = RoleManagementData.Select_Record(RoleManagement);

            bool bSucess = false;
            bSucess = RoleManagementData.Delete(RoleManagement);
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

        private static List<SelectListItem> GetFields(String select)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem Item1 = new SelectListItem { Text = "Role I D", Value = "Role I D" };
            SelectListItem Item2 = new SelectListItem { Text = "Add User I D", Value = "Add User I D" };
            SelectListItem Item3 = new SelectListItem { Text = "Add Date", Value = "Add Date" };
            SelectListItem Item4 = new SelectListItem { Text = "Archive User I D", Value = "Archive User I D" };
            SelectListItem Item5 = new SelectListItem { Text = "Archive Date", Value = "Archive Date" };
            SelectListItem Item6 = new SelectListItem { Text = "User Type I D", Value = "User Type I D" };

            if (select == "Role I D") { Item1.Selected = true; }
            else if (select == "Add User I D") { Item2.Selected = true; }
            else if (select == "Add Date") { Item3.Selected = true; }
            else if (select == "Archive User I D") { Item4.Selected = true; }
            else if (select == "Archive Date") { Item5.Selected = true; }
            else if (select == "User Type I D") { Item6.Selected = true; }

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
                PDFform pdfForm = new PDFform(dt, "Dbo. Role Management", "Many");
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
