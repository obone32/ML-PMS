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
    public class BackupLogController : Controller
    {

        DataTable dtBackupLog = new DataTable();

        // GET: /BackupLog/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Backup Log I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["BackupLogIDSortParm"] = sortOrder == "BackupLogID_asc" ? "BackupLogID_desc" : "BackupLogID_asc";
            ViewData["BackupDateSortParm"] = sortOrder == "BackupDate_asc" ? "BackupDate_desc" : "BackupDate_asc";
            ViewData["FilePathSortParm"] = sortOrder == "FilePath_asc" ? "FilePath_desc" : "FilePath_asc";
            ViewData["RemarksSortParm"] = sortOrder == "Remarks_asc" ? "Remarks_desc" : "Remarks_asc";

            dtBackupLog = BackupLogData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtBackupLog = BackupLogData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowBackupLog in dtBackupLog.AsEnumerable()
                        select new BackupLog() {
                            BackupLogID = rowBackupLog.Field<Int32>("BackupLogID")
                           ,BackupDate = rowBackupLog.Field<DateTime>("BackupDate")
                           ,FilePath = rowBackupLog.Field<String>("FilePath")
                           ,Remarks = rowBackupLog.Field<String>("Remarks")
                        };

            switch (sortOrder)
            {
                case "BackupLogID_desc":
                    Query = Query.OrderByDescending(s => s.BackupLogID);
                    break;
                case "BackupLogID_asc":
                    Query = Query.OrderBy(s => s.BackupLogID);
                    break;
                case "BackupDate_desc":
                    Query = Query.OrderByDescending(s => s.BackupDate);
                    break;
                case "BackupDate_asc":
                    Query = Query.OrderBy(s => s.BackupDate);
                    break;
                case "FilePath_desc":
                    Query = Query.OrderByDescending(s => s.FilePath);
                    break;
                case "FilePath_asc":
                    Query = Query.OrderBy(s => s.FilePath);
                    break;
                case "Remarks_desc":
                    Query = Query.OrderByDescending(s => s.Remarks);
                    break;
                case "Remarks_asc":
                    Query = Query.OrderBy(s => s.Remarks);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.BackupLogID);
                    break;
            }

            if (command == "Export") {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Backup Log I D", typeof(string));
                dt.Columns.Add("Backup Date", typeof(string));
                dt.Columns.Add("File Path", typeof(string));
                dt.Columns.Add("Remarks", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.BackupLogID
                       ,item.BackupDate
                       ,item.FilePath
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

        // GET: /BackupLog/Details/<id>
        public ActionResult Details(
                                      Int32? BackupLogID
                                   )
        {
            if (
                    BackupLogID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            BackupLog BackupLog = new BackupLog();
            BackupLog.BackupLogID = System.Convert.ToInt32(BackupLogID);
            BackupLog = BackupLogData.Select_Record(BackupLog);

            if (BackupLog == null)
            {
                return HttpNotFound();
            }
            return View(BackupLog);
        }

        // GET: /BackupLog/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: /BackupLog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
				           "BackupDate"
				   + "," + "FilePath"
				   + "," + "Remarks"
				  )] BackupLog BackupLog)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = BackupLogData.Add(BackupLog);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }

            return View(BackupLog);
        }

        // GET: /BackupLog/Edit/<id>
        public ActionResult Edit(
                                   Int32? BackupLogID
                                )
        {
            if (
                    BackupLogID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BackupLog BackupLog = new BackupLog();
            BackupLog.BackupLogID = System.Convert.ToInt32(BackupLogID);
            BackupLog = BackupLogData.Select_Record(BackupLog);

            if (BackupLog == null)
            {
                return HttpNotFound();
            }

            return View(BackupLog);
        }

        // POST: /BackupLog/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BackupLog BackupLog)
        {

            BackupLog oBackupLog = new BackupLog();
            oBackupLog.BackupLogID = System.Convert.ToInt32(BackupLog.BackupLogID);
            oBackupLog = BackupLogData.Select_Record(BackupLog);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = BackupLogData.Update(oBackupLog, BackupLog);
                if (bSucess == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }

            return View(BackupLog);
        }

        // GET: /BackupLog/Delete/<id>
        public ActionResult Delete(
                                     Int32? BackupLogID
                                  )
        {
            if (
                    BackupLogID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            BackupLog BackupLog = new BackupLog();
            BackupLog.BackupLogID = System.Convert.ToInt32(BackupLogID);
            BackupLog = BackupLogData.Select_Record(BackupLog);

            if (BackupLog == null)
            {
                return HttpNotFound();
            }
            return View(BackupLog);
        }

        // POST: /BackupLog/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? BackupLogID
                                            )
        {

            BackupLog BackupLog = new BackupLog();
            BackupLog.BackupLogID = System.Convert.ToInt32(BackupLogID);
            BackupLog = BackupLogData.Select_Record(BackupLog);

            bool bSucess = false;
            bSucess = BackupLogData.Delete(BackupLog);
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
            SelectListItem Item1 = new SelectListItem { Text = "Backup Log I D", Value = "Backup Log I D" };
            SelectListItem Item2 = new SelectListItem { Text = "Backup Date", Value = "Backup Date" };
            SelectListItem Item3 = new SelectListItem { Text = "File Path", Value = "File Path" };
            SelectListItem Item4 = new SelectListItem { Text = "Remarks", Value = "Remarks" };

                 if (select == "Backup Log I D") { Item1.Selected = true; }
            else if (select == "Backup Date") { Item2.Selected = true; }
            else if (select == "File Path") { Item3.Selected = true; }
            else if (select == "Remarks") { Item4.Selected = true; }

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
                PDFform pdfForm = new PDFform(dt, "Dbo. Backup Log", "Many");
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
 
