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
    public class TaskStateController : Controller
    {

        DataTable dtTaskState = new DataTable();

        // GET: /TaskState/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Task State I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["TaskStateIDSortParm"] = sortOrder == "TaskStateID_asc" ? "TaskStateID_desc" : "TaskStateID_asc";
            ViewData["TaskStateNameSortParm"] = sortOrder == "TaskStateName_asc" ? "TaskStateName_desc" : "TaskStateName_asc";

            dtTaskState = TaskStateData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtTaskState = TaskStateData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowTaskState in dtTaskState.AsEnumerable()
                        select new TaskState() {
                            TaskStateID = rowTaskState.Field<Int32>("TaskStateID")
                           ,TaskStateName = rowTaskState.Field<String>("TaskStateName")
                        };

            switch (sortOrder)
            {
                case "TaskStateID_desc":
                    Query = Query.OrderByDescending(s => s.TaskStateID);
                    break;
                case "TaskStateID_asc":
                    Query = Query.OrderBy(s => s.TaskStateID);
                    break;
                case "TaskStateName_desc":
                    Query = Query.OrderByDescending(s => s.TaskStateName);
                    break;
                case "TaskStateName_asc":
                    Query = Query.OrderBy(s => s.TaskStateName);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.TaskStateID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Task State I D", typeof(string));
                dt.Columns.Add("Task State Name", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.TaskStateID
                       ,item.TaskStateName
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

        // GET: /TaskState/Details/<id>
        public ActionResult Details(
                                      Int32? TaskStateID
                                   )
        {
            if (
                    TaskStateID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            TaskState TaskState = new TaskState();
            TaskState.TaskStateID = System.Convert.ToInt32(TaskStateID);
            TaskState = TaskStateData.Select_Record(TaskState);

            if (TaskState == null)
            {
                return HttpNotFound();
            }
            return View(TaskState);
        }

        // GET: /TaskState/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /TaskState/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "TaskStateName"
				  )] TaskState TaskState)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = TaskStateData.Add(TaskState);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(TaskState);
        }

        // GET: /TaskState/Edit/<id>
        public ActionResult Edit(
                                   Int32? TaskStateID
                                )
        {
            if (
                    TaskStateID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TaskState TaskState = new TaskState();
            TaskState.TaskStateID = System.Convert.ToInt32(TaskStateID);
            TaskState = TaskStateData.Select_Record(TaskState);

            if (TaskState == null)
            {
                return HttpNotFound();
            }

            return View(TaskState);
        }

        // POST: /TaskState/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskState TaskState)
        {

            TaskState oTaskState = new TaskState();
            oTaskState.TaskStateID = System.Convert.ToInt32(TaskState.TaskStateID);
            oTaskState = TaskStateData.Select_Record(TaskState);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = TaskStateData.Update(oTaskState, TaskState);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(TaskState);
        }

        // GET: /TaskState/Delete/<id>
        public ActionResult Delete(
                                     Int32? TaskStateID
                                  )
        {
            if (
                    TaskStateID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            TaskState TaskState = new TaskState();
            TaskState.TaskStateID = System.Convert.ToInt32(TaskStateID);
            TaskState = TaskStateData.Select_Record(TaskState);

            if (TaskState == null)
            {
                return HttpNotFound();
            }
            return View(TaskState);
        }

        // POST: /TaskState/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? TaskStateID
                                            )
        {

            TaskState TaskState = new TaskState();
            TaskState.TaskStateID = System.Convert.ToInt32(TaskStateID);
            TaskState = TaskStateData.Select_Record(TaskState);

            bool bSucess = false;
            bSucess = TaskStateData.Delete(TaskState);
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
            SelectListItem Item1 = new SelectListItem { Text = "Task State I D", Value = "Task State I D" };
            SelectListItem Item2 = new SelectListItem { Text = "Task State Name", Value = "Task State Name" };

                 if (select == "Task State I D") { Item1.Selected = true; }
            else if (select == "Task State Name") { Item2.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Task State", "Many");
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
 
