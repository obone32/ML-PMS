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
using System.Text;
using System.Security.Cryptography;

namespace CloudTrixApp.Controllers
{
    public class EmployeeController : Controller
    {

        DataTable dtEmployee = new DataTable();
        DataTable dtCompany = new DataTable();
        DataTable dtUserType = new DataTable();

        // GET: /Employee/
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

            ViewData["SearchFields"] = GetFields((Session["SearchField"] == null ? "Employee I D" : Convert.ToString(Session["SearchField"])));
            ViewData["SearchConditions"] = Library.GetConditions((Session["SearchCondition"] == null ? "Contains" : Convert.ToString(Session["SearchCondition"])));
            ViewData["SearchText"] = Session["SearchText"];
            ViewData["Exports"] = Library.GetExports((Session["Export"] == null ? "Pdf" : Convert.ToString(Session["Export"])));
            ViewData["PageSizes"] = Library.GetPageSizes();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["EmployeeIDSortParm"] = sortOrder == "EmployeeID_asc" ? "EmployeeID_desc" : "EmployeeID_asc";
            ViewData["FirstNameSortParm"] = sortOrder == "FirstName_asc" ? "FirstName_desc" : "FirstName_asc";
            ViewData["LastNameSortParm"] = sortOrder == "LastName_asc" ? "LastName_desc" : "LastName_asc";
            ViewData["DOBSortParm"] = sortOrder == "DOB_asc" ? "DOB_desc" : "DOB_asc";
            ViewData["DOJSortParm"] = sortOrder == "DOJ_asc" ? "DOJ_desc" : "DOJ_asc";
            ViewData["GenderSortParm"] = sortOrder == "Gender_asc" ? "Gender_desc" : "Gender_asc";
            ViewData["EMailSortParm"] = sortOrder == "EMail_asc" ? "EMail_desc" : "EMail_asc";
            ViewData["MobileSortParm"] = sortOrder == "Mobile_asc" ? "Mobile_desc" : "Mobile_asc";
            ViewData["Address1SortParm"] = sortOrder == "Address1_asc" ? "Address1_desc" : "Address1_asc";
            ViewData["Address2SortParm"] = sortOrder == "Address2_asc" ? "Address2_desc" : "Address2_asc";
            ViewData["SalarySortParm"] = sortOrder == "Salary_asc" ? "Salary_desc" : "Salary_asc";
            ViewData["SignatureURLSortParm"] = sortOrder == "SignatureURL_asc" ? "SignatureURL_desc" : "SignatureURL_asc";
            ViewData["UserNameSortParm"] = sortOrder == "UserName_asc" ? "UserName_desc" : "UserName_asc";
            ViewData["PasswordSortParm"] = sortOrder == "Password_asc" ? "Password_desc" : "Password_asc";
            ViewData["CompanyIDSortParm"] = sortOrder == "CompanyID_asc" ? "CompanyID_desc" : "CompanyID_asc";
            ViewData["AddUserIDSortParm"] = sortOrder == "AddUserID_asc" ? "AddUserID_desc" : "AddUserID_asc";
            ViewData["AddDateSortParm"] = sortOrder == "AddDate_asc" ? "AddDate_desc" : "AddDate_asc";
            ViewData["ArchiveUserIDSortParm"] = sortOrder == "ArchiveUserID_asc" ? "ArchiveUserID_desc" : "ArchiveUserID_asc";
            ViewData["ArchiveDateSortParm"] = sortOrder == "ArchiveDate_asc" ? "ArchiveDate_desc" : "ArchiveDate_asc";

            dtEmployee = EmployeeData.SelectAll();
            dtCompany = Employee_CompanyData.SelectAll();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session["SearchField"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchCondition"])) & !string.IsNullOrEmpty(Convert.ToString(Session["SearchText"])))
                {
                    dtEmployee = EmployeeData.Search(Convert.ToString(Session["SearchField"]), Convert.ToString(Session["SearchCondition"]), Convert.ToString(Session["SearchText"]));
                }
            }
            catch { }

