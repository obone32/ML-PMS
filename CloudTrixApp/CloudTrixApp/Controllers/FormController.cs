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
    public class FormController : Controller
    {
        //
        // GET: /Form/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Form/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Form/Create
        public ActionResult Create()
        {
            DataTable dtForm = new DataTable();
            dtForm = FormData.SelectAll();
            return View(dtForm);
        }

        //
        // POST: /Form/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
