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
    public class ArchitectAssociateController : Controller
    {

        DataTable dtArchitectAssociate = new DataTable();
        DataTable dtArchitect = new DataTable();

        // GET: /ArchitectAssociate/
        public ActionResult Index(string sortOrder,
                                  String SearchField,
                                  String SearchCondition,
                                  String SearchText,
                                  String Export,
                                  int? PageSize,
                                  int? page,
                                  string command)
        {

            if (command == "Show All")
            {
                SearchField = null;
                SearchCondition = null;
                SearchText = null;
                Session["SearchField"] = null;
                Session["SearchCondition"] = null;
                Session["SearchText"] = null;
            }
            else if (command == "Add New Record") { return RedirectToAction("Create"); }
            else if (command == "Export") { Session["Export"] = Export; }
            else if (command == "Search" | command == "Page Size")
            {
                if (!string.IsNullOrEmpty(SearchText))
                {
                    Session["SearchField"] = SearchField;
                    Session["SearchCondition"] = SearchCondition;
                    Session["SearchText"] = SearchText;
                }
            }
            if (command == "Page Size") { Session["PageSize"] = PageSize; }

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Architect Associate I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["ArchitectAssociateIDSortParm"] = sortOrder == "ArchitectAssociateID_asc" ? "ArchitectAssociateID_desc" : "ArchitectAssociateID_asc";
            ViewData["ArchitectIDSortParm"] = sortOrder == "ArchitectID_asc" ? "ArchitectID_desc" : "ArchitectID_asc";
            ViewData["AssociateNameSortParm"] = sortOrder == "AssociateName_asc" ? "AssociateName_desc" : "AssociateName_asc";
            ViewData["ContactNoSortParm"] = sortOrder == "ContactNo_asc" ? "ContactNo_desc" : "ContactNo_asc";
            ViewData["EMailSortParm"] = sortOrder == "EMail_asc" ? "EMail_desc" : "EMail_asc";

            dtArchitectAssociate = ArchitectAssociateData.SelectAll();
            dtArchitect = ArchitectAssociate_ArchitectData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtArchitectAssociate = ArchitectAssociateData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowArchitectAssociate in dtArchitectAssociate.AsEnumerable()
                        join rowArchitect in dtArchitect.AsEnumerable() on rowArchitectAssociate.Field<Int32>("ArchitectID") equals rowArchitect.Field<Int32>("ArchitectID")
                        select new ArchitectAssociate()
                        {
                            ArchitectAssociateID = rowArchitectAssociate.Field<Int32>("ArchitectAssociateID")
                           ,
                            Architect = new Architect()
                            {
                                ArchitectID = rowArchitect.Field<Int32>("ArchitectID")
                                  ,
                                ArchitectName = rowArchitect.Field<String>("ArchitectName")
                            }
                           ,
                            AssociateName = rowArchitectAssociate.Field<String>("AssociateName")
                           ,
                            ContactNo = rowArchitectAssociate.Field<String>("ContactNo")
                           ,
                            EMail = rowArchitectAssociate.Field<String>("EMail")
                        };

            switch (sortOrder)
            {
                case "ArchitectAssociateID_desc":
                    Query = Query.OrderByDescending(s => s.ArchitectAssociateID);
                    break;
                case "ArchitectAssociateID_asc":
                    Query = Query.OrderBy(s => s.ArchitectAssociateID);
                    break;
                case "ArchitectID_desc":
                    Query = Query.OrderByDescending(s => s.Architect.ArchitectName);
                    break;
                case "ArchitectID_asc":
                    Query = Query.OrderBy(s => s.Architect.ArchitectName);
                    break;
                case "AssociateName_desc":
                    Query = Query.OrderByDescending(s => s.AssociateName);
                    break;
                case "AssociateName_asc":
                    Query = Query.OrderBy(s => s.AssociateName);
                    break;
                case "ContactNo_desc":
                    Query = Query.OrderByDescending(s => s.ContactNo);
                    break;
                case "ContactNo_asc":
                    Query = Query.OrderBy(s => s.ContactNo);
                    break;
                case "EMail_desc":
                    Query = Query.OrderByDescending(s => s.EMail);
                    break;
                case "EMail_asc":
                    Query = Query.OrderBy(s => s.EMail);
                    break;
                default:  // Name ascending 
                    Query = Query.OrderBy(s => s.ArchitectAssociateID);
                    break;
            }

            if (command == "Export")
            {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Architect Associate I D", typeof(string));
                dt.Columns.Add("Architect I D", typeof(string));
                dt.Columns.Add("Associate Name", typeof(string));
                dt.Columns.Add("Contact No", typeof(string));
                dt.Columns.Add("E Mail", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.ArchitectAssociateID
                       , item.Architect.ArchitectName
                       , item.AssociateName
                       , item.ContactNo
                       , item.EMail
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

        // GET: /ArchitectAssociate/Details/<id>
        public ActionResult Details(
                                      Int32? ArchitectAssociateID
                                   )
        {
            if (
                    ArchitectAssociateID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtArchitect = ArchitectAssociate_ArchitectData.SelectAll();

            ArchitectAssociate ArchitectAssociate = new ArchitectAssociate();
            ArchitectAssociate.ArchitectAssociateID = System.Convert.ToInt32(ArchitectAssociateID);
            ArchitectAssociate = ArchitectAssociateData.Select_Record(ArchitectAssociate);
            ArchitectAssociate.Architect = new Architect()
            {
                ArchitectID = (Int32)ArchitectAssociate.ArchitectID
               ,
                ArchitectName = (from DataRow rowArchitect in dtArchitect.Rows
                                 where ArchitectAssociate.ArchitectID == (int)rowArchitect["ArchitectID"]
                                 select (String)rowArchitect["ArchitectName"]).FirstOrDefault()
            };

            if (ArchitectAssociate == null)
            {
                return HttpNotFound();
            }
            return View(ArchitectAssociate);
        }

        // GET: /ArchitectAssociate/Create
        public ActionResult Create()
        {
            // ComboBox
            ViewData["ArchitectID"] = new SelectList(ArchitectAssociate_ArchitectData.List(), "ArchitectID", "ArchitectName");

            return View();
        }

        // POST: /ArchitectAssociate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include=
                           "ArchitectID"
                   + "," + "AssociateName"
                   + "," + "ContactNo"
                   + "," + "EMail"
                  )] ArchitectAssociate ArchitectAssociate)
        {
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = ArchitectAssociateData.Add(ArchitectAssociate);
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
            ViewData["ArchitectID"] = new SelectList(ArchitectAssociate_ArchitectData.List(), "ArchitectID", "ArchitectName", ArchitectAssociate.ArchitectID);

            return View(ArchitectAssociate);
        }

        // GET: /ArchitectAssociate/Edit/<id>
        public ActionResult Edit(
                                   Int32? ArchitectAssociateID
                                )
        {
            if (
                    ArchitectAssociateID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ArchitectAssociate ArchitectAssociate = new ArchitectAssociate();
            ArchitectAssociate.ArchitectAssociateID = System.Convert.ToInt32(ArchitectAssociateID);
            ArchitectAssociate = ArchitectAssociateData.Select_Record(ArchitectAssociate);

            if (ArchitectAssociate == null)
            {
                return HttpNotFound();
            }
            // ComboBox
            ViewData["ArchitectID"] = new SelectList(ArchitectAssociate_ArchitectData.List(), "ArchitectID", "ArchitectName", ArchitectAssociate.ArchitectID);

            return View(ArchitectAssociate);
        }

        // POST: /ArchitectAssociate/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArchitectAssociate ArchitectAssociate)
        {

            ArchitectAssociate oArchitectAssociate = new ArchitectAssociate();
            oArchitectAssociate.ArchitectAssociateID = System.Convert.ToInt32(ArchitectAssociate.ArchitectAssociateID);
            oArchitectAssociate = ArchitectAssociateData.Select_Record(ArchitectAssociate);

            if (ModelState.IsValid)
            {
                bool bSucess = false;
                bSucess = ArchitectAssociateData.Update(oArchitectAssociate, ArchitectAssociate);
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
            ViewData["ArchitectID"] = new SelectList(ArchitectAssociate_ArchitectData.List(), "ArchitectID", "ArchitectName", ArchitectAssociate.ArchitectID);

            return View(ArchitectAssociate);
        }

        // GET: /ArchitectAssociate/Delete/<id>
        public ActionResult Delete(
                                     Int32? ArchitectAssociateID
                                  )
        {
            if (
                    ArchitectAssociateID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtArchitect = ArchitectAssociate_ArchitectData.SelectAll();

            ArchitectAssociate ArchitectAssociate = new ArchitectAssociate();
            ArchitectAssociate.ArchitectAssociateID = System.Convert.ToInt32(ArchitectAssociateID);
            ArchitectAssociate = ArchitectAssociateData.Select_Record(ArchitectAssociate);
            ArchitectAssociate.Architect = new Architect()
            {
                ArchitectID = (Int32)ArchitectAssociate.ArchitectID
               ,
                ArchitectName = (from DataRow rowArchitect in dtArchitect.Rows
                                 where ArchitectAssociate.ArchitectID == (int)rowArchitect["ArchitectID"]
                                 select (String)rowArchitect["ArchitectName"]).FirstOrDefault()
            };

            if (ArchitectAssociate == null)
            {
                return HttpNotFound();
            }
            return View(ArchitectAssociate);
        }

        // POST: /ArchitectAssociate/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? ArchitectAssociateID
                                            )
        {

            ArchitectAssociate ArchitectAssociate = new ArchitectAssociate();
            ArchitectAssociate.ArchitectAssociateID = System.Convert.ToInt32(ArchitectAssociateID);
            ArchitectAssociate = ArchitectAssociateData.Select_Record(ArchitectAssociate);

            bool bSucess = false;
            bSucess = ArchitectAssociateData.Delete(ArchitectAssociate);
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
            SelectListItem Item1 = new SelectListItem { Text = "Architect Associate I D", Value = "Architect Associate I D" };
            SelectListItem Item2 = new SelectListItem { Text = "Architect I D", Value = "Architect I D" };
            SelectListItem Item3 = new SelectListItem { Text = "Associate Name", Value = "Associate Name" };
            SelectListItem Item4 = new SelectListItem { Text = "Contact No", Value = "Contact No" };
            SelectListItem Item5 = new SelectListItem { Text = "E Mail", Value = "E Mail" };

            if (select == "Architect Associate I D") { Item1.Selected = true; }
            else if (select == "Architect I D") { Item2.Selected = true; }
            else if (select == "Associate Name") { Item3.Selected = true; }
            else if (select == "Contact No") { Item4.Selected = true; }
            else if (select == "E Mail") { Item5.Selected = true; }

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
                PDFform pdfForm = new PDFform(dt, "Dbo. Architect Associate", "Many");
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

