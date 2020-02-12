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
    public class PermissionController : Controller
    {

        DataTable dtPermission = new DataTable();

        // GET: /Permission/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Permission I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["PermissionIDSortParm"] = sortOrder == "PermissionID_asc" ? "PermissionID_desc" : "PermissionID_asc";
            ViewData["PermissionNameSortParm"] = sortOrder == "PermissionName_asc" ? "PermissionName_desc" : "PermissionName_asc";

            dtPermission = PermissionData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtPermission = PermissionData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowPermission in dtPermission.AsEnumerable()
                        select new Permission() {
                            PermissionID = rowPermission.Field<Int32>("PermissionID")
                           ,PermissionName = rowPermission.Field<String>("PermissionName")
                        };

            switch (sortOrder)
            {
                case "PermissionID_desc":
                    Query = Query.OrderByDescending(s => s.PermissionID);
                    break;
                case "PermissionID_asc":
                    Query = Query.OrderBy(s => s.PermissionID);
                    break;
                case "PermissionName_desc":
                    Query = Query.OrderByDescending(s => s.PermissionName);
                    break;
                case "PermissionName_asc":
                    Query = Query.OrderBy(s => s.PermissionName);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.PermissionID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Permission I D", typeof(string));
                dt.Columns.Add("Permission Name", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.PermissionID
                       ,item.PermissionName
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

        // GET: /Permission/Details/<id>
        public ActionResult Details(
                                      Int32? PermissionID
                                   )
        {
            if (
                    PermissionID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Permission Permission = new Permission();
            Permission.PermissionID = System.Convert.ToInt32(PermissionID);
            Permission = PermissionData.Select_Record(Permission);

            if (Permission == null)
            {
                return HttpNotFound();
            }
            return View(Permission);
        }

        // GET: /Permission/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /Permission/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "PermissionName"
				  )] Permission Permission)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = PermissionData.Add(Permission);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(Permission);
        }

        // GET: /Permission/Edit/<id>
        public ActionResult Edit(
                                   Int32? PermissionID
                                )
        {
            if (
                    PermissionID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Permission Permission = new Permission();
            Permission.PermissionID = System.Convert.ToInt32(PermissionID);
            Permission = PermissionData.Select_Record(Permission);

            if (Permission == null)
            {
                return HttpNotFound();
            }

            return View(Permission);
        }

        // POST: /Permission/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Permission Permission)
        {

            Permission oPermission = new Permission();
            oPermission.PermissionID = System.Convert.ToInt32(Permission.PermissionID);
            oPermission = PermissionData.Select_Record(Permission);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = PermissionData.Update(oPermission, Permission);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(Permission);
        }

        // GET: /Permission/Delete/<id>
        public ActionResult Delete(
                                     Int32? PermissionID
                                  )
        {
            if (
                    PermissionID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Permission Permission = new Permission();
            Permission.PermissionID = System.Convert.ToInt32(PermissionID);
            Permission = PermissionData.Select_Record(Permission);

            if (Permission == null)
            {
                return HttpNotFound();
            }
            return View(Permission);
        }

        // POST: /Permission/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? PermissionID
                                            )
        {

            Permission Permission = new Permission();
            Permission.PermissionID = System.Convert.ToInt32(PermissionID);
            Permission = PermissionData.Select_Record(Permission);

            bool bSucess = false;
            bSucess = PermissionData.Delete(Permission);
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
            SelectListItem Item1 = new SelectListItem { Text = "Permission I D", Value = "Permission I D" };
            SelectListItem Item2 = new SelectListItem { Text = "Permission Name", Value = "Permission Name" };

                 if (select == "Permission I D") { Item1.Selected = true; }
            else if (select == "Permission Name") { Item2.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Permission", "Many");
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
 
