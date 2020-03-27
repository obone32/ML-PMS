using CloudTrixApp.Data;
using CloudTrixApp.Models;
using CloudTrixApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CloudTrixApp.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            //Role Permission
            string RemoveTab = "";
            string ShowTab = "";
            int EmployeeID = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
            DataTable dtRolePermission = ClientData.Role_Permission(EmployeeID, 0);
            DataRow[] rows = dtRolePermission.Select();
            if (dtRolePermission != null)
            {
                foreach (DataRow item in dtRolePermission.Rows)
                {
                    if (item["AddPermission"].ToString() == "False" && item["UpdatePermission"].ToString() == "False" && item["DeletePermission"].ToString() == "False" && item["ViewPermission"].ToString() == "False")
                    {
                        RemoveTab += item["FormID"].ToString() + "|";
                    }
                    else
                    {
                        ShowTab += item["FormID"].ToString() + "|";
                    }
                }
            }
            TempData["RemoveTab"] = RemoveTab;
            TempData["ShowTab"] = ShowTab;
            LoadDataCount();
            return View();
        }
        public void LoadDataCount()
        {
            //Project
            DataTable dtProject = Data.ProjectData.SelectAll();
            TempData["TotalPRoject"] = dtProject.Rows.Count;
            //Load Project data
            if (dtProject.Rows.Count > 0)
            {
                List<Project> ProjectDataDetails = new List<Project>();
                ProjectDataDetails = DataTableToList<Project>(dtProject);
                ViewBag.ProjectDetailsList = ProjectDataDetails;
            }
            //==================
            //Company
            DataTable dtCompany = Data.CompanyData.SelectAll();
            TempData["TotalCompany"] = dtCompany.Rows.Count;
            //Load Company data
            if (dtCompany.Rows.Count > 0)
            {
                List<Company> CompanyDataDetails = new List<Company>();
                CompanyDataDetails = DataTableToList<Company>(dtCompany);
                ViewBag.CompanyDetailsList = CompanyDataDetails;
            }
            //==================
            //Client
            DataTable dtClient = Data.ClientData.SelectAll();
            TempData["TotalClient"] = dtClient.Rows.Count;
            //Load Client data
            if (dtClient.Rows.Count > 0)
            {
                List<Client> ClientDataDetails = new List<Client>();
                ClientDataDetails = DataTableToList<Client>(dtClient);
                ViewBag.ClientDetailsList = ClientDataDetails;
            }
            //==================
            //Employee
            DataTable dtEmployee = Data.EmployeeData.SelectAll();
            TempData["TotalEmployee"] = dtEmployee.Rows.Count;
            //Load Employee data
            if (dtEmployee.Rows.Count > 0)
            {
                List<CloudTrixApp.Models.Employee> EmployeeDataDetails = new List<CloudTrixApp.Models.Employee>();
                EmployeeDataDetails = DataTableToList<CloudTrixApp.Models.Employee>(dtEmployee);
                ViewBag.EmployeeDetailsList = EmployeeDataDetails;
            }
            //==================
            //Invoice
            DataTable dtInvoice = Data.InvoiceData.SelectAll();
            TempData["TotalInvoice"] = dtInvoice.Rows.Count;
            //Load Invoice data
            if (dtInvoice.Rows.Count > 0)
            {
                List<Invoice> InvoiceDataDetails = new List<Invoice>();
                InvoiceDataDetails = DataTableToList<Invoice>(dtInvoice);
                ViewBag.InvoiceDetailsList = InvoiceDataDetails;
            }
            //==================
            //Task Management
            DataTable dtTaskManagement = Data.TaskAssignmentData.SelectAll();
            TempData["TotalTaskManagement"] = dtTaskManagement.Rows.Count;
            //Load Invoice data
            if (dtTaskManagement.Rows.Count > 0)
            {
              //  List<Task_Details> TaskDataDetails = new List<Task_Details>();
                List<DataRow> rows = dtTaskManagement.Rows.Cast<DataRow>().ToList();
              //  TaskDataDetails = DataTableToList<Task_Details>(dtTaskManagement);
                ViewBag.TaskManagementDetailsList = rows.ToList();
            }
            //SqlConnection connection = PMMSData.GetConnection();
            //var sql = "Task_Details";
            //var multi = connection.QueryMultiple(sql);
            //DataTable dtTaskManagement = Data.TaskAssignmentData.SelectAll();
            //TempData["TotalTaskManagement"] = dtTaskManagement.Rows.Count;
            ////Load Invoice data
            //if (dtTaskManagement.Rows.Count > 0)
            //{
            //    Task_Details ObjTask_Details = new Task_Details();

            //    //Assigning each Multiple tables data to specific single model class  
            //    ObjTask_Details.clasTask = connection.Read<Task>().ToList();
            //    ObjTask_Details.clasTaskState = objDetails.Read<TaskState>().ToList();
            //    ObjTask_Details.clsTaskAssignment = objDetails.Read<TaskAssignment>().ToList();

            //    List<MasterDetails> CustomerObj = new List<MasterDetails>();  

            //    //List<Task_Details> TaskDataDetails = new List<Task_Details>();
            //    //TaskDataDetails = DataTableToList<Task_Details>(dtTaskManagement);
            //    //ViewBag.TaskManagementDetailsList = TaskDataDetails;
            //}
            //==================
            //Time Sheet
            DataTable dtTimeSheet = Data.TimesheetData.SelectAll();
            TempData["TotalTimeSheet"] = dtTimeSheet.Rows.Count;
            //Load Time Sheet data
            if (dtTimeSheet.Rows.Count > 0)
            {
                List<Timesheet> TimesheetDetails = new List<Timesheet>();
                TimesheetDetails = DataTableToList<Timesheet>(dtTimeSheet);
                ViewBag.TimeSheetDetailsList = TimesheetDetails;
            }
        }
        [Authorize]
        public ActionResult Profile()
        {
            return View();
        }


        [HttpPost]
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Create", "Login");
        }
        public static List<T> DataTableToList<T>(DataTable dt) where T : class, new()
        {
            List<T> lstItems = new List<T>();
            if (dt != null && dt.Rows.Count > 0)
                foreach (DataRow row in dt.Rows)
                    lstItems.Add(ConvertDataRowToGenericType<T>(row));
            else
                lstItems = null;
            return lstItems;
        }
        private static T ConvertDataRowToGenericType<T>(DataRow row) where T : class,new()
        {
            Type entityType = typeof(T);
            T objEntity = new T();
            foreach (DataColumn column in row.Table.Columns)
            {
                object value = row[column.ColumnName];
                if (value == DBNull.Value) value = null;
                PropertyInfo property = entityType.GetProperty(column.ColumnName, BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public);
                try
                {
                    if (property != null && property.CanWrite)
                        property.SetValue(objEntity, value, null);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return objEntity;
        }
    }
}