            var Query = from rowEmployee in dtEmployee.AsEnumerable()
                        join rowCompany in dtCompany.AsEnumerable() on rowEmployee.Field<Int32>("CompanyID") equals rowCompany.Field<Int32>("CompanyID")
                        select new Employee()
                        {
                            EmployeeID = rowEmployee.Field<Int32>("EmployeeID")
                           ,
                            FirstName = rowEmployee.Field<String>("FirstName")
                           ,
                            LastName = rowEmployee.Field<String>("LastName")
                           ,
                            DOB = rowEmployee.Field<DateTime>("DOB")
                           ,
                            DOJ = rowEmployee.Field<DateTime>("DOJ")
                           ,
                            Gender = rowEmployee.Field<String>("Gender")
                           ,
                            EMail = rowEmployee.Field<String>("EMail")
                           ,
                            Mobile = rowEmployee.Field<String>("Mobile")
                           ,
                            Address1 = rowEmployee.Field<String>("Address1")
                           ,
                            Address2 = rowEmployee.Field<String>("Address2")
                           ,
                            Salary = rowEmployee.Field<Decimal>("Salary")
                            ,
                            UserName = rowEmployee.Field<String>("UserName")
                           ,
                            Password = rowEmployee.Field<String>("Password")
                           ,
                            SignatureURL = rowEmployee.Field<String>("SignatureURL")
                           ,
                            Company = new Company()
                            {
                                CompanyID = rowCompany.Field<Int32>("CompanyID")
                                  ,
                                CompanyName = rowCompany.Field<String>("CompanyName")
                            }
                           ,
                            AddUserID = rowEmployee.Field<Int32>("AddUserID")
                           ,
                            AddDate = rowEmployee.Field<DateTime>("AddDate")
                           ,
                            ArchiveUserID = rowEmployee.Field<Int32?>("ArchiveUserID")
                           ,
                            ArchiveDate = rowEmployee.Field<DateTime?>("ArchiveDate")
                        };

            switch (sortOrder)
            {
                case "EmployeeID_desc":
                    Query = Query.OrderByDescending(s => s.EmployeeID);
                    break;
                case "EmployeeID_asc":
                    Query = Query.OrderBy(s => s.EmployeeID);
                    break;
                case "FirstName_desc":
                    Query = Query.OrderByDescending(s => s.FirstName);
                    break;
                case "FirstName_asc":
                    Query = Query.OrderBy(s => s.FirstName);
                    break;
                case "LastName_desc":
                    Query = Query.OrderByDescending(s => s.LastName);
                    break;
                case "LastName_asc":
                    Query = Query.OrderBy(s => s.LastName);
                    break;
                case "DOB_desc":
                    Query = Query.OrderByDescending(s => s.DOB);
                    break;
                case "DOB_asc":
                    Query = Query.OrderBy(s => s.DOB);
                    break;
                case "DOJ_desc":
                    Query = Query.OrderByDescending(s => s.DOJ);
                    break;
                case "DOJ_asc":
                    Query = Query.OrderBy(s => s.DOJ);
                    break;
                case "Gender_desc":
                    Query = Query.OrderByDescending(s => s.Gender);
                    break;
                case "Gender_asc":
                    Query = Query.OrderBy(s => s.Gender);
                    break;
                case "EMail_desc":
                    Query = Query.OrderByDescending(s => s.EMail);
                    break;
                case "EMail_asc":
                    Query = Query.OrderBy(s => s.EMail);
                    break;
                case "Mobile_desc":
                    Query = Query.OrderByDescending(s => s.Mobile);
                    break;
                case "Mobile_asc":
                    Query = Query.OrderBy(s => s.Mobile);
                    break;
                case "Address1_desc":
                    Query = Query.OrderByDescending(s => s.Address1);
                    break;
                case "Address1_asc":
                    Query = Query.OrderBy(s => s.Address1);
                    break;
                case "Address2_desc":
                    Query = Query.OrderByDescending(s => s.Address2);
                    break;
                case "Address2_asc":
                    Query = Query.OrderBy(s => s.Address2);
                    break;
                case "Salary_desc":
                    Query = Query.OrderByDescending(s => s.Salary);
                    break;
                case "Salary_asc":
                    Query = Query.OrderBy(s => s.Salary);
                    break;
                case "SignatureURL_desc":
                    Query = Query.OrderByDescending(s => s.SignatureURL);
                    break;
                case "SignatureURL_asc":
                    Query = Query.OrderBy(s => s.SignatureURL);
                    break;
                case "UserName_desc":
                    Query = Query.OrderByDescending(s => s.UserName);
                    break;
                case "UserName_asc":
                    Query = Query.OrderBy(s => s.UserName);
                    break;
                case "Password_desc":
                    Query = Query.OrderByDescending(s => s.Password);
                    break;
                case "Password_asc":
                    Query = Query.OrderBy(s => s.Password);
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
                    Query = Query.OrderBy(s => s.EmployeeID);
                    break;
            }

