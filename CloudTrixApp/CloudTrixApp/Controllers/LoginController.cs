using CloudTrixApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudTrixApp.Models;
using CloudTrixApp.Data;

namespace CloudTrixApp.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Login/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Login/Create
        [HttpPost]
        public ActionResult Create(Employee objEmployee)
        {
            try
            {
                // TODO: Add insert logic here
                Login(objEmployee.UserName, objEmployee.Password);
              //  

                if (ModelState.IsValid)
                {
                    bool bSucess = false;
                    bSucess = Login(objEmployee.UserName, objEmployee.Password);
                    if (bSucess == true)
                    {
                        return RedirectToAction("~/Home/Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Can Not Found");
                    }
                }
                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }

        }
        public bool Login(string UserName, string Password)
        {
            try
            {
                // TODO: Add insert logic here

                Employee Employee = new Employee();
                Employee.UserName = UserName;
                Employee.Password = Password;

                Employee = EmployeeData.Select_Record(Employee);

                if (Employee != null)
                {
                    return true;
                    //   return View("UserLandingView");
                }
                else
                {
                    return false;
                    //  ViewBag.Failedcount = item;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        //
        // GET: /Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Login/Edit/5
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
        // GET: /Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Login/Delete/5
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
