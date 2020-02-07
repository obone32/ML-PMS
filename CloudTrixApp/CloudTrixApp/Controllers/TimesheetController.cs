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
    public class TimesheetController : Controller
    {

        DataTable dtTimesheet = new DataTable();
        DataTable dtEmployee = new DataTable();
        DataTable dtProject = new DataTable();

        // GET: /Timesheet/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Timesheet I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["TimesheetIDSortParm"] = sortOrder == "TimesheetID_asc" ? "TimesheetID_desc" : "TimesheetID_asc";
            ViewData["EmployeeIDSortParm"] = sortOrder == "EmployeeID_asc" ? "EmployeeID_desc" : "EmployeeID_asc";
            ViewData["ProjectIDSortParm"] = sortOrder == "ProjectID_asc" ? "ProjectID_desc" : "ProjectID_asc";
            ViewData["EntryDateSortParm"] = sortOrder == "EntryDate_asc" ? "EntryDate_desc" : "EntryDate_asc";
            ViewData["StartTimeSortParm"] = sortOrder == "StartTime_asc" ? "StartTime_desc" : "StartTime_asc";
            ViewData["EndTimeSortParm"] = sortOrder == "EndTime_asc" ? "EndTime_desc" : "EndTime_asc";
            ViewData["TotTimeSortParm"] = sortOrder == "TotTime_asc" ? "TotTime_desc" : "TotTime_asc";
            ViewData["RemarksSortParm"] = sortOrder == "Remarks_asc" ? "Remarks_desc" : "Remarks_asc";

            dtTimesheet = TimesheetData.SelectAll();
            dtEmployee = Timesheet_EmployeeData.SelectAll();
            dtProject = Timesheet_ProjectData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtTimesheet = TimesheetData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowTimesheet in dtTimesheet.AsEnumerable()
                        join rowEmployee in dtEmployee.AsEnumerable() on rowTimesheet.Field<Int32>("EmployeeID") equals rowEmployee.Field<Int32>("EmployeeID")
                        join rowProject in dtProject.AsEnumerable() on rowTimesheet.Field<Int32>("ProjectID") equals rowProject.Field<Int32>("ProjectID")
                        select new Timesheet() {
                            TimesheetID = rowTimesheet.Field<Int32>("TimesheetID")
                           ,
                            Employee = new Employee() 
                            {
                                   EmployeeID = rowEmployee.Field<Int32>("EmployeeID")
                                  ,FirstName = rowEmployee.Field<String>("FirstName")
                            }
                           ,
                            Project = new Project() 
                            {
                                   ProjectID = rowProject.Field<Int32>("ProjectID")
                                  ,ProjectName = rowProject.Field<String>("ProjectName")
                            }
                           ,EntryDate = rowTimesheet.Field<DateTime>("EntryDate")
                           ,StartTime = rowTimesheet.Field<DateTime>("StartTime")
                           ,EndTime = rowTimesheet.Field<DateTime>("EndTime")
                           ,TotTime = rowTimesheet.Field<String>("TotTime")
                           ,Remarks = rowTimesheet.Field<String>("Remarks")
                        };

            switch (sortOrder)
            {
                case "TimesheetID_desc":
                    Query = Query.OrderByDescending(s => s.TimesheetID);
                    break;
                case "TimesheetID_asc":
                    Query = Query.OrderBy(s => s.TimesheetID);
                    break;
                case "EmployeeID_desc":
                    Query = Query.OrderByDescending(s => s.Employee.FirstName);
                    break;
                case "EmployeeID_asc":
                    Query = Query.OrderBy(s => s.Employee.FirstName);
                    break;
                case "ProjectID_desc":
                    Query = Query.OrderByDescending(s => s.Project.ProjectName);
                    break;
                case "ProjectID_asc":
                    Query = Query.OrderBy(s => s.Project.ProjectName);
                    break;
                case "EntryDate_desc":
                    Query = Query.OrderByDescending(s => s.EntryDate);
                    break;
                case "EntryDate_asc":
                    Query = Query.OrderBy(s => s.EntryDate);
                    break;
                case "StartTime_desc":
                    Query = Query.OrderByDescending(s => s.StartTime);
                    break;
                case "StartTime_asc":
                    Query = Query.OrderBy(s => s.StartTime);
                    break;
                case "EndTime_desc":
                    Query = Query.OrderByDescending(s => s.EndTime);
                    break;
                case "EndTime_asc":
                    Query = Query.OrderBy(s => s.EndTime);
                    break;
                case "TotTime_desc":
                    Query = Query.OrderByDescending(s => s.TotTime);
                    break;
                case "TotTime_asc":
                    Query = Query.OrderBy(s => s.TotTime);
                    break;
                case "Remarks_desc":
                    Query = Query.OrderByDescending(s => s.Remarks);
                    break;
                case "Remarks_asc":
                    Query = Query.OrderBy(s => s.Remarks);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.TimesheetID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Timesheet I D", typeof(string));
                dt.Columns.Add("Employee I D", typeof(string));
                dt.Columns.Add("Project I D", typeof(string));
                dt.Columns.Add("Entry Date", typeof(string));
                dt.Columns.Add("Start Time", typeof(string));
                dt.Columns.Add("End Time", typeof(string));
                dt.Columns.Add("Tot Time", typeof(string));
                dt.Columns.Add("Remarks", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.TimesheetID
                       ,item.Employee.FirstName
                       ,item.Project.ProjectName
                       ,item.EntryDate
                       ,item.StartTime
                       ,item.EndTime
                       ,item.TotTime
                       ,item.Remarks
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

        // GET: /Timesheet/Details/<id>
        public ActionResult Details(
                                      Int32? TimesheetID
                                   )
        {
            if (
                    TimesheetID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtEmployee = Timesheet_EmployeeData.SelectAll();
            dtProject = Timesheet_ProjectData.SelectAll();

            Timesheet Timesheet = new Timesheet();
            Timesheet.TimesheetID = System.Convert.ToInt32(TimesheetID);
            Timesheet = TimesheetData.Select_Record(Timesheet);
            Timesheet.Employee = new Employee()
            {
                EmployeeID = (Int32)Timesheet.EmployeeID
               ,FirstName = (from DataRow rowEmployee in dtEmployee.Rows
                      where Timesheet.EmployeeID == (int)rowEmployee["EmployeeID"]
                      select (String)rowEmployee["FirstName"]).FirstOrDefault()
            };
            Timesheet.Project = new Project()
            {
                ProjectID = (Int32)Timesheet.ProjectID
               ,ProjectName = (from DataRow rowProject in dtProject.Rows
                      where Timesheet.ProjectID == (int)rowProject["ProjectID"]
                      select (String)rowProject["ProjectName"]).FirstOrDefault()
            };

            if (Timesheet == null)
            {
                return HttpNotFound();
            }
            return View(Timesheet);
        }

        // GET: /Timesheet/Create
        public ActionResult Create()
        {
        // ComboBox
            ViewData["EmployeeID"] = new SelectList(Timesheet_EmployeeData.List(), "EmployeeID", "FirstName");
            ViewData["ProjectID"] = new SelectList(Timesheet_ProjectData.List(), "ProjectID", "ProjectName");

            return View();
        }

        // POST: /Timesheet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "EmployeeID"
				   + "," + "ProjectID"
				   + "," + "EntryDate"
				   + "," + "StartTime"
				   + "," + "EndTime"
				   + "," + "TotTime"
				   + "," + "Remarks"
				  )] Timesheet Timesheet)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = TimesheetData.Add(Timesheet);
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
            ViewData["EmployeeID"] = new SelectList(Timesheet_EmployeeData.List(), "EmployeeID", "FirstName", Timesheet.EmployeeID);
            ViewData["ProjectID"] = new SelectList(Timesheet_ProjectData.List(), "ProjectID", "ProjectName", Timesheet.ProjectID);

            return View(Timesheet);
        }

        // GET: /Timesheet/Edit/<id>
        public ActionResult Edit(
                                   Int32? TimesheetID
                                )
        {
            if (
                    TimesheetID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Timesheet Timesheet = new Timesheet();
            Timesheet.TimesheetID = System.Convert.ToInt32(TimesheetID);
            Timesheet = TimesheetData.Select_Record(Timesheet);

            if (Timesheet == null)
            {
                return HttpNotFound();
            }
        // ComboBox
            ViewData["EmployeeID"] = new SelectList(Timesheet_EmployeeData.List(), "EmployeeID", "FirstName", Timesheet.EmployeeID);
            ViewData["ProjectID"] = new SelectList(Timesheet_ProjectData.List(), "ProjectID", "ProjectName", Timesheet.ProjectID);

            return View(Timesheet);
        }

        // POST: /Timesheet/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Timesheet Timesheet)
        {

            Timesheet oTimesheet = new Timesheet();
            oTimesheet.TimesheetID = System.Convert.ToInt32(Timesheet.TimesheetID);
            oTimesheet = TimesheetData.Select_Record(Timesheet);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = TimesheetData.Update(oTimesheet, Timesheet);
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
            ViewData["EmployeeID"] = new SelectList(Timesheet_EmployeeData.List(), "EmployeeID", "FirstName", Timesheet.EmployeeID);
            ViewData["ProjectID"] = new SelectList(Timesheet_ProjectData.List(), "ProjectID", "ProjectName", Timesheet.ProjectID);

            return View(Timesheet);
        }

        // GET: /Timesheet/Delete/<id>
        public ActionResult Delete(
                                     Int32? TimesheetID
                                  )
        {
            if (
                    TimesheetID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtEmployee = Timesheet_EmployeeData.SelectAll();
            dtProject = Timesheet_ProjectData.SelectAll();

            Timesheet Timesheet = new Timesheet();
            Timesheet.TimesheetID = System.Convert.ToInt32(TimesheetID);
            Timesheet = TimesheetData.Select_Record(Timesheet);
            Timesheet.Employee = new Employee()
            {
                EmployeeID = (Int32)Timesheet.EmployeeID
               ,FirstName = (from DataRow rowEmployee in dtEmployee.Rows
                      where Timesheet.EmployeeID == (int)rowEmployee["EmployeeID"]
                      select (String)rowEmployee["FirstName"]).FirstOrDefault()
            };
            Timesheet.Project = new Project()
            {
                ProjectID = (Int32)Timesheet.ProjectID
               ,ProjectName = (from DataRow rowProject in dtProject.Rows
                      where Timesheet.ProjectID == (int)rowProject["ProjectID"]
                      select (String)rowProject["ProjectName"]).FirstOrDefault()
            };

            if (Timesheet == null)
            {
                return HttpNotFound();
            }
            return View(Timesheet);
        }

        // POST: /Timesheet/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? TimesheetID
                                            )
        {

            Timesheet Timesheet = new Timesheet();
            Timesheet.TimesheetID = System.Convert.ToInt32(TimesheetID);
            Timesheet = TimesheetData.Select_Record(Timesheet);

            bool bSucess = false;
            bSucess = TimesheetData.Delete(Timesheet);
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
            SelectListItem Item1 = new SelectListItem { Text = "Timesheet I D", Value = "Timesheet I D" };
            SelectListItem Item2 = new SelectListItem { Text = "Employee I D", Value = "Employee I D" };
            SelectListItem Item3 = new SelectListItem { Text = "Project I D", Value = "Project I D" };
            SelectListItem Item4 = new SelectListItem { Text = "Entry Date", Value = "Entry Date" };
            SelectListItem Item5 = new SelectListItem { Text = "Start Time", Value = "Start Time" };
            SelectListItem Item6 = new SelectListItem { Text = "End Time", Value = "End Time" };
            SelectListItem Item7 = new SelectListItem { Text = "Tot Time", Value = "Tot Time" };
            SelectListItem Item8 = new SelectListItem { Text = "Remarks", Value = "Remarks" };

                 if (select == "Timesheet I D") { Item1.Selected = true; }
            else if (select == "Employee I D") { Item2.Selected = true; }
            else if (select == "Project I D") { Item3.Selected = true; }
            else if (select == "Entry Date") { Item4.Selected = true; }
            else if (select == "Start Time") { Item5.Selected = true; }
            else if (select == "End Time") { Item6.Selected = true; }
            else if (select == "Tot Time") { Item7.Selected = true; }
            else if (select == "Remarks") { Item8.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);
            list.Add(Item5);
            list.Add(Item6);
            list.Add(Item7);
            list.Add(Item8);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Timesheet", "Many");
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
 
