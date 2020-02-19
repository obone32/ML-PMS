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
using System.Collections.Generic;
using System.Data;
using System;
using System.Reflection;

namespace CloudTrixApp.Controllers
{
    public class RoleManagementController : Controller
    {
        //
        // GET: /RoleManagement/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /RoleManagement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /RoleManagement/Create
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        public ActionResult Create()
        {
            ViewData["UserTypeID"] = new SelectList(UserTypePermission_UserTypeData.List(), "UserTypeID", "UserTypeName");
            DataTable dtForm = new DataTable();
            dtForm = FormData.SelectAll();
            List<Form> FormDetails = new List<Form>();
            FormDetails = ConvertDataTable<Form>(dtForm);
            ViewBag.MyList = FormDetails;
            return View();
        }

        //
        // POST: /RoleManagement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include =
                           "UserTypeID"
                   + "," + "AddUserID"
                   + "," + "AddDate"
                   + "," + "ArchiveUserID"
                   + "," + "ArchiveDate"
                  )] RoleManagement RoleManagement,
           [Bind(Include =
                           "AddPermission"
                   + "," + "UpdatePermission"
                   + "," + "DeletePermission"
                   + "," + "ViewPermission"
                   + "," + "RoleID"
                   + "," + "FormID"
                  )] RoleManagementDetails RoleManagementDetails)
        {
            //if (ModelState.IsValid)
            //{
            bool bSucess = false;
            bSucess = RoleManagementData.Add(RoleManagement);
            bSucess = RoleManagementDetailsData.Add(RoleManagementDetails);
            if (bSucess == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Can Not Insert");
            }
            //}

            ViewData["UserTypeID"] = new SelectList(UserTypePermission_UserTypeData.List(), "UserTypeID", "UserTypeName", RoleManagement.UserTypeID);
            DataTable dtForm = new DataTable();
            dtForm = FormData.SelectAll();
            List<Form> FormDetails = new List<Form>();
            FormDetails = ConvertDataTable<Form>(dtForm);
            ViewBag.MyList = FormDetails;
            return View(RoleManagement);
        }

        //
        // GET: /Form/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Form/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Form/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Form/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
