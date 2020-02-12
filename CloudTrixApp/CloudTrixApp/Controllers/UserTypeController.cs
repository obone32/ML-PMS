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
    public class UserTypeController : Controller
    {

        DataTable dtUserType = new DataTable();

        // GET: /UserType/
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
            ViewData["UserTypeNameSortParm"] = sortOrder == "UserTypeName_asc" ? "UserTypeName_desc" : "UserTypeName_asc";

            dtUserType = UserTypeData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtUserType = UserTypeData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowUserType in dtUserType.AsEnumerable()
                        select new UserType() {
                            UserTypeID = rowUserType.Field<Int32>("UserTypeID")
                           ,UserTypeName = rowUserType.Field<String>("UserTypeName")
                        };

            switch (sortOrder)
            {
                case "UserTypeID_desc":
                    Query = Query.OrderByDescending(s => s.UserTypeID);
                    break;
                case "UserTypeID_asc":
                    Query = Query.OrderBy(s => s.UserTypeID);
                    break;
                case "UserTypeName_desc":
                    Query = Query.OrderByDescending(s => s.UserTypeName);
                    break;
                case "UserTypeName_asc":
                    Query = Query.OrderBy(s => s.UserTypeName);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.UserTypeID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("User Type I D", typeof(string));
                dt.Columns.Add("User Type Name", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.UserTypeID
                       ,item.UserTypeName
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

        // GET: /UserType/Details/<id>
        public ActionResult Details(
                                      Int32? UserTypeID
                                   )
        {
            if (
                    UserTypeID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            UserType UserType = new UserType();
            UserType.UserTypeID = System.Convert.ToInt32(UserTypeID);
            UserType = UserTypeData.Select_Record(UserType);

            if (UserType == null)
            {
                return HttpNotFound();
            }
            return View(UserType);
        }

        // GET: /UserType/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /UserType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "UserTypeName"
				  )] UserType UserType)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = UserTypeData.Add(UserType);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(UserType);
        }

        // GET: /UserType/Edit/<id>
        public ActionResult Edit(
                                   Int32? UserTypeID
                                )
        {
            if (
                    UserTypeID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserType UserType = new UserType();
            UserType.UserTypeID = System.Convert.ToInt32(UserTypeID);
            UserType = UserTypeData.Select_Record(UserType);

            if (UserType == null)
            {
                return HttpNotFound();
            }

            return View(UserType);
        }

        // POST: /UserType/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserType UserType)
        {

            UserType oUserType = new UserType();
            oUserType.UserTypeID = System.Convert.ToInt32(UserType.UserTypeID);
            oUserType = UserTypeData.Select_Record(UserType);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = UserTypeData.Update(oUserType, UserType);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(UserType);
        }

        // GET: /UserType/Delete/<id>
        public ActionResult Delete(
                                     Int32? UserTypeID
                                  )
        {
            if (
                    UserTypeID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            UserType UserType = new UserType();
            UserType.UserTypeID = System.Convert.ToInt32(UserTypeID);
            UserType = UserTypeData.Select_Record(UserType);

            if (UserType == null)
            {
                return HttpNotFound();
            }
            return View(UserType);
        }

        // POST: /UserType/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? UserTypeID
                                            )
        {

            UserType UserType = new UserType();
            UserType.UserTypeID = System.Convert.ToInt32(UserTypeID);
            UserType = UserTypeData.Select_Record(UserType);

            bool bSucess = false;
            bSucess = UserTypeData.Delete(UserType);
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
            SelectListItem Item2 = new SelectListItem { Text = "User Type Name", Value = "User Type Name" };

                 if (select == "User Type I D") { Item1.Selected = true; }
            else if (select == "User Type Name") { Item2.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. User Type", "Many");
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
 
