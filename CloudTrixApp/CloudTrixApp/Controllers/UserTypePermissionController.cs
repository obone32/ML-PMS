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
    public class UserTypePermissionController : Controller
    {

        DataTable dtUserTypePermission = new DataTable();
        DataTable dtUserType = new DataTable();
        DataTable dtPermission = new DataTable();

        // GET: /UserTypePermission/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "User Type I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["UserTypeIDSortParm"] = sortOrder == "UserTypeID_asc" ? "UserTypeID_desc" : "UserTypeID_asc";
            ViewData["UserTypePermissionIDSortParm"] = sortOrder == "UserTypePermissionID_asc" ? "UserTypePermissionID_desc" : "UserTypePermissionID_asc";
            ViewData["PermissionIDSortParm"] = sortOrder == "PermissionID_asc" ? "PermissionID_desc" : "PermissionID_asc";

            dtUserTypePermission = UserTypePermissionData.SelectAll();
            dtUserType = UserTypePermission_UserTypeData.SelectAll();
            dtPermission = UserTypePermission_PermissionData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtUserTypePermission = UserTypePermissionData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowUserTypePermission in dtUserTypePermission.AsEnumerable()
                        join rowUserType in dtUserType.AsEnumerable() on rowUserTypePermission.Field<Int32>("UserTypeID") equals rowUserType.Field<Int32>("UserTypeID")
                        join rowPermission in dtPermission.AsEnumerable() on rowUserTypePermission.Field<Int32>("PermissionID") equals rowPermission.Field<Int32>("PermissionID")
                        select new UserTypePermission() {
                            UserType = new UserType() 
                            {
                                   UserTypeID = rowUserType.Field<Int32>("UserTypeID")
                                  ,UserTypeName = rowUserType.Field<String>("UserTypeName")
                            }
                           ,UserTypePermissionID = rowUserTypePermission.Field<Int32>("UserTypePermissionID")
                           ,
                            Permission = new Permission() 
                            {
                                   PermissionID = rowPermission.Field<Int32>("PermissionID")
                                  ,PermissionName = rowPermission.Field<String>("PermissionName")
                            }
                        };

            switch (sortOrder)
            {
                case "UserTypeID_desc":
                    Query = Query.OrderByDescending(s => s.UserType.UserTypeName);
                    break;
                case "UserTypeID_asc":
                    Query = Query.OrderBy(s => s.UserType.UserTypeName);
                    break;
                case "UserTypePermissionID_desc":
                    Query = Query.OrderByDescending(s => s.UserTypePermissionID);
                    break;
                case "UserTypePermissionID_asc":
                    Query = Query.OrderBy(s => s.UserTypePermissionID);
                    break;
                case "PermissionID_desc":
                    Query = Query.OrderByDescending(s => s.Permission.PermissionName);
                    break;
                case "PermissionID_asc":
                    Query = Query.OrderBy(s => s.Permission.PermissionName);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.UserTypeID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("User Type I D", typeof(string));
                dt.Columns.Add("User Type Permission I D", typeof(string));
                dt.Columns.Add("Permission I D", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.UserType.UserTypeName
                       ,item.UserTypePermissionID
                       ,item.Permission.PermissionName
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

        // GET: /UserTypePermission/Details/<id>
        public ActionResult Details(
                                      Int32? UserTypeID
                                     ,Int32? UserTypePermissionID
                                   )
        {
            if (
                    UserTypeID == null
                 || UserTypePermissionID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtUserType = UserTypePermission_UserTypeData.SelectAll();
            dtPermission = UserTypePermission_PermissionData.SelectAll();

            UserTypePermission UserTypePermission = new UserTypePermission();
            UserTypePermission.UserTypeID = System.Convert.ToInt32(UserTypeID);
            UserTypePermission.UserTypePermissionID = System.Convert.ToInt32(UserTypePermissionID);
            UserTypePermission = UserTypePermissionData.Select_Record(UserTypePermission);
            UserTypePermission.UserType = new UserType()
            {
                UserTypeID = (Int32)UserTypePermission.UserTypeID
               ,UserTypeName = (from DataRow rowUserType in dtUserType.Rows
                      where UserTypePermission.UserTypeID == (int)rowUserType["UserTypeID"]
                      select (String)rowUserType["UserTypeName"]).FirstOrDefault()
            };
            UserTypePermission.Permission = new Permission()
            {
                PermissionID = (Int32)UserTypePermission.PermissionID
               ,PermissionName = (from DataRow rowPermission in dtPermission.Rows
                      where UserTypePermission.PermissionID == (int)rowPermission["PermissionID"]
                      select (String)rowPermission["PermissionName"]).FirstOrDefault()
            };

            if (UserTypePermission == null)
            {
                return HttpNotFound();
            }
            return View(UserTypePermission);
        }

        // GET: /UserTypePermission/Create
        public ActionResult Create()
        {
        // ComboBox
            ViewData["UserTypeID"] = new SelectList(UserTypePermission_UserTypeData.List(), "UserTypeID", "UserTypeName");
            ViewData["PermissionID"] = new SelectList(UserTypePermission_PermissionData.List(), "PermissionID", "PermissionName");

            return View();
        }

        // POST: /UserTypePermission/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "UserTypeID"
				   + "," + "UserTypePermissionID"
				   + "," + "PermissionID"
				  )] UserTypePermission UserTypePermission)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = UserTypePermissionData.Add(UserTypePermission);
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
            ViewData["UserTypeID"] = new SelectList(UserTypePermission_UserTypeData.List(), "UserTypeID", "UserTypeName", UserTypePermission.UserTypeID);
            ViewData["PermissionID"] = new SelectList(UserTypePermission_PermissionData.List(), "PermissionID", "PermissionName", UserTypePermission.PermissionID);

            return View(UserTypePermission);
        }

        // GET: /UserTypePermission/Edit/<id>
        public ActionResult Edit(
                                   Int32? UserTypeID
                                  ,Int32? UserTypePermissionID
                                )
        {
            if (
                    UserTypeID == null
                 || UserTypePermissionID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserTypePermission UserTypePermission = new UserTypePermission();
            UserTypePermission.UserTypeID = System.Convert.ToInt32(UserTypeID);
            UserTypePermission.UserTypePermissionID = System.Convert.ToInt32(UserTypePermissionID);
            UserTypePermission = UserTypePermissionData.Select_Record(UserTypePermission);

            if (UserTypePermission == null)
            {
                return HttpNotFound();
            }
        // ComboBox
            ViewData["UserTypeID"] = new SelectList(UserTypePermission_UserTypeData.List(), "UserTypeID", "UserTypeName", UserTypePermission.UserTypeID);
            ViewData["PermissionID"] = new SelectList(UserTypePermission_PermissionData.List(), "PermissionID", "PermissionName", UserTypePermission.PermissionID);

            return View(UserTypePermission);
        }

        // POST: /UserTypePermission/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserTypePermission UserTypePermission)
        {

            UserTypePermission oUserTypePermission = new UserTypePermission();
            oUserTypePermission.UserTypeID = System.Convert.ToInt32(UserTypePermission.UserTypeID);
            oUserTypePermission.UserTypePermissionID = System.Convert.ToInt32(UserTypePermission.UserTypePermissionID);
            oUserTypePermission = UserTypePermissionData.Select_Record(UserTypePermission);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = UserTypePermissionData.Update(oUserTypePermission, UserTypePermission);
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
            ViewData["UserTypeID"] = new SelectList(UserTypePermission_UserTypeData.List(), "UserTypeID", "UserTypeName", UserTypePermission.UserTypeID);
            ViewData["PermissionID"] = new SelectList(UserTypePermission_PermissionData.List(), "PermissionID", "PermissionName", UserTypePermission.PermissionID);

            return View(UserTypePermission);
        }

        // GET: /UserTypePermission/Delete/<id>
        public ActionResult Delete(
                                     Int32? UserTypeID
                                    ,Int32? UserTypePermissionID
                                  )
        {
            if (
                    UserTypeID == null
                 || UserTypePermissionID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtUserType = UserTypePermission_UserTypeData.SelectAll();
            dtPermission = UserTypePermission_PermissionData.SelectAll();

            UserTypePermission UserTypePermission = new UserTypePermission();
            UserTypePermission.UserTypeID = System.Convert.ToInt32(UserTypeID);
            UserTypePermission.UserTypePermissionID = System.Convert.ToInt32(UserTypePermissionID);
            UserTypePermission = UserTypePermissionData.Select_Record(UserTypePermission);
            UserTypePermission.UserType = new UserType()
            {
                UserTypeID = (Int32)UserTypePermission.UserTypeID
               ,UserTypeName = (from DataRow rowUserType in dtUserType.Rows
                      where UserTypePermission.UserTypeID == (int)rowUserType["UserTypeID"]
                      select (String)rowUserType["UserTypeName"]).FirstOrDefault()
            };
            UserTypePermission.Permission = new Permission()
            {
                PermissionID = (Int32)UserTypePermission.PermissionID
               ,PermissionName = (from DataRow rowPermission in dtPermission.Rows
                      where UserTypePermission.PermissionID == (int)rowPermission["PermissionID"]
                      select (String)rowPermission["PermissionName"]).FirstOrDefault()
            };

            if (UserTypePermission == null)
            {
                return HttpNotFound();
            }
            return View(UserTypePermission);
        }

        // POST: /UserTypePermission/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? UserTypeID
                                            ,Int32? UserTypePermissionID
                                            )
        {

            UserTypePermission UserTypePermission = new UserTypePermission();
            UserTypePermission.UserTypeID = System.Convert.ToInt32(UserTypeID);
            UserTypePermission.UserTypePermissionID = System.Convert.ToInt32(UserTypePermissionID);
            UserTypePermission = UserTypePermissionData.Select_Record(UserTypePermission);

            bool bSucess = false;
            bSucess = UserTypePermissionData.Delete(UserTypePermission);
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
            SelectListItem Item1 = new SelectListItem { Text = "User Type I D", Value = "User Type I D" };
            SelectListItem Item2 = new SelectListItem { Text = "User Type Permission I D", Value = "User Type Permission I D" };
            SelectListItem Item3 = new SelectListItem { Text = "Permission I D", Value = "Permission I D" };

                 if (select == "User Type I D") { Item1.Selected = true; }
            else if (select == "User Type Permission I D") { Item2.Selected = true; }
            else if (select == "Permission I D") { Item3.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. User Type Permission", "Many");
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
 
