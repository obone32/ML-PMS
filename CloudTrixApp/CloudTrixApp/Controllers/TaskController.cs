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
    public class TaskController : Controller
    {

        DataTable dtTask = new DataTable();

        // GET: /Task/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Task I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["TaskIDSortParm"] = sortOrder == "TaskID_asc" ? "TaskID_desc" : "TaskID_asc";
            ViewData["TaskNameSortParm"] = sortOrder == "TaskName_asc" ? "TaskName_desc" : "TaskName_asc";
            ViewData["DescriptionSortParm"] = sortOrder == "Description_asc" ? "Description_desc" : "Description_asc";
            ViewData["CreationDateSortParm"] = sortOrder == "CreationDate_asc" ? "CreationDate_desc" : "CreationDate_asc";

            dtTask = TaskData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtTask = TaskData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowTask in dtTask.AsEnumerable()
                        select new Task() {
                            TaskID = rowTask.Field<Int32>("TaskID")
                           ,TaskName = rowTask.Field<String>("TaskName")
                           ,Description = rowTask.Field<String>("Description")
                           ,CreationDate = rowTask.Field<DateTime>("CreationDate")
                        };

            switch (sortOrder)
            {
                case "TaskID_desc":
                    Query = Query.OrderByDescending(s => s.TaskID);
                    break;
                case "TaskID_asc":
                    Query = Query.OrderBy(s => s.TaskID);
                    break;
                case "TaskName_desc":
                    Query = Query.OrderByDescending(s => s.TaskName);
                    break;
                case "TaskName_asc":
                    Query = Query.OrderBy(s => s.TaskName);
                    break;
                case "Description_desc":
                    Query = Query.OrderByDescending(s => s.Description);
                    break;
                case "Description_asc":
                    Query = Query.OrderBy(s => s.Description);
                    break;
                case "CreationDate_desc":
                    Query = Query.OrderByDescending(s => s.CreationDate);
                    break;
                case "CreationDate_asc":
                    Query = Query.OrderBy(s => s.CreationDate);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.TaskID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Task I D", typeof(string));
                dt.Columns.Add("Task Name", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Creation Date", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.TaskID
                       ,item.TaskName
                       ,item.Description
                       ,item.CreationDate
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

        // GET: /Task/Details/<id>
        public ActionResult Details(
                                      Int32? TaskID
                                   )
        {
            if (
                    TaskID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Task Task = new Task();
            Task.TaskID = System.Convert.ToInt32(TaskID);
            Task = TaskData.Select_Record(Task);

            if (Task == null)
            {
                return HttpNotFound();
            }
            return View(Task);
        }

        // GET: /Task/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /Task/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "TaskName"
				   + "," + "Description"
				   + "," + "CreationDate"
				  )] Task Task)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = TaskData.Add(Task);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(Task);
        }

        // GET: /Task/Edit/<id>
        public ActionResult Edit(
                                   Int32? TaskID
                                )
        {
            if (
                    TaskID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Task Task = new Task();
            Task.TaskID = System.Convert.ToInt32(TaskID);
            Task = TaskData.Select_Record(Task);

            if (Task == null)
            {
                return HttpNotFound();
            }

            return View(Task);
        }

        // POST: /Task/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Task Task)
        {

            Task oTask = new Task();
            oTask.TaskID = System.Convert.ToInt32(Task.TaskID);
            oTask = TaskData.Select_Record(Task);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = TaskData.Update(oTask, Task);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(Task);
        }

        // GET: /Task/Delete/<id>
        public ActionResult Delete(
                                     Int32? TaskID
                                  )
        {
            if (
                    TaskID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Task Task = new Task();
            Task.TaskID = System.Convert.ToInt32(TaskID);
            Task = TaskData.Select_Record(Task);

            if (Task == null)
            {
                return HttpNotFound();
            }
            return View(Task);
        }

        // POST: /Task/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? TaskID
                                            )
        {

            Task Task = new Task();
            Task.TaskID = System.Convert.ToInt32(TaskID);
            Task = TaskData.Select_Record(Task);

            bool bSucess = false;
            bSucess = TaskData.Delete(Task);
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
            SelectListItem Item1 = new SelectListItem { Text = "Task I D", Value = "Task I D" };
            SelectListItem Item2 = new SelectListItem { Text = "Task Name", Value = "Task Name" };
            SelectListItem Item3 = new SelectListItem { Text = "Description", Value = "Description" };
            SelectListItem Item4 = new SelectListItem { Text = "Creation Date", Value = "Creation Date" };

                 if (select == "Task I D") { Item1.Selected = true; }
            else if (select == "Task Name") { Item2.Selected = true; }
            else if (select == "Description") { Item3.Selected = true; }
            else if (select == "Creation Date") { Item4.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Task", "Many");
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
 
