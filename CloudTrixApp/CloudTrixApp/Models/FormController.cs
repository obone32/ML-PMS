using CloudTrixApp.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudTrixApp.Models
{
    public class FormController : Controller
    {
        DataTable dtForm = new DataTable();

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
            dtForm = FormData.SelectAll();
            //foreach (var item in dtForm.Columns)
            //{
            //   item =item(s=>s.)
            //}
            ViewData.Model = dtForm.AsEnumerable();
            return View();
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
