using CloudTrixApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudTrixApp.Models;
using CloudTrixApp.Data;
using System.Security.Cryptography;
using System.IO;
using System.Web.Security;

namespace CloudTrixApp.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        DataTable dtEmployee = new DataTable();
        int EmployeeId = 0;
        public ActionResult Index()
        {
            //  User.Identity.
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee objEmployee)
        {
            try
            {
                // TODO: Add insert logic here
                //if (ModelState.IsValid)
                //{
                // Login(objEmployee.UserName, objEmployee.Password);
                //  


                bool bSucess = false;
                bSucess = Login(objEmployee.UserName, objEmployee.Password);
                if (bSucess == true)
                {
                    FormsAuthentication.SetAuthCookie(objEmployee.UserName + "|" + EmployeeId, false);

                    //Role Permission
                    string RemoveTab="";
                    string ShowTab = "";
                    DataTable dtRolePermission = ClientData.Role_Permission(EmployeeId, 0);
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
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Found");
                }
                //}
                return RedirectToAction("Create");
            }
            catch
            {
                return View();
            }

        }
        public string Decrypt(string str)
        {
            str = str.Replace(" ", "+");
            string DecryptKey = "2013;[pnuLIT)WebCodeExpert";
            byte[] byKey = { };
            byte[] IV = { 18, 52, 86, 120, 144, 171, 205, 239 };
            byte[] inputByteArray = new byte[str.Length];

            byKey = System.Text.Encoding.UTF8.GetBytes(DecryptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(str.Replace(" ", "+"));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        public bool Login(string UserName, string Password)
        {
            try
            {
                // TODO: Add insert logic here

                string LoginPwd = Password;
                dtEmployee = EmployeeData.SelectLoginData(UserName);
                DataRow[] rows = dtEmployee.Select();
                if (dtEmployee != null)
                {
                    EmployeeId = Convert.ToInt32(rows[0]["EmployeeID"]);
                    string decryptpassword = rows[0]["Password"].ToString();

                    string decPwd = Decrypt(decryptpassword);

                    //System.Web.Security.FormsAuthentication.SetAuthCookie(user.Username, false);

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
