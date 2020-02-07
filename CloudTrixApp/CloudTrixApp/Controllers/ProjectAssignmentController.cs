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
    public class ProjectAssignmentController : Controller
    {

        DataTable dtProjectAssignment = new DataTable();
        DataTable dtProject = new DataTable();
        DataTable dtEmployee = new DataTable();

        // GET: /ProjectAssignment/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Project Assignment I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["ProjectAssignmentIDSortParm"] = sortOrder == "ProjectAssignmentID_asc" ? "ProjectAssignmentID_desc" : "ProjectAssignmentID_asc";
            ViewData["ProjectIDSortParm"] = sortOrder == "ProjectID_asc" ? "ProjectID_desc" : "ProjectID_asc";
            ViewData["EmployeeIDSortParm"] = sortOrder == "EmployeeID_asc" ? "EmployeeID_desc" : "EmployeeID_asc";
            ViewData["AssignmentDateSortParm"] = sortOrder == "AssignmentDate_asc" ? "AssignmentDate_desc" : "AssignmentDate_asc";
            ViewData["RemarksSortParm"] = sortOrder == "Remarks_asc" ? "Remarks_desc" : "Remarks_asc";
            ViewData["AddUserIDSortParm"] = sortOrder == "AddUserID_asc" ? "AddUserID_desc" : "AddUserID_asc";
            ViewData["AddDateSortParm"] = sortOrder == "AddDate_asc" ? "AddDate_desc" : "AddDate_asc";
            ViewData["ArchiveUserIDSortParm"] = sortOrder == "ArchiveUserID_asc" ? "ArchiveUserID_desc" : "ArchiveUserID_asc";
            ViewData["ArchiveDateSortParm"] = sortOrder == "ArchiveDate_asc" ? "ArchiveDate_desc" : "ArchiveDate_asc";

            dtProjectAssignment = ProjectAssignmentData.SelectAll();
            dtProject = ProjectAssignment_ProjectData.SelectAll();
            dtEmployee = ProjectAssignment_EmployeeData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtProjectAssignment = ProjectAssignmentData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowProjectAssignment in dtProjectAssignment.AsEnumerable()
                        join rowProject in dtProject.AsEnumerable() on rowProjectAssignment.Field<Int32>("ProjectID") equals rowProject.Field<Int32>("ProjectID")
                        join rowEmployee in dtEmployee.AsEnumerable() on rowProjectAssignment.Field<Int32>("EmployeeID") equals rowEmployee.Field<Int32>("EmployeeID")
                        select new ProjectAssignment() {
                            ProjectAssignmentID = rowProjectAssignment.Field<Int32>("ProjectAssignmentID")
                           ,
                            Project = new Project() 
                            {
                                   ProjectID = rowProject.Field<Int32>("ProjectID")
                                  ,ProjectName = rowProject.Field<String>("ProjectName")
                            }
                           ,
                            Employee = new Employee() 
                            {
                                   EmployeeID = rowEmployee.Field<Int32>("EmployeeID")
                                  ,FirstName = rowEmployee.Field<String>("FirstName")
                            }
                           ,AssignmentDate = rowProjectAssignment.Field<DateTime>("AssignmentDate")
                           ,Remarks = rowProjectAssignment.Field<String>("Remarks")
                           ,AddUserID = rowProjectAssignment.Field<Int32>("AddUserID")
                           ,AddDate = rowProjectAssignment.Field<DateTime>("AddDate")
                           ,ArchiveUserID = rowProjectAssignment.Field<Int32?>("ArchiveUserID")
                           ,ArchiveDate = rowProjectAssignment.Field<DateTime?>("ArchiveDate")
                        };

            switch (sortOrder)
            {
                case "ProjectAssignmentID_desc":
                    Query = Query.OrderByDescending(s => s.ProjectAssignmentID);
                    break;
                case "ProjectAssignmentID_asc":
                    Query = Query.OrderBy(s => s.ProjectAssignmentID);
                    break;
                case "ProjectID_desc":
                    Query = Query.OrderByDescending(s => s.Project.ProjectName);
                    break;
                case "ProjectID_asc":
                    Query = Query.OrderBy(s => s.Project.ProjectName);
                    break;
                case "EmployeeID_desc":
                    Query = Query.OrderByDescending(s => s.Employee.FirstName);
                    break;
                case "EmployeeID_asc":
                    Query = Query.OrderBy(s => s.Employee.FirstName);
                    break;
                case "AssignmentDate_desc":
                    Query = Query.OrderByDescending(s => s.AssignmentDate);
                    break;
                case "AssignmentDate_asc":
                    Query = Query.OrderBy(s => s.AssignmentDate);
                    break;
                case "Remarks_desc":
                    Query = Query.OrderByDescending(s => s.Remarks);
                    break;
                case "Remarks_asc":
                    Query = Query.OrderBy(s => s.Remarks);
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
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.ProjectAssignmentID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Project Assignment I D", typeof(string));
                dt.Columns.Add("Project I D", typeof(string));
                dt.Columns.Add("Employee I D", typeof(string));
                dt.Columns.Add("Assignment Date", typeof(string));
                dt.Columns.Add("Remarks", typeof(string));
                dt.Columns.Add("Add User I D", typeof(string));
                dt.Columns.Add("Add Date", typeof(string));
                dt.Columns.Add("Archive User I D", typeof(string));
                dt.Columns.Add("Archive Date", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.ProjectAssignmentID
                       ,item.Project.ProjectName
                       ,item.Employee.FirstName
                       ,item.AssignmentDate
                       ,item.Remarks
                       ,item.AddUserID
                       ,item.AddDate
                       ,item.ArchiveUserID
                       ,item.ArchiveDate
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

        // GET: /ProjectAssignment/Details/<id>
        public ActionResult Details(
                                      Int32? ProjectAssignmentID
                                   )
        {
            if (
                    ProjectAssignmentID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtProject = ProjectAssignment_ProjectData.SelectAll();
            dtEmployee = ProjectAssignment_EmployeeData.SelectAll();

            ProjectAssignment ProjectAssignment = new ProjectAssignment();
            ProjectAssignment.ProjectAssignmentID = System.Convert.ToInt32(ProjectAssignmentID);
            ProjectAssignment = ProjectAssignmentData.Select_Record(ProjectAssignment);
            ProjectAssignment.Project = new Project()
            {
                ProjectID = (Int32)ProjectAssignment.ProjectID
               ,ProjectName = (from DataRow rowProject in dtProject.Rows
                      where ProjectAssignment.ProjectID == (int)rowProject["ProjectID"]
                      select (String)rowProject["ProjectName"]).FirstOrDefault()
            };
            ProjectAssignment.Employee = new Employee()
            {
                EmployeeID = (Int32)ProjectAssignment.EmployeeID
               ,FirstName = (from DataRow rowEmployee in dtEmployee.Rows
                      where ProjectAssignment.EmployeeID == (int)rowEmployee["EmployeeID"]
                      select (String)rowEmployee["FirstName"]).FirstOrDefault()
            };

            if (ProjectAssignment == null)
            {
                return HttpNotFound();
            }
            return View(ProjectAssignment);
        }

        // GET: /ProjectAssignment/Create
        public ActionResult Create()
        {
        // ComboBox
            ViewData["ProjectID"] = new SelectList(ProjectAssignment_ProjectData.List(), "ProjectID", "ProjectName");
            ViewData["EmployeeID"] = new SelectList(ProjectAssignment_EmployeeData.List(), "EmployeeID", "FirstName");

            return View();
        }

        // POST: /ProjectAssignment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "ProjectID"
				   + "," + "EmployeeID"
				   + "," + "AssignmentDate"
				   + "," + "Remarks"
				   + "," + "AddUserID"
				   + "," + "AddDate"
				   + "," + "ArchiveUserID"
				   + "," + "ArchiveDate"
				  )] ProjectAssignment ProjectAssignment)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = ProjectAssignmentData.Add(ProjectAssignment);
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
            ViewData["ProjectID"] = new SelectList(ProjectAssignment_ProjectData.List(), "ProjectID", "ProjectName", ProjectAssignment.ProjectID);
            ViewData["EmployeeID"] = new SelectList(ProjectAssignment_EmployeeData.List(), "EmployeeID", "FirstName", ProjectAssignment.EmployeeID);

            return View(ProjectAssignment);
        }

        // GET: /ProjectAssignment/Edit/<id>
        public ActionResult Edit(
                                   Int32? ProjectAssignmentID
                                )
        {
            if (
                    ProjectAssignmentID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProjectAssignment ProjectAssignment = new ProjectAssignment();
            ProjectAssignment.ProjectAssignmentID = System.Convert.ToInt32(ProjectAssignmentID);
            ProjectAssignment = ProjectAssignmentData.Select_Record(ProjectAssignment);

            if (ProjectAssignment == null)
            {
                return HttpNotFound();
            }
        // ComboBox
            ViewData["ProjectID"] = new SelectList(ProjectAssignment_ProjectData.List(), "ProjectID", "ProjectName", ProjectAssignment.ProjectID);
            ViewData["EmployeeID"] = new SelectList(ProjectAssignment_EmployeeData.List(), "EmployeeID", "FirstName", ProjectAssignment.EmployeeID);

            return View(ProjectAssignment);
        }

        // POST: /ProjectAssignment/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectAssignment ProjectAssignment)
        {

            ProjectAssignment oProjectAssignment = new ProjectAssignment();
            oProjectAssignment.ProjectAssignmentID = System.Convert.ToInt32(ProjectAssignment.ProjectAssignmentID);
            oProjectAssignment = ProjectAssignmentData.Select_Record(ProjectAssignment);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = ProjectAssignmentData.Update(oProjectAssignment, ProjectAssignment);
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
            ViewData["ProjectID"] = new SelectList(ProjectAssignment_ProjectData.List(), "ProjectID", "ProjectName", ProjectAssignment.ProjectID);
            ViewData["EmployeeID"] = new SelectList(ProjectAssignment_EmployeeData.List(), "EmployeeID", "FirstName", ProjectAssignment.EmployeeID);

            return View(ProjectAssignment);
        }

        // GET: /ProjectAssignment/Delete/<id>
        public ActionResult Delete(
                                     Int32? ProjectAssignmentID
                                  )
        {
            if (
                    ProjectAssignmentID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtProject = ProjectAssignment_ProjectData.SelectAll();
            dtEmployee = ProjectAssignment_EmployeeData.SelectAll();

            ProjectAssignment ProjectAssignment = new ProjectAssignment();
            ProjectAssignment.ProjectAssignmentID = System.Convert.ToInt32(ProjectAssignmentID);
            ProjectAssignment = ProjectAssignmentData.Select_Record(ProjectAssignment);
            ProjectAssignment.Project = new Project()
            {
                ProjectID = (Int32)ProjectAssignment.ProjectID
               ,ProjectName = (from DataRow rowProject in dtProject.Rows
                      where ProjectAssignment.ProjectID == (int)rowProject["ProjectID"]
                      select (String)rowProject["ProjectName"]).FirstOrDefault()
            };
            ProjectAssignment.Employee = new Employee()
            {
                EmployeeID = (Int32)ProjectAssignment.EmployeeID
               ,FirstName = (from DataRow rowEmployee in dtEmployee.Rows
                      where ProjectAssignment.EmployeeID == (int)rowEmployee["EmployeeID"]
                      select (String)rowEmployee["FirstName"]).FirstOrDefault()
            };

            if (ProjectAssignment == null)
            {
                return HttpNotFound();
            }
            return View(ProjectAssignment);
        }

        // POST: /ProjectAssignment/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? ProjectAssignmentID
                                            )
        {

            ProjectAssignment ProjectAssignment = new ProjectAssignment();
            ProjectAssignment.ProjectAssignmentID = System.Convert.ToInt32(ProjectAssignmentID);
            ProjectAssignment = ProjectAssignmentData.Select_Record(ProjectAssignment);

            bool bSucess = false;
            bSucess = ProjectAssignmentData.Delete(ProjectAssignment);
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
            SelectListItem Item1 = new SelectListItem { Text = "Project Assignment I D", Value = "Project Assignment I D" };
            SelectListItem Item2 = new SelectListItem { Text = "Project I D", Value = "Project I D" };
            SelectListItem Item3 = new SelectListItem { Text = "Employee I D", Value = "Employee I D" };
            SelectListItem Item4 = new SelectListItem { Text = "Assignment Date", Value = "Assignment Date" };
            SelectListItem Item5 = new SelectListItem { Text = "Remarks", Value = "Remarks" };
            SelectListItem Item6 = new SelectListItem { Text = "Add User I D", Value = "Add User I D" };
            SelectListItem Item7 = new SelectListItem { Text = "Add Date", Value = "Add Date" };
            SelectListItem Item8 = new SelectListItem { Text = "Archive User I D", Value = "Archive User I D" };
            SelectListItem Item9 = new SelectListItem { Text = "Archive Date", Value = "Archive Date" };

                 if (select == "Project Assignment I D") { Item1.Selected = true; }
            else if (select == "Project I D") { Item2.Selected = true; }
            else if (select == "Employee I D") { Item3.Selected = true; }
            else if (select == "Assignment Date") { Item4.Selected = true; }
            else if (select == "Remarks") { Item5.Selected = true; }
            else if (select == "Add User I D") { Item6.Selected = true; }
            else if (select == "Add Date") { Item7.Selected = true; }
            else if (select == "Archive User I D") { Item8.Selected = true; }
            else if (select == "Archive Date") { Item9.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);
            list.Add(Item5);
            list.Add(Item6);
            list.Add(Item7);
            list.Add(Item8);
            list.Add(Item9);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Project Assignment", "Many");
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
 
