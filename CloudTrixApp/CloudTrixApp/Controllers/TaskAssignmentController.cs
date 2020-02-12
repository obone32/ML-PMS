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
    public class TaskAssignmentController : Controller
    {

        DataTable dtTaskAssignment = new DataTable();
        DataTable dtTask = new DataTable();
        DataTable dtEmployee = new DataTable();
        DataTable dtTaskState = new DataTable();

        // GET: /TaskAssignment/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Task Assignment I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["TaskAssignmentIDSortParm"] = sortOrder == "TaskAssignmentID_asc" ? "TaskAssignmentID_desc" : "TaskAssignmentID_asc";
            ViewData["AssignmentDateSortParm"] = sortOrder == "AssignmentDate_asc" ? "AssignmentDate_desc" : "AssignmentDate_asc";
            ViewData["TaskIDSortParm"] = sortOrder == "TaskID_asc" ? "TaskID_desc" : "TaskID_asc";
            ViewData["EmployeeIDSortParm"] = sortOrder == "EmployeeID_asc" ? "EmployeeID_desc" : "EmployeeID_asc";
            ViewData["TaskStateIDSortParm"] = sortOrder == "TaskStateID_asc" ? "TaskStateID_desc" : "TaskStateID_asc";

            dtTaskAssignment = TaskAssignmentData.SelectAll();
            dtTask = TaskAssignment_TaskData.SelectAll();
            dtEmployee = TaskAssignment_EmployeeData.SelectAll();
            dtTaskState = TaskAssignment_TaskStateData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtTaskAssignment = TaskAssignmentData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowTaskAssignment in dtTaskAssignment.AsEnumerable()
                        join rowTask in dtTask.AsEnumerable() on rowTaskAssignment.Field<Int32>("TaskID") equals rowTask.Field<Int32>("TaskID")
                        join rowEmployee in dtEmployee.AsEnumerable() on rowTaskAssignment.Field<Int32>("EmployeeID") equals rowEmployee.Field<Int32>("EmployeeID")
                        join rowTaskState in dtTaskState.AsEnumerable() on rowTaskAssignment.Field<Int32>("TaskStateID") equals rowTaskState.Field<Int32>("TaskStateID")
                        select new TaskAssignment() {
                            TaskAssignmentID = rowTaskAssignment.Field<Int32>("TaskAssignmentID")
                           ,AssignmentDate = rowTaskAssignment.Field<DateTime>("AssignmentDate")
                           ,
                            Task = new Task() 
                            {
                                   TaskID = rowTask.Field<Int32>("TaskID")
                                  ,TaskName = rowTask.Field<String>("TaskName")
                            }
                           ,
                            Employee = new Employee() 
                            {
                                   EmployeeID = rowEmployee.Field<Int32>("EmployeeID")
                                  ,FirstName = rowEmployee.Field<String>("FirstName")
                            }
                           ,
                            TaskState = new TaskState() 
                            {
                                   TaskStateID = rowTaskState.Field<Int32>("TaskStateID")
                                  ,TaskStateName = rowTaskState.Field<String>("TaskStateName")
                            }
                        };

            switch (sortOrder)
            {
                case "TaskAssignmentID_desc":
                    Query = Query.OrderByDescending(s => s.TaskAssignmentID);
                    break;
                case "TaskAssignmentID_asc":
                    Query = Query.OrderBy(s => s.TaskAssignmentID);
                    break;
                case "AssignmentDate_desc":
                    Query = Query.OrderByDescending(s => s.AssignmentDate);
                    break;
                case "AssignmentDate_asc":
                    Query = Query.OrderBy(s => s.AssignmentDate);
                    break;
                case "TaskID_desc":
                    Query = Query.OrderByDescending(s => s.Task.TaskName);
                    break;
                case "TaskID_asc":
                    Query = Query.OrderBy(s => s.Task.TaskName);
                    break;
                case "EmployeeID_desc":
                    Query = Query.OrderByDescending(s => s.Employee.FirstName);
                    break;
                case "EmployeeID_asc":
                    Query = Query.OrderBy(s => s.Employee.FirstName);
                    break;
                case "TaskStateID_desc":
                    Query = Query.OrderByDescending(s => s.TaskState.TaskStateName);
                    break;
                case "TaskStateID_asc":
                    Query = Query.OrderBy(s => s.TaskState.TaskStateName);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.TaskAssignmentID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Task Assignment I D", typeof(string));
                dt.Columns.Add("Assignment Date", typeof(string));
                dt.Columns.Add("Task I D", typeof(string));
                dt.Columns.Add("Employee I D", typeof(string));
                dt.Columns.Add("Task State I D", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.TaskAssignmentID
                       ,item.AssignmentDate
                       ,item.Task.TaskName
                       ,item.Employee.FirstName
                       ,item.TaskState.TaskStateName
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

        // GET: /TaskAssignment/Details/<id>
        public ActionResult Details(
                                      Int32? TaskAssignmentID
                                   )
        {
            if (
                    TaskAssignmentID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtTask = TaskAssignment_TaskData.SelectAll();
            dtEmployee = TaskAssignment_EmployeeData.SelectAll();
            dtTaskState = TaskAssignment_TaskStateData.SelectAll();

            TaskAssignment TaskAssignment = new TaskAssignment();
            TaskAssignment.TaskAssignmentID = System.Convert.ToInt32(TaskAssignmentID);
            TaskAssignment = TaskAssignmentData.Select_Record(TaskAssignment);
            TaskAssignment.Task = new Task()
            {
                TaskID = (Int32)TaskAssignment.TaskID
               ,TaskName = (from DataRow rowTask in dtTask.Rows
                      where TaskAssignment.TaskID == (int)rowTask["TaskID"]
                      select (String)rowTask["TaskName"]).FirstOrDefault()
            };
            TaskAssignment.Employee = new Employee()
            {
                EmployeeID = (Int32)TaskAssignment.EmployeeID
               ,FirstName = (from DataRow rowEmployee in dtEmployee.Rows
                      where TaskAssignment.EmployeeID == (int)rowEmployee["EmployeeID"]
                      select (String)rowEmployee["FirstName"]).FirstOrDefault()
            };
            TaskAssignment.TaskState = new TaskState()
            {
                TaskStateID = (Int32)TaskAssignment.TaskStateID
               ,TaskStateName = (from DataRow rowTaskState in dtTaskState.Rows
                      where TaskAssignment.TaskStateID == (int)rowTaskState["TaskStateID"]
                      select (String)rowTaskState["TaskStateName"]).FirstOrDefault()
            };

            if (TaskAssignment == null)
            {
                return HttpNotFound();
            }
            return View(TaskAssignment);
        }

        // GET: /TaskAssignment/Create
        public ActionResult Create()
        {
        // ComboBox
            ViewData["TaskID"] = new SelectList(TaskAssignment_TaskData.List(), "TaskID", "TaskName");
            ViewData["EmployeeID"] = new SelectList(TaskAssignment_EmployeeData.List(), "EmployeeID", "FirstName");
            ViewData["TaskStateID"] = new SelectList(TaskAssignment_TaskStateData.List(), "TaskStateID", "TaskStateName");

            return View();
        }

        // POST: /TaskAssignment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "AssignmentDate"
				   + "," + "TaskID"
				   + "," + "EmployeeID"
				   + "," + "TaskStateID"
				  )] TaskAssignment TaskAssignment)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = TaskAssignmentData.Add(TaskAssignment);
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
            ViewData["TaskID"] = new SelectList(TaskAssignment_TaskData.List(), "TaskID", "TaskName", TaskAssignment.TaskID);
            ViewData["EmployeeID"] = new SelectList(TaskAssignment_EmployeeData.List(), "EmployeeID", "FirstName", TaskAssignment.EmployeeID);
            ViewData["TaskStateID"] = new SelectList(TaskAssignment_TaskStateData.List(), "TaskStateID", "TaskStateName", TaskAssignment.TaskStateID);

            return View(TaskAssignment);
        }

        // GET: /TaskAssignment/Edit/<id>
        public ActionResult Edit(
                                   Int32? TaskAssignmentID
                                )
        {
            if (
                    TaskAssignmentID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TaskAssignment TaskAssignment = new TaskAssignment();
            TaskAssignment.TaskAssignmentID = System.Convert.ToInt32(TaskAssignmentID);
            TaskAssignment = TaskAssignmentData.Select_Record(TaskAssignment);

            if (TaskAssignment == null)
            {
                return HttpNotFound();
            }
        // ComboBox
            ViewData["TaskID"] = new SelectList(TaskAssignment_TaskData.List(), "TaskID", "TaskName", TaskAssignment.TaskID);
            ViewData["EmployeeID"] = new SelectList(TaskAssignment_EmployeeData.List(), "EmployeeID", "FirstName", TaskAssignment.EmployeeID);
            ViewData["TaskStateID"] = new SelectList(TaskAssignment_TaskStateData.List(), "TaskStateID", "TaskStateName", TaskAssignment.TaskStateID);

            return View(TaskAssignment);
        }

        // POST: /TaskAssignment/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskAssignment TaskAssignment)
        {

            TaskAssignment oTaskAssignment = new TaskAssignment();
            oTaskAssignment.TaskAssignmentID = System.Convert.ToInt32(TaskAssignment.TaskAssignmentID);
            oTaskAssignment = TaskAssignmentData.Select_Record(TaskAssignment);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = TaskAssignmentData.Update(oTaskAssignment, TaskAssignment);
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
            ViewData["TaskID"] = new SelectList(TaskAssignment_TaskData.List(), "TaskID", "TaskName", TaskAssignment.TaskID);
            ViewData["EmployeeID"] = new SelectList(TaskAssignment_EmployeeData.List(), "EmployeeID", "FirstName", TaskAssignment.EmployeeID);
            ViewData["TaskStateID"] = new SelectList(TaskAssignment_TaskStateData.List(), "TaskStateID", "TaskStateName", TaskAssignment.TaskStateID);

            return View(TaskAssignment);
        }

        // GET: /TaskAssignment/Delete/<id>
        public ActionResult Delete(
                                     Int32? TaskAssignmentID
                                  )
        {
            if (
                    TaskAssignmentID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtTask = TaskAssignment_TaskData.SelectAll();
            dtEmployee = TaskAssignment_EmployeeData.SelectAll();
            dtTaskState = TaskAssignment_TaskStateData.SelectAll();

            TaskAssignment TaskAssignment = new TaskAssignment();
            TaskAssignment.TaskAssignmentID = System.Convert.ToInt32(TaskAssignmentID);
            TaskAssignment = TaskAssignmentData.Select_Record(TaskAssignment);
            TaskAssignment.Task = new Task()
            {
                TaskID = (Int32)TaskAssignment.TaskID
               ,TaskName = (from DataRow rowTask in dtTask.Rows
                      where TaskAssignment.TaskID == (int)rowTask["TaskID"]
                      select (String)rowTask["TaskName"]).FirstOrDefault()
            };
            TaskAssignment.Employee = new Employee()
            {
                EmployeeID = (Int32)TaskAssignment.EmployeeID
               ,FirstName = (from DataRow rowEmployee in dtEmployee.Rows
                      where TaskAssignment.EmployeeID == (int)rowEmployee["EmployeeID"]
                      select (String)rowEmployee["FirstName"]).FirstOrDefault()
            };
            TaskAssignment.TaskState = new TaskState()
            {
                TaskStateID = (Int32)TaskAssignment.TaskStateID
               ,TaskStateName = (from DataRow rowTaskState in dtTaskState.Rows
                      where TaskAssignment.TaskStateID == (int)rowTaskState["TaskStateID"]
                      select (String)rowTaskState["TaskStateName"]).FirstOrDefault()
            };

            if (TaskAssignment == null)
            {
                return HttpNotFound();
            }
            return View(TaskAssignment);
        }

        // POST: /TaskAssignment/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? TaskAssignmentID
                                            )
        {

            TaskAssignment TaskAssignment = new TaskAssignment();
            TaskAssignment.TaskAssignmentID = System.Convert.ToInt32(TaskAssignmentID);
            TaskAssignment = TaskAssignmentData.Select_Record(TaskAssignment);

            bool bSucess = false;
            bSucess = TaskAssignmentData.Delete(TaskAssignment);
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
            SelectListItem Item1 = new SelectListItem { Text = "Task Assignment I D", Value = "Task Assignment I D" };
            SelectListItem Item2 = new SelectListItem { Text = "Assignment Date", Value = "Assignment Date" };
            SelectListItem Item3 = new SelectListItem { Text = "Task I D", Value = "Task I D" };
            SelectListItem Item4 = new SelectListItem { Text = "Employee I D", Value = "Employee I D" };
            SelectListItem Item5 = new SelectListItem { Text = "Task State I D", Value = "Task State I D" };

                 if (select == "Task Assignment I D") { Item1.Selected = true; }
            else if (select == "Assignment Date") { Item2.Selected = true; }
            else if (select == "Task I D") { Item3.Selected = true; }
            else if (select == "Employee I D") { Item4.Selected = true; }
            else if (select == "Task State I D") { Item5.Selected = true; }

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
                PDFform pdfForm = new PDFform(dt, "Dbo. Task Assignment", "Many");
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
 