            if (command == "Export")
            {
                GridView gv = new GridView();
                DataTable dt = new DataTable();
                dt.Columns.Add("Employee I D", typeof(string));
                dt.Columns.Add("First Name", typeof(string));
                dt.Columns.Add("Last Name", typeof(string));
                dt.Columns.Add("D O B", typeof(string));
                dt.Columns.Add("D O J", typeof(string));
                dt.Columns.Add("Gender", typeof(string));
                dt.Columns.Add("E Mail", typeof(string));
                dt.Columns.Add("Mobile", typeof(string));
                dt.Columns.Add("Address1", typeof(string));
                dt.Columns.Add("Address2", typeof(string));
                dt.Columns.Add("Salary", typeof(string));
                dt.Columns.Add("Signature U R L", typeof(string));
                dt.Columns.Add("User Name", typeof(string));
                dt.Columns.Add("Password", typeof(string));
                dt.Columns.Add("Company I D", typeof(string));
                dt.Columns.Add("Add User I D", typeof(string));
                dt.Columns.Add("Add Date", typeof(string));
                dt.Columns.Add("Archive User I D", typeof(string));
                dt.Columns.Add("Archive Date", typeof(string));
                foreach (var item in Query)
                {
                    dt.Rows.Add(
                        item.EmployeeID
                       , item.FirstName
                       , item.LastName
                       , item.DOB
                       , item.DOJ
                       , item.Gender
                       , item.EMail
                       , item.Mobile
                       , item.Address1
                       , item.Address2
                       , item.Salary
                       , item.SignatureURL
                       , item.UserName
                       , item.Password
                       , item.Company.CompanyName
                       , item.AddUserID
                       , item.AddDate
                       , item.ArchiveUserID
                       , item.ArchiveDate
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

        // GET: /Employee/Details/<id>
        public ActionResult Details(
                                      Int32? EmployeeID
                                   )
        {
            if (
                    EmployeeID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtCompany = Employee_CompanyData.SelectAll();
            dtUserType = UserTypePermission_UserTypeData.SelectAll();

            Employee Employee = new Employee();
            Employee.EmployeeID = System.Convert.ToInt32(EmployeeID);
            Employee = EmployeeData.Select_Record(Employee);
            Employee.Company = new Company()
            {
                CompanyID = (Int32)Employee.CompanyID
               ,
                CompanyName = (from DataRow rowCompany in dtCompany.Rows
                               where Employee.CompanyID == (int)rowCompany["CompanyID"]
                               select (String)rowCompany["CompanyName"]).FirstOrDefault()
            };
            Employee.UserType = new UserType()
            {
                UserTypeID = (Int32)Employee.UserTypeID
               ,
                UserTypeName = (from DataRow rowUserType in dtUserType.Rows
                                where Employee.UserTypeID == (int)rowUserType["UserTypeID"]
                                select (String)rowUserType["UserTypeName"]).FirstOrDefault()
            };
            if (Employee == null)
            {
                return HttpNotFound();
            }
            return View(Employee);
        }

        // GET: /Employee/Create
        public ActionResult Create()
        {
            // ComboBox
            ViewData["CompanyID"] = new SelectList(Employee_CompanyData.List(), "CompanyID", "CompanyName");
            ViewData["UserTypeID"] = new SelectList(UserTypePermission_UserTypeData.List(), "UserTypeID", "UserTypeName");
            return View();
        }

        // POST: /Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include =
                           "FirstName"
                   + "," + "LastName"
                   + "," + "DOB"
                   + "," + "DOJ"
                   + "," + "Gender"
                   + "," + "EMail"
                   + "," + "Mobile"
                   + "," + "Address1"
                   + "," + "Address2"
                   + "," + "Salary"
                   + "," + "SignatureURL"
                   + "," + "UserName"
                   + "," + "Password"
                   + "," + "CompanyID"
                   + "," + "UserTypeID"
                   + "," + "AddUserID"
                   + "," + "AddDate"
                   + "," + "ArchiveUserID"
                   + "," + "ArchiveDate"
                   + "," + "IsActive"
                  )] Employee Employee)
        {
          
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                string strpass = Encrypt(Employee.Password);  
                Employee.Password = strpass;
                bSucess = EmployeeData.Add(Employee);
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
            ViewData["CompanyID"] = new SelectList(Employee_CompanyData.List(), "CompanyID", "CompanyName", Employee.CompanyID);
            ViewData["UserTypeID"] = new SelectList(UserTypePermission_UserTypeData.List(), "UserTypeID", "UserTypeName", Employee.UserTypeID);
            return View(Employee);
        }

        public string Encrypt(string str)
        {
            string EncrptKey = "2013;[pnuLIT)WebCodeExpert";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byKey = System.Text.Encoding.UTF8.GetBytes(EncrptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(str);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        // GET: /Employee/Edit/<id>
        public ActionResult Edit(
                                   Int32? EmployeeID
                                )
        {
            if (
                    EmployeeID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee Employee = new Employee();
            Employee.EmployeeID = System.Convert.ToInt32(EmployeeID);
            Employee = EmployeeData.Select_Record(Employee);

            if (Employee == null)
            {
                return HttpNotFound();
            }
            // ComboBox
            ViewData["CompanyID"] = new SelectList(Employee_CompanyData.List(), "CompanyID", "CompanyName", Employee.CompanyID);
            ViewData["UserTypeID"] = new SelectList(UserTypePermission_UserTypeData.List(), "UserTypeID", "UserTypeName", Employee.UserTypeID);
            return View(Employee);
        }

        // POST: /Employee/Edit/<id>
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee Employee)
        {

            Employee oEmployee = new Employee();
            oEmployee.EmployeeID = System.Convert.ToInt32(Employee.EmployeeID);
            oEmployee = EmployeeData.Select_Record(Employee);
            string strpass = Encrypt(Employee.Password);  
            if (ModelState.IsValid)
            {
                bool bSucess = false;
                Employee.Password = strpass;
                bSucess = EmployeeData.Update(oEmployee, Employee);
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
            ViewData["CompanyID"] = new SelectList(Employee_CompanyData.List(), "CompanyID", "CompanyName", Employee.CompanyID);
            ViewData["UserTypeID"] = new SelectList(UserTypePermission_UserTypeData.List(), "UserTypeID", "UserTypeName", Employee.UserTypeID);
            return View(Employee);
        }

        // GET: /Employee/Delete/<id>
        public ActionResult Delete(
                                     Int32? EmployeeID
                                  )
        {
            if (
                    EmployeeID == null
               )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dtCompany = Employee_CompanyData.SelectAll();
            dtUserType = UserTypePermission_UserTypeData.SelectAll();

            Employee Employee = new Employee();
            Employee.EmployeeID = System.Convert.ToInt32(EmployeeID);
            Employee = EmployeeData.Select_Record(Employee);
            Employee.Company = new Company()
            {
                CompanyID = (Int32)Employee.CompanyID
               ,
                CompanyName = (from DataRow rowCompany in dtCompany.Rows
                               where Employee.CompanyID == (int)rowCompany["CompanyID"]
                               select (String)rowCompany["CompanyName"]).FirstOrDefault()
            };
            Employee.UserType = new UserType()
            {
                UserTypeID = (Int32)Employee.UserTypeID
               ,
                UserTypeName = (from DataRow rowUserType in dtUserType.Rows
                                where Employee.UserTypeID == (int)rowUserType["UserTypeID"]
                                select (String)rowUserType["UserTypeName"]).FirstOrDefault()
            };
            if (Employee == null)
            {
                return HttpNotFound();
            }

            return View(Employee);
        }

        // POST: /Employee/Delete/<id>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(
                                            Int32? EmployeeID
                                            )
        {

            Employee Employee = new Employee();
            Employee.EmployeeID = System.Convert.ToInt32(EmployeeID);
            Employee = EmployeeData.Select_Record(Employee);

            bool bSucess = false;
            bSucess = EmployeeData.Delete(Employee);
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
            SelectListItem Item1 = new SelectListItem { Text = "Employee I D", Value = "Employee I D" };
            SelectListItem Item2 = new SelectListItem { Text = "First Name", Value = "First Name" };
            SelectListItem Item3 = new SelectListItem { Text = "Last Name", Value = "Last Name" };
            SelectListItem Item4 = new SelectListItem { Text = "D O B", Value = "D O B" };
            SelectListItem Item5 = new SelectListItem { Text = "D O J", Value = "D O J" };
            SelectListItem Item6 = new SelectListItem { Text = "Gender", Value = "Gender" };
            SelectListItem Item7 = new SelectListItem { Text = "E Mail", Value = "E Mail" };
            SelectListItem Item8 = new SelectListItem { Text = "Mobile", Value = "Mobile" };
            SelectListItem Item9 = new SelectListItem { Text = "Address1", Value = "Address1" };
            SelectListItem Item10 = new SelectListItem { Text = "Address2", Value = "Address2" };
            SelectListItem Item11 = new SelectListItem { Text = "Salary", Value = "Salary" };
            SelectListItem Item12 = new SelectListItem { Text = "Signature U R L", Value = "Signature U R L" };
            SelectListItem Item13 = new SelectListItem { Text = "User Name", Value = "User Name" };
            SelectListItem Item14 = new SelectListItem { Text = "Password", Value = "Password" };
            SelectListItem Item15 = new SelectListItem { Text = "Company I D", Value = "Company I D" };
            SelectListItem Item16 = new SelectListItem { Text = "Add User I D", Value = "Add User I D" };
            SelectListItem Item17 = new SelectListItem { Text = "Add Date", Value = "Add Date" };
            SelectListItem Item18 = new SelectListItem { Text = "Archive User I D", Value = "Archive User I D" };
            SelectListItem Item19 = new SelectListItem { Text = "Archive Date", Value = "Archive Date" };
            SelectListItem Item20 = new SelectListItem { Text = "UserTypeID", Value = "UserTypeID" };

            if (select == "Employee I D") { Item1.Selected = true; }
            else if (select == "First Name") { Item2.Selected = true; }
            else if (select == "Last Name") { Item3.Selected = true; }
            else if (select == "D O B") { Item4.Selected = true; }
            else if (select == "D O J") { Item5.Selected = true; }
            else if (select == "Gender") { Item6.Selected = true; }
            else if (select == "E Mail") { Item7.Selected = true; }
            else if (select == "Mobile") { Item8.Selected = true; }
            else if (select == "Address1") { Item9.Selected = true; }
            else if (select == "Address2") { Item10.Selected = true; }
            else if (select == "Salary") { Item11.Selected = true; }
            else if (select == "Signature U R L") { Item12.Selected = true; }
            else if (select == "User Name") { Item13.Selected = true; }
            else if (select == "Password") { Item14.Selected = true; }
            else if (select == "Company I D") { Item13.Selected = true; }
            else if (select == "Add User I D") { Item14.Selected = true; }
            else if (select == "Add Date") { Item15.Selected = true; }
            else if (select == "Archive User I D") { Item16.Selected = true; }
            else if (select == "Archive Date") { Item17.Selected = true; }
            else if (select == "UserTypeID") { Item17.Selected = true; }

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
            list.Add(Item16);
            list.Add(Item17);
            list.Add(Item18);
            list.Add(Item19);
            list.Add(Item20);

            return list.ToList();
        }

        private void ExportData(String Export, GridView gv, DataTable dt)
        {
            if (Export == "Pdf")
            {
                PDFform pdfForm = new PDFform(dt, "Dbo. Employee", "Many");
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

