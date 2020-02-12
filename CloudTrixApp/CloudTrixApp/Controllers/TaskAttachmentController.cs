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
    public class TaskAttachmentController : Controller
    {

        DataTable dtTaskAttachment = new DataTable();
        DataTable dtTask = new DataTable();

        // GET: /TaskAttachment/
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
            ViewData["TaskAttachmentIDSortParm"] = sortOrder == "TaskAttachmentID_asc" ? "TaskAttachmentID_desc" : "TaskAttachmentID_asc";
            ViewData["AttachmentNameSortParm"] = sortOrder == "AttachmentName_asc" ? "AttachmentName_desc" : "AttachmentName_asc";
            ViewData["DecriptionSortParm"] = sortOrder == "Decription_asc" ? "Decription_desc" : "Decription_asc";
            ViewData["FilePathSortParm"] = sortOrder == "FilePath_asc" ? "FilePath_desc" : "FilePath_asc";

            dtTaskAttachment = TaskAttachmentData.SelectAll();
            dtTask = TaskAttachment_TaskData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtTaskAttachment = TaskAttachmentData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowTaskAttachment in dtTaskAttachment.AsEnumerable()
                        join rowTask in dtTask.AsEnumerable() on rowTaskAttachment.Field<Int32>("TaskID") equals rowTask.Field<Int32>("TaskID")
                        select new TaskAttachment() {
                            Task = new Task() 
                            {
                                   TaskID = rowTask.Field<Int32>("TaskID")
                            }
                           ,TaskAttachmentID = rowTaskAttachment.Field<Int32>("TaskAttachmentID")
                           ,AttachmentName = rowTaskAttachment.Field<String>("AttachmentName")
                           ,Decription = rowTaskAttachment.Field<String>("Decription")
                           ,FilePath = rowTaskAttachment.Field<String>("FilePath")
                        };

            switch (sortOrder)
            {
                case "TaskID_desc":
                    Query = Query.OrderByDescending(s => s.Task.TaskID);
                    break;
                case "TaskID_asc":
                    Query = Query.OrderBy(s => s.Task.TaskID);
                    break;
                case "TaskAttachmentID_desc":
                    Query = Query.OrderByDescending(s => s.TaskAttachmentID);
                    break;
                case "TaskAttachmentID_asc":
                    Query = Query.OrderBy(s => s.TaskAttachmentID);
                    break;
                case "AttachmentName_desc":
                    Query = Query.OrderByDescending(s => s.AttachmentName);
                    break;
                case "AttachmentName_asc":
                    Query = Query.OrderBy(s => s.AttachmentName);
                    break;
                case "Decription_desc":
                    Query = Query.OrderByDescending(s => s.Decription);
                    break;
                case "Decription_asc":
                    Query = Query.OrderBy(s => s.Decription);
                    break;
                case "FilePath_desc":
                    Query = Query.OrderByDescending(s => s.FilePath);
                    break;
                case "FilePath_asc":
                    Query = Query.OrderBy(s => s.FilePath);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.TaskID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Task I D", typeof(string));
                dt.Columns.Add("Task Attachment I D", typeof(string));
                dt.Columns.Add("Attachment Name", typeof(string));
                dt.Columns.Add("Decription", typeof(string));
                dt.Columns.Add("File Path", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.Task.TaskID
                       ,item.TaskAttachmentID
                       ,item.AttachmentName
                       ,item.Decription
                       ,item.FilePath
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

        // GET: /TaskAttachment/Details/<id>
        public ActionResult Details(
                                      Int32? TaskID
                                     ,Int32? TaskAttachmentID
                                   )
        {
            if (
                    TaskID == null
                 || TaskAttachmentID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtTask = TaskAttachment_TaskData.SelectAll();

            TaskAttachment TaskAttachment = new TaskAttachment();
            TaskAttachment.TaskID = System.Convert.ToInt32(TaskID);
            TaskAttachment.TaskAttachmentID = System.Convert.ToInt32(TaskAttachmentID);
            TaskAttachment = TaskAttachmentData.Select_Record(TaskAttachment);
            TaskAttachment.Task = new Task()
            {
                TaskID = (Int32)TaskAttachment.TaskID
            };

            if (TaskAttachment == null)
            {
                return HttpNotFound();
            }
            return View(TaskAttachment);
        }

        // GET: /TaskAttachment/Create
        public ActionResult Create()
        {
        // ComboBox
            ViewData["TaskID"] = new SelectList(TaskAttachment_TaskData.List(), "TaskID", "TaskID");

            return View();
        }

        // POST: /TaskAttachment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "TaskID"
				   + "," + "TaskAttachmentID"
				   + "," + "AttachmentName"
				   + "," + "Decription"
				   + "," + "FilePath"
				  )] TaskAttachment TaskAttachment)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = TaskAttachmentData.Add(TaskAttachment);
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
            ViewData["TaskID"] = new SelectList(TaskAttachment_TaskData.List(), "TaskID", "TaskID", TaskAttachment.TaskID);

            return View(TaskAttachment);
        }

        // GET: /TaskAttachment/Edit/<id>
        public ActionResult Edit(
                                   Int32? TaskID
                                  ,Int32? TaskAttachmentID
                                )
        {
            if (
                    TaskID == null
                 || TaskAttachmentID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TaskAttachment TaskAttachment = new TaskAttachment();
            TaskAttachment.TaskID = System.Convert.ToInt32(TaskID);
            TaskAttachment.TaskAttachmentID = System.Convert.ToInt32(TaskAttachmentID);
            TaskAttachment = TaskAttachmentData.Select_Record(TaskAttachment);

            if (TaskAttachment == null)
            {
                return HttpNotFound();
            }
        // ComboBox
            ViewData["TaskID"] = new SelectList(TaskAttachment_TaskData.List(), "TaskID", "TaskID", TaskAttachment.TaskID);

            return View(TaskAttachment);
        }

        // POST: /TaskAttachment/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskAttachment TaskAttachment)
        {

            TaskAttachment oTaskAttachment = new TaskAttachment();
            oTaskAttachment.TaskID = System.Convert.ToInt32(TaskAttachment.TaskID);
            oTaskAttachment.TaskAttachmentID = System.Convert.ToInt32(TaskAttachment.TaskAttachmentID);
            oTaskAttachment = TaskAttachmentData.Select_Record(TaskAttachment);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = TaskAttachmentData.Update(oTaskAttachment, TaskAttachment);
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
            ViewData["TaskID"] = new SelectList(TaskAttachment_TaskData.List(), "TaskID", "TaskID", TaskAttachment.TaskID);

            return View(TaskAttachment);
        }

        // GET: /TaskAttachment/Delete/<id>
        public ActionResult Delete(
                                     Int32? TaskID
                                    ,Int32? TaskAttachmentID
                                  )
        {
            if (
                    TaskID == null
                 || TaskAttachmentID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtTask = TaskAttachment_TaskData.SelectAll();

            TaskAttachment TaskAttachment = new TaskAttachment();
            TaskAttachment.TaskID = System.Convert.ToInt32(TaskID);
            TaskAttachment.TaskAttachmentID = System.Convert.ToInt32(TaskAttachmentID);
            TaskAttachment = TaskAttachmentData.Select_Record(TaskAttachment);
            TaskAttachment.Task = new Task()
            {
                TaskID = (Int32)TaskAttachment.TaskID
            };

            if (TaskAttachment == null)
            {
                return HttpNotFound();
            }
            return View(TaskAttachment);
        }

        // POST: /TaskAttachment/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? TaskID
                                            ,Int32? TaskAttachmentID
                                            )
        {

            TaskAttachment TaskAttachment = new TaskAttachment();
            TaskAttachment.TaskID = System.Convert.ToInt32(TaskID);
            TaskAttachment.TaskAttachmentID = System.Convert.ToInt32(TaskAttachmentID);
            TaskAttachment = TaskAttachmentData.Select_Record(TaskAttachment);

            bool bSucess = false;
            bSucess = TaskAttachmentData.Delete(TaskAttachment);
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
            SelectListItem Item2 = new SelectListItem { Text = "Task Attachment I D", Value = "Task Attachment I D" };
            SelectListItem Item3 = new SelectListItem { Text = "Attachment Name", Value = "Attachment Name" };
            SelectListItem Item4 = new SelectListItem { Text = "Decription", Value = "Decription" };
            SelectListItem Item5 = new SelectListItem { Text = "File Path", Value = "File Path" };

                 if (select == "Task I D") { Item1.Selected = true; }
            else if (select == "Task Attachment I D") { Item2.Selected = true; }
            else if (select == "Attachment Name") { Item3.Selected = true; }
            else if (select == "Decription") { Item4.Selected = true; }
            else if (select == "File Path") { Item5.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);
            list.Add(Item5);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Task Attachment", "Many");
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
 
