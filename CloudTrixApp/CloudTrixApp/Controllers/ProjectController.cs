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
    public class ProjectController : Controller
    {

        DataTable dtProject = new DataTable();
        DataTable dtProjectStatus = new DataTable();
        DataTable dtClient = new DataTable();
        DataTable dtArchitect = new DataTable();
        DataTable dtCompany = new DataTable();

        // GET: /Project/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Project I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["ProjectIDSortParm"] = sortOrder == "ProjectID_asc" ? "ProjectID_desc" : "ProjectID_asc";
            ViewData["ProjectNameSortParm"] = sortOrder == "ProjectName_asc" ? "ProjectName_desc" : "ProjectName_asc";
            ViewData["BillingNameSortParm"] = sortOrder == "BillingName_asc" ? "BillingName_desc" : "BillingName_asc";
            ViewData["DescriptionSortParm"] = sortOrder == "Description_asc" ? "Description_desc" : "Description_asc";
            ViewData["LocationSortParm"] = sortOrder == "Location_asc" ? "Location_desc" : "Location_asc";
            ViewData["StartDateSortParm"] = sortOrder == "StartDate_asc" ? "StartDate_desc" : "StartDate_asc";
            ViewData["EndDateSortParm"] = sortOrder == "EndDate_asc" ? "EndDate_desc" : "EndDate_asc";
            ViewData["ProjectStatusIDSortParm"] = sortOrder == "ProjectStatusID_asc" ? "ProjectStatusID_desc" : "ProjectStatusID_asc";
            ViewData["ClientIDSortParm"] = sortOrder == "ClientID_asc" ? "ClientID_desc" : "ClientID_asc";
            ViewData["ArchitectIDSortParm"] = sortOrder == "ArchitectID_asc" ? "ArchitectID_desc" : "ArchitectID_asc";
            ViewData["CompanyIDSortParm"] = sortOrder == "CompanyID_asc" ? "CompanyID_desc" : "CompanyID_asc";
            ViewData["AddUserIDSortParm"] = sortOrder == "AddUserID_asc" ? "AddUserID_desc" : "AddUserID_asc";
            ViewData["AddDateSortParm"] = sortOrder == "AddDate_asc" ? "AddDate_desc" : "AddDate_asc";
            ViewData["ArchiveUserIDSortParm"] = sortOrder == "ArchiveUserID_asc" ? "ArchiveUserID_desc" : "ArchiveUserID_asc";
            ViewData["ArchiveDateSortParm"] = sortOrder == "ArchiveDate_asc" ? "ArchiveDate_desc" : "ArchiveDate_asc";

            dtProject = ProjectData.SelectAll();
            dtProjectStatus = Project_ProjectStatusData.SelectAll();
            dtClient = Project_ClientData.SelectAll();
            dtArchitect = Project_ArchitectData.SelectAll();
            dtCompany = Project_CompanyData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtProject = ProjectData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowProject in dtProject.AsEnumerable()
                        join rowProjectStatus in dtProjectStatus.AsEnumerable() on rowProject.Field<Int32>("ProjectStatusID") equals rowProjectStatus.Field<Int32>("ProjectStatusID")
                        join rowClient in dtClient.AsEnumerable() on rowProject.Field<Int32>("ClientID") equals rowClient.Field<Int32>("ClientID")
                        join rowArchitect in dtArchitect.AsEnumerable() on rowProject.Field<Int32>("ArchitectID") equals rowArchitect.Field<Int32>("ArchitectID")
                        join rowCompany in dtCompany.AsEnumerable() on rowProject.Field<Int32?>("CompanyID") equals rowCompany.Field<Int32>("CompanyID")
                        select new Project() {
                            ProjectID = rowProject.Field<Int32>("ProjectID")
                           ,ProjectName = rowProject.Field<String>("ProjectName")
                           ,BillingName = rowProject.Field<String>("BillingName")
                           ,Description = rowProject.Field<String>("Description")
                           ,Location = rowProject.Field<String>("Location")
                           ,StartDate = rowProject.Field<DateTime>("StartDate")
                           ,EndDate = rowProject.Field<DateTime?>("EndDate")
                           ,
                            ProjectStatus = new ProjectStatus() 
                            {
                                   ProjectStatusID = rowProjectStatus.Field<Int32>("ProjectStatusID")
                                  ,ProjectStatusName = rowProjectStatus.Field<String>("ProjectStatusName")
                            }
                           ,
                            Client = new Client() 
                            {
                                   ClientID = rowClient.Field<Int32>("ClientID")
                                  ,ClientName = rowClient.Field<String>("ClientName")
                            }
                           ,
                            Architect = new Architect() 
                            {
                                   ArchitectID = rowArchitect.Field<Int32>("ArchitectID")
                                  ,ArchitectName = rowArchitect.Field<String>("ArchitectName")
                            }
                           ,
                            Company = new Company() 
                            {
                                   CompanyID = rowCompany.Field<Int32>("CompanyID")
                                  ,CompanyName = rowCompany.Field<String>("CompanyName")
                            }
                           ,AddUserID = rowProject.Field<Int32>("AddUserID")
                           ,AddDate = rowProject.Field<DateTime>("AddDate")
                           ,ArchiveUserID = rowProject.Field<Int32?>("ArchiveUserID")
                           ,ArchiveDate = rowProject.Field<DateTime?>("ArchiveDate")
                        };

            switch (sortOrder)
            {
                case "ProjectID_desc":
                    Query = Query.OrderByDescending(s => s.ProjectID);
                    break;
                case "ProjectID_asc":
                    Query = Query.OrderBy(s => s.ProjectID);
                    break;
                case "ProjectName_desc":
                    Query = Query.OrderByDescending(s => s.ProjectName);
                    break;
                case "ProjectName_asc":
                    Query = Query.OrderBy(s => s.ProjectName);
                    break;
                case "BillingName_desc":
                    Query = Query.OrderByDescending(s => s.BillingName);
                    break;
                case "BillingName_asc":
                    Query = Query.OrderBy(s => s.BillingName);
                    break;
                case "Description_desc":
                    Query = Query.OrderByDescending(s => s.Description);
                    break;
                case "Description_asc":
                    Query = Query.OrderBy(s => s.Description);
                    break;
                case "Location_desc":
                    Query = Query.OrderByDescending(s => s.Location);
                    break;
                case "Location_asc":
                    Query = Query.OrderBy(s => s.Location);
                    break;
                case "StartDate_desc":
                    Query = Query.OrderByDescending(s => s.StartDate);
                    break;
                case "StartDate_asc":
                    Query = Query.OrderBy(s => s.StartDate);
                    break;
                case "EndDate_desc":
                    Query = Query.OrderByDescending(s => s.EndDate);
                    break;
                case "EndDate_asc":
                    Query = Query.OrderBy(s => s.EndDate);
                    break;
                case "ProjectStatusID_desc":
                    Query = Query.OrderByDescending(s => s.ProjectStatus.ProjectStatusName);
                    break;
                case "ProjectStatusID_asc":
                    Query = Query.OrderBy(s => s.ProjectStatus.ProjectStatusName);
                    break;
                case "ClientID_desc":
                    Query = Query.OrderByDescending(s => s.Client.ClientName);
                    break;
                case "ClientID_asc":
                    Query = Query.OrderBy(s => s.Client.ClientName);
                    break;
                case "ArchitectID_desc":
                    Query = Query.OrderByDescending(s => s.Architect.ArchitectName);
                    break;
                case "ArchitectID_asc":
                    Query = Query.OrderBy(s => s.Architect.ArchitectName);
                    break;
                case "CompanyID_desc":
                    Query = Query.OrderByDescending(s => s.Company.CompanyName);
                    break;
                case "CompanyID_asc":
                    Query = Query.OrderBy(s => s.Company.CompanyName);
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
                    Query = Query.OrderBy(s => s.ProjectID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Project I D", typeof(string));
                dt.Columns.Add("Project Name", typeof(string));
                dt.Columns.Add("Billing Name", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Location", typeof(string));
                dt.Columns.Add("Start Date", typeof(string));
                dt.Columns.Add("End Date", typeof(string));
                dt.Columns.Add("Project Status I D", typeof(string));
                dt.Columns.Add("Client I D", typeof(string));
                dt.Columns.Add("Architect I D", typeof(string));
                dt.Columns.Add("Company I D", typeof(string));
                dt.Columns.Add("Add User I D", typeof(string));
                dt.Columns.Add("Add Date", typeof(string));
                dt.Columns.Add("Archive User I D", typeof(string));
                dt.Columns.Add("Archive Date", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.ProjectID
                       ,item.ProjectName
                       ,item.BillingName
                       ,item.Description
                       ,item.Location
                       ,item.StartDate
                       ,item.EndDate
                       ,item.ProjectStatus.ProjectStatusName
                       ,item.Client.ClientName
                       ,item.Architect.ArchitectName
                       ,item.Company.CompanyName
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

        // GET: /Project/Details/<id>
        public ActionResult Details(
                                      Int32? ProjectID
                                   )
        {
            if (
                    ProjectID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtProjectStatus = Project_ProjectStatusData.SelectAll();
            dtClient = Project_ClientData.SelectAll();
            dtArchitect = Project_ArchitectData.SelectAll();
            dtCompany = Project_CompanyData.SelectAll();

            Project Project = new Project();
            Project.ProjectID = System.Convert.ToInt32(ProjectID);
            Project = ProjectData.Select_Record(Project);
            Project.ProjectStatus = new ProjectStatus()
            {
                ProjectStatusID = (Int32)Project.ProjectStatusID
               ,ProjectStatusName = (from DataRow rowProjectStatus in dtProjectStatus.Rows
                      where Project.ProjectStatusID == (int)rowProjectStatus["ProjectStatusID"]
                      select (String)rowProjectStatus["ProjectStatusName"]).FirstOrDefault()
            };
            Project.Client = new Client()
            {
                ClientID = (Int32)Project.ClientID
               ,ClientName = (from DataRow rowClient in dtClient.Rows
                      where Project.ClientID == (int)rowClient["ClientID"]
                      select (String)rowClient["ClientName"]).FirstOrDefault()
            };
            Project.Architect = new Architect()
            {
                ArchitectID = (Int32)Project.ArchitectID
               ,ArchitectName = (from DataRow rowArchitect in dtArchitect.Rows
                      where Project.ArchitectID == (int)rowArchitect["ArchitectID"]
                      select (String)rowArchitect["ArchitectName"]).FirstOrDefault()
            };
            Project.Company = new Company()
            {
                CompanyID = (Int32)Project.CompanyID
               ,CompanyName = (from DataRow rowCompany in dtCompany.Rows
                      where Project.CompanyID == (int)rowCompany["CompanyID"]
                      select (String)rowCompany["CompanyName"]).FirstOrDefault()
            };

            if (Project == null)
            {
                return HttpNotFound();
            }
            return View(Project);
        }

        // GET: /Project/Create
        public ActionResult Create()
        {
        // ComboBox
            ViewData["ProjectStatusID"] = new SelectList(Project_ProjectStatusData.List(), "ProjectStatusID", "ProjectStatusName");
            ViewData["ClientID"] = new SelectList(Project_ClientData.List(), "ClientID", "ClientName");
            ViewData["ArchitectID"] = new SelectList(Project_ArchitectData.List(), "ArchitectID", "ArchitectName");
            ViewData["CompanyID"] = new SelectList(Project_CompanyData.List(), "CompanyID", "CompanyName");

            return View();
        }

        // POST: /Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "ProjectName"
				   + "," + "BillingName"
				   + "," + "Description"
				   + "," + "Location"
				   + "," + "StartDate"
				   + "," + "EndDate"
				   + "," + "ProjectStatusID"
				   + "," + "ClientID"
				   + "," + "ArchitectID"
				   + "," + "CompanyID"
				   + "," + "AddUserID"
				   + "," + "AddDate"
				   + "," + "ArchiveUserID"
				   + "," + "ArchiveDate"
				  )] Project Project)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = ProjectData.Add(Project);
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
            ViewData["ProjectStatusID"] = new SelectList(Project_ProjectStatusData.List(), "ProjectStatusID", "ProjectStatusName", Project.ProjectStatusID);
            ViewData["ClientID"] = new SelectList(Project_ClientData.List(), "ClientID", "ClientName", Project.ClientID);
            ViewData["ArchitectID"] = new SelectList(Project_ArchitectData.List(), "ArchitectID", "ArchitectName", Project.ArchitectID);
            ViewData["CompanyID"] = new SelectList(Project_CompanyData.List(), "CompanyID", "CompanyName", Project.CompanyID);

            return View(Project);
        }

        // GET: /Project/Edit/<id>
        public ActionResult Edit(
                                   Int32? ProjectID
                                )
        {
            if (
                    ProjectID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project Project = new Project();
            Project.ProjectID = System.Convert.ToInt32(ProjectID);
            Project = ProjectData.Select_Record(Project);

            if (Project == null)
            {
                return HttpNotFound();
            }
        // ComboBox
            ViewData["ProjectStatusID"] = new SelectList(Project_ProjectStatusData.List(), "ProjectStatusID", "ProjectStatusName", Project.ProjectStatusID);
            ViewData["ClientID"] = new SelectList(Project_ClientData.List(), "ClientID", "ClientName", Project.ClientID);
            ViewData["ArchitectID"] = new SelectList(Project_ArchitectData.List(), "ArchitectID", "ArchitectName", Project.ArchitectID);
            ViewData["CompanyID"] = new SelectList(Project_CompanyData.List(), "CompanyID", "CompanyName", Project.CompanyID);

            return View(Project);
        }

        // POST: /Project/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project Project)
        {

            Project oProject = new Project();
            oProject.ProjectID = System.Convert.ToInt32(Project.ProjectID);
            oProject = ProjectData.Select_Record(Project);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = ProjectData.Update(oProject, Project);
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
            ViewData["ProjectStatusID"] = new SelectList(Project_ProjectStatusData.List(), "ProjectStatusID", "ProjectStatusName", Project.ProjectStatusID);
            ViewData["ClientID"] = new SelectList(Project_ClientData.List(), "ClientID", "ClientName", Project.ClientID);
            ViewData["ArchitectID"] = new SelectList(Project_ArchitectData.List(), "ArchitectID", "ArchitectName", Project.ArchitectID);
            ViewData["CompanyID"] = new SelectList(Project_CompanyData.List(), "CompanyID", "CompanyName", Project.CompanyID);

            return View(Project);
        }

        // GET: /Project/Delete/<id>
        public ActionResult Delete(
                                     Int32? ProjectID
                                  )
        {
            if (
                    ProjectID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtProjectStatus = Project_ProjectStatusData.SelectAll();
            dtClient = Project_ClientData.SelectAll();
            dtArchitect = Project_ArchitectData.SelectAll();
            dtCompany = Project_CompanyData.SelectAll();

            Project Project = new Project();
            Project.ProjectID = System.Convert.ToInt32(ProjectID);
            Project = ProjectData.Select_Record(Project);
            Project.ProjectStatus = new ProjectStatus()
            {
                ProjectStatusID = (Int32)Project.ProjectStatusID
               ,ProjectStatusName = (from DataRow rowProjectStatus in dtProjectStatus.Rows
                      where Project.ProjectStatusID == (int)rowProjectStatus["ProjectStatusID"]
                      select (String)rowProjectStatus["ProjectStatusName"]).FirstOrDefault()
            };
            Project.Client = new Client()
            {
                ClientID = (Int32)Project.ClientID
               ,ClientName = (from DataRow rowClient in dtClient.Rows
                      where Project.ClientID == (int)rowClient["ClientID"]
                      select (String)rowClient["ClientName"]).FirstOrDefault()
            };
            Project.Architect = new Architect()
            {
                ArchitectID = (Int32)Project.ArchitectID
               ,ArchitectName = (from DataRow rowArchitect in dtArchitect.Rows
                      where Project.ArchitectID == (int)rowArchitect["ArchitectID"]
                      select (String)rowArchitect["ArchitectName"]).FirstOrDefault()
            };
            Project.Company = new Company()
            {
                CompanyID = (Int32)Project.CompanyID
               ,CompanyName = (from DataRow rowCompany in dtCompany.Rows
                      where Project.CompanyID == (int)rowCompany["CompanyID"]
                      select (String)rowCompany["CompanyName"]).FirstOrDefault()
            };

            if (Project == null)
            {
                return HttpNotFound();
            }
            return View(Project);
        }

        // POST: /Project/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? ProjectID
                                            )
        {

            Project Project = new Project();
            Project.ProjectID = System.Convert.ToInt32(ProjectID);
            Project = ProjectData.Select_Record(Project);

            bool bSucess = false;
            bSucess = ProjectData.Delete(Project);
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
            SelectListItem Item1 = new SelectListItem { Text = "Project I D", Value = "Project I D" };
            SelectListItem Item2 = new SelectListItem { Text = "Project Name", Value = "Project Name" };
            SelectListItem Item3 = new SelectListItem { Text = "Billing Name", Value = "Billing Name" };
            SelectListItem Item4 = new SelectListItem { Text = "Description", Value = "Description" };
            SelectListItem Item5 = new SelectListItem { Text = "Location", Value = "Location" };
            SelectListItem Item6 = new SelectListItem { Text = "Start Date", Value = "Start Date" };
            SelectListItem Item7 = new SelectListItem { Text = "End Date", Value = "End Date" };
            SelectListItem Item8 = new SelectListItem { Text = "Project Status I D", Value = "Project Status I D" };
            SelectListItem Item9 = new SelectListItem { Text = "Client I D", Value = "Client I D" };
            SelectListItem Item10 = new SelectListItem { Text = "Architect I D", Value = "Architect I D" };
            SelectListItem Item11 = new SelectListItem { Text = "Company I D", Value = "Company I D" };
            SelectListItem Item12 = new SelectListItem { Text = "Add User I D", Value = "Add User I D" };
            SelectListItem Item13 = new SelectListItem { Text = "Add Date", Value = "Add Date" };
            SelectListItem Item14 = new SelectListItem { Text = "Archive User I D", Value = "Archive User I D" };
            SelectListItem Item15 = new SelectListItem { Text = "Archive Date", Value = "Archive Date" };

                 if (select == "Project I D") { Item1.Selected = true; }
            else if (select == "Project Name") { Item2.Selected = true; }
            else if (select == "Billing Name") { Item3.Selected = true; }
            else if (select == "Description") { Item4.Selected = true; }
            else if (select == "Location") { Item5.Selected = true; }
            else if (select == "Start Date") { Item6.Selected = true; }
            else if (select == "End Date") { Item7.Selected = true; }
            else if (select == "Project Status I D") { Item8.Selected = true; }
            else if (select == "Client I D") { Item9.Selected = true; }
            else if (select == "Architect I D") { Item10.Selected = true; }
            else if (select == "Company I D") { Item11.Selected = true; }
            else if (select == "Add User I D") { Item12.Selected = true; }
            else if (select == "Add Date") { Item13.Selected = true; }
            else if (select == "Archive User I D") { Item14.Selected = true; }
            else if (select == "Archive Date") { Item15.Selected = true; }

            list.Add(Item1);
            list.Add(Item2);
            list.Add(Item3);
            list.Add(Item4);
            list.Add(Item5);
            list.Add(Item6);
            list.Add(Item7);
            list.Add(Item8);
            list.Add(Item9);
            list.Add(Item10);
            list.Add(Item11);
            list.Add(Item12);
            list.Add(Item13);
            list.Add(Item14);
            list.Add(Item15);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Project", "Many");
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
 
