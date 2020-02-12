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
    public class ProjectStatusController : Controller
    {

        DataTable dtProjectStatus = new DataTable();

        // GET: /ProjectStatus/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Project Status I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["ProjectStatusIDSortParm"] = sortOrder == "ProjectStatusID_asc" ? "ProjectStatusID_desc" : "ProjectStatusID_asc";
            ViewData["ProjectStatusNameSortParm"] = sortOrder == "ProjectStatusName_asc" ? "ProjectStatusName_desc" : "ProjectStatusName_asc";

            dtProjectStatus = ProjectStatusData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtProjectStatus = ProjectStatusData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowProjectStatus in dtProjectStatus.AsEnumerable()
                        select new ProjectStatus() {
                            ProjectStatusID = rowProjectStatus.Field<Int32>("ProjectStatusID")
                           ,ProjectStatusName = rowProjectStatus.Field<String>("ProjectStatusName")
                        };

            switch (sortOrder)
            {
                case "ProjectStatusID_desc":
                    Query = Query.OrderByDescending(s => s.ProjectStatusID);
                    break;
                case "ProjectStatusID_asc":
                    Query = Query.OrderBy(s => s.ProjectStatusID);
                    break;
                case "ProjectStatusName_desc":
                    Query = Query.OrderByDescending(s => s.ProjectStatusName);
                    break;
                case "ProjectStatusName_asc":
                    Query = Query.OrderBy(s => s.ProjectStatusName);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.ProjectStatusID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Project Status I D", typeof(string));
                dt.Columns.Add("Project Status Name", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.ProjectStatusID
                       ,item.ProjectStatusName
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

        // GET: /ProjectStatus/Details/<id>
        public ActionResult Details(
                                      Int32? ProjectStatusID
                                   )
        {
            if (
                    ProjectStatusID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            ProjectStatus ProjectStatus = new ProjectStatus();
            ProjectStatus.ProjectStatusID = System.Convert.ToInt32(ProjectStatusID);
            ProjectStatus = ProjectStatusData.Select_Record(ProjectStatus);

            if (ProjectStatus == null)
            {
                return HttpNotFound();
            }
            return View(ProjectStatus);
        }

        // GET: /ProjectStatus/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /ProjectStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "ProjectStatusName"
				  )] ProjectStatus ProjectStatus)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = ProjectStatusData.Add(ProjectStatus);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(ProjectStatus);
        }

        // GET: /ProjectStatus/Edit/<id>
        public ActionResult Edit(
                                   Int32? ProjectStatusID
                                )
        {
            if (
                    ProjectStatusID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProjectStatus ProjectStatus = new ProjectStatus();
            ProjectStatus.ProjectStatusID = System.Convert.ToInt32(ProjectStatusID);
            ProjectStatus = ProjectStatusData.Select_Record(ProjectStatus);

            if (ProjectStatus == null)
            {
                return HttpNotFound();
            }

            return View(ProjectStatus);
        }

        // POST: /ProjectStatus/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectStatus ProjectStatus)
        {

            ProjectStatus oProjectStatus = new ProjectStatus();
            oProjectStatus.ProjectStatusID = System.Convert.ToInt32(ProjectStatus.ProjectStatusID);
            oProjectStatus = ProjectStatusData.Select_Record(ProjectStatus);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = ProjectStatusData.Update(oProjectStatus, ProjectStatus);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(ProjectStatus);
        }

        // GET: /ProjectStatus/Delete/<id>
        public ActionResult Delete(
                                     Int32? ProjectStatusID
                                  )
        {
            if (
                    ProjectStatusID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            ProjectStatus ProjectStatus = new ProjectStatus();
            ProjectStatus.ProjectStatusID = System.Convert.ToInt32(ProjectStatusID);
            ProjectStatus = ProjectStatusData.Select_Record(ProjectStatus);

            if (ProjectStatus == null)
            {
                return HttpNotFound();
            }
            return View(ProjectStatus);
        }

        // POST: /ProjectStatus/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? ProjectStatusID
                                            )
        {

            ProjectStatus ProjectStatus = new ProjectStatus();
            ProjectStatus.ProjectStatusID = System.Convert.ToInt32(ProjectStatusID);
            ProjectStatus = ProjectStatusData.Select_Record(ProjectStatus);

            bool bSucess = false;
            bSucess = ProjectStatusData.Delete(ProjectStatus);
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
            SelectListItem Item1 = new SelectListItem { Text = "Project Status I D", Value = "Project Status I D" };
            SelectListItem Item2 = new SelectListItem { Text = "Project Status Name", Value = "Project Status Name" };

                 if (select == "Project Status I D") { Item1.Selected = true; }
            else if (select == "Project Status Name") { Item2.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Project Status", "Many");
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
 
